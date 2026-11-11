using System;
using System.Data;
using System.Data.SqlClient;
using HRMPayrollSystem.DTO;

namespace HRMPayrollSystem.DAL
{
    public class HRMDAL
    {
        private SqlDataAdapter adapterChamCong;
        private DataTable dtChamCong;

        // 1. Quản lý Hồ sơ Nhân sự (3-Tier CRUD + Tìm kiếm SqlParameter)
        public DataTable GetPhongBan()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaPB, TenPB, Mota FROM PhongBan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetNhanVien(string keyword = "")
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, nv.MaPB, pb.TenPB, nv.LuongCoBan, nv.HeSoLuong
                               FROM NhanVien nv
                               LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                               WHERE nv.HoTen LIKE @kw OR nv.CCCD LIKE @kw OR pb.TenPB LIKE @kw";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemNhanVien(NhanVienHRMDTO nv)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong)
                               VALUES (@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                cmd.Parameters.AddWithValue("@MaPB", nv.MaPB);
                cmd.Parameters.AddWithValue("@LuongCoBan", nv.LuongCoBan);
                cmd.Parameters.AddWithValue("@HeSoLuong", nv.HeSoLuong);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaNhanVien(NhanVienHRMDTO nv)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE NhanVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, CCCD=@CCCD, 
                               MaPB=@MaPB, LuongCoBan=@LuongCoBan, HeSoLuong=@HeSoLuong WHERE MaNV=@MaNV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                cmd.Parameters.AddWithValue("@MaPB", nv.MaPB);
                cmd.Parameters.AddWithValue("@LuongCoBan", nv.LuongCoBan);
                cmd.Parameters.AddWithValue("@HeSoLuong", nv.HeSoLuong);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaNhanVien(int maNV)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 2. Chấm công Ngắt kết nối (Disconnected Model: SqlDataAdapter + SqlCommandBuilder)
        public DataTable LoadBangChamCongDisconnected(string thangNam)
        {
            using (SqlConnection connInit = DbHelper.GetConnection())
            {
                connInit.Open();
                // Tự động khởi tạo dòng chấm công = 0 cho nhân viên chưa có trong tháng để tiện nhập trên lưới
                string sqlInit = @"INSERT INTO BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot)
                                   SELECT MaNV, @ThangNam, 22, 0, 0 FROM NhanVien
                                   WHERE MaNV NOT IN (SELECT MaNV FROM BangChamCong WHERE ThangNam = @ThangNam)";
                SqlCommand cmdInit = new SqlCommand(sqlInit, connInit);
                cmdInit.Parameters.AddWithValue("@ThangNam", thangNam);
                cmdInit.ExecuteNonQuery();
            }

            SqlConnection conn = DbHelper.GetConnection();
            string sql = "SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot FROM BangChamCong WHERE ThangNam = @ThangNam";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ThangNam", thangNam);

            adapterChamCong = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapterChamCong);

            dtChamCong = new DataTable();
            adapterChamCong.Fill(dtChamCong);
            return dtChamCong;
        }

        public int LuuChamCongDisconnected()
        {
            if (adapterChamCong != null && dtChamCong != null)
            {
                return adapterChamCong.Update(dtChamCong);
            }
            return 0;
        }

        // 3. Tính Lương & Chốt Lương (SqlTransaction)
        public bool ChotLuongThang(string thangNam, out string msg)
        {
            msg = "";
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string sqlCheck = "SELECT COUNT(*) FROM BangChamCong WHERE ThangNam = @ThangNam AND DaChot = 1";
                    SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans);
                    cmdCheck.Parameters.AddWithValue("@ThangNam", thangNam);
                    if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
                    {
                        trans.Rollback();
                        msg = $"Tháng {thangNam} đã được chốt lương và khóa dữ liệu trước đó!";
                        return false;
                    }

                    string sqlInsert = @"
                        INSERT INTO BangLuong (MaNV, ThangNam, ThucLinh)
                        SELECT 
                            nv.MaNV, 
                            cc.ThangNam,
                            CAST(((nv.LuongCoBan * nv.HeSoLuong / 22.0) * cc.SoNgayCong + (cc.SoGioOT * 50000)) AS DECIMAL(18,2))
                        FROM BangChamCong cc
                        JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                        WHERE cc.ThangNam = @ThangNam AND cc.DaChot = 0";

                    SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, trans);
                    cmdInsert.Parameters.AddWithValue("@ThangNam", thangNam);
                    int rows = cmdInsert.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        trans.Rollback();
                        msg = $"Không có dữ liệu chấm công tháng {thangNam} để chốt lương!";
                        return false;
                    }

                    string sqlLock = "UPDATE BangChamCong SET DaChot = 1 WHERE ThangNam = @ThangNam";
                    SqlCommand cmdLock = new SqlCommand(sqlLock, conn, trans);
                    cmdLock.Parameters.AddWithValue("@ThangNam", thangNam);
                    cmdLock.ExecuteNonQuery();

                    trans.Commit();
                    msg = $"Chốt lương thành công cho {rows} nhân viên tháng {thangNam}!";
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    msg = "Lỗi chốt lương (Đã Rollback): " + ex.Message;
                    return false;
                }
            }
        }

        public DataTable GetBangLuong(string thangNam)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string sql = @"SELECT bl.MaLuong, bl.MaNV, nv.HoTen, pb.TenPB, bl.ThangNam, 
                                      nv.LuongCoBan, nv.HeSoLuong, cc.SoNgayCong, cc.SoGioOT, bl.ThucLinh
                               FROM BangLuong bl
                               JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                               LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                               LEFT JOIN BangChamCong cc ON bl.MaNV = cc.MaNV AND bl.ThangNam = cc.ThangNam
                               WHERE bl.ThangNam = @ThangNam";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ThangNam", thangNam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
