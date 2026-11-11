using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.DAL
{
    public class SalesDAL
    {
        // 1. Quản lý Sản phẩm (CRUD + Tìm kiếm chống SQL Injection)
        public DataTable GetSanPham(string keyword = "")
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham WHERE MaSP LIKE @kw OR TenSP LIKE @kw";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemSanPham(SanPhamDTO sp)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES (@MaSP, @TenSP, @GiaBan, @SoLuongTon)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaSanPham(SanPhamDTO sp)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE SanPham SET TenSP = @TenSP, GiaBan = @GiaBan, SoLuongTon = @SoLuongTon WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaSanPham(string maSP)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetNhanVien()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaNV, TenNV, SoDienThoai, ChucVu FROM NhanVien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetKhachHang()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaKH, TenKH, DienThoai, DiemTichLuy FROM KhachHang", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Giao dịch Thanh toán Hóa đơn (SqlTransaction)
        public bool ThanhToanHoaDon(int maNV, int? maKH, decimal tongTien, List<ChiTietGioHangDTO> gioHang, out string message)
        {
            message = "";
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Bước 1: Tạo Hóa đơn
                    string sqlHD = @"INSERT INTO HoaDon (MaNV, MaKH, TongTien) VALUES (@MaNV, @MaKH, @TongTien);
                                     SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans);
                    cmdHD.Parameters.AddWithValue("@MaNV", maNV);
                    cmdHD.Parameters.AddWithValue("@MaKH", (object)maKH ?? DBNull.Value);
                    cmdHD.Parameters.AddWithValue("@TongTien", tongTien);
                    int maHD = Convert.ToInt32(cmdHD.ExecuteScalar());

                    // Bước 2 & 3: Thêm Chi tiết & Trừ tồn kho
                    foreach (var item in gioHang)
                    {
                        string sqlCheck = "SELECT SoLuongTon FROM SanPham WITH (UPDLOCK) WHERE MaSP = @MaSP";
                        SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans);
                        cmdCheck.Parameters.AddWithValue("@MaSP", item.MaSP);
                        int tonKho = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (tonKho < item.SoLuong)
                        {
                            trans.Rollback();
                            message = $"Lỗi Transaction Rollback: Sản phẩm [{item.MaSP} - {item.TenSP}] chỉ còn {tonKho} trong kho, không đủ bán {item.SoLuong}!";
                            return false;
                        }

                        string sqlCT = "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) VALUES (@MaHD, @MaSP, @SoLuong, @DonGia)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                        cmdCT.Parameters.AddWithValue("@MaHD", maHD);
                        cmdCT.Parameters.AddWithValue("@MaSP", item.MaSP);
                        cmdCT.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                        cmdCT.Parameters.AddWithValue("@DonGia", item.DonGia);
                        cmdCT.ExecuteNonQuery();

                        string sqlUpd = "UPDATE SanPham SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP = @MaSP";
                        SqlCommand cmdUpd = new SqlCommand(sqlUpd, conn, trans);
                        cmdUpd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                        cmdUpd.Parameters.AddWithValue("@MaSP", item.MaSP);
                        cmdUpd.ExecuteNonQuery();
                    }

                    // Bước 4: Cộng điểm tích lũy
                    if (maKH.HasValue)
                    {
                        int diemCong = (int)(tongTien / 100000m);
                        string sqlKH = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @Diem WHERE MaKH = @MaKH";
                        SqlCommand cmdKH = new SqlCommand(sqlKH, conn, trans);
                        cmdKH.Parameters.AddWithValue("@Diem", diemCong);
                        cmdKH.Parameters.AddWithValue("@MaKH", maKH.Value);
                        cmdKH.ExecuteNonQuery();
                    }

                    // Bước 5: Commit
                    trans.Commit();
                    message = $"Thanh toán thành công Hóa đơn #{maHD}!";
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    message = "Lỗi hệ thống (Đã Rollback): " + ex.Message;
                    return false;
                }
            }
        }

        // 3. Thống kê Doanh thu (ExecuteScalar) & Top 5 SP (SqlDataReader)
        public decimal GetDoanhThuNgay(DateTime ngay)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE CAST(NgayLap AS DATE) = @Ngay";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public DataTable GetTop5SanPham()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSP", typeof(string));
            dt.Columns.Add("TenSP", typeof(string));
            dt.Columns.Add("TongSoLuongBan", typeof(int));

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT TOP 5 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongBan
                               FROM ChiTietHoaDon ct
                               JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               GROUP BY sp.MaSP, sp.TenSP
                               ORDER BY TongBan DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                    }
                }
            }
            return dt;
        }
    }
}
