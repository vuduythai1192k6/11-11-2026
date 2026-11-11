using System;
using System.Data;
using HRMPayrollSystem.DAL;
using HRMPayrollSystem.DTO;

namespace HRMPayrollSystem.BUS
{
    public class HRMBUS
    {
        private readonly HRMDAL dal = new HRMDAL();

        public DataTable LayPhongBan() => dal.GetPhongBan();
        public DataTable LayNhanVien(string kw = "") => dal.GetNhanVien(kw);

        public bool ThemNhanVien(NhanVienHRMDTO nv, out string msg)
        {
            if (string.IsNullOrWhiteSpace(nv.HoTen) || string.IsNullOrWhiteSpace(nv.CCCD))
            {
                msg = "Họ tên và CCCD không được để trống!";
                return false;
            }
            try
            {
                bool ok = dal.ThemNhanVien(nv);
                msg = ok ? "Thêm nhân viên thành công!" : "Thêm thất bại!";
                return ok;
            }
            catch (Exception ex)
            {
                msg = "Lỗi: " + ex.Message;
                return false;
            }
        }

        public bool SuaNhanVien(NhanVienHRMDTO nv, out string msg)
        {
            try
            {
                bool ok = dal.SuaNhanVien(nv);
                msg = ok ? "Cập nhật hồ sơ nhân sự thành công!" : "Không tìm thấy nhân viên!";
                return ok;
            }
            catch (Exception ex)
            {
                msg = "Lỗi: " + ex.Message;
                return false;
            }
        }

        public bool XoaNhanVien(int maNV, out string msg)
        {
            try
            {
                bool ok = dal.XoaNhanVien(maNV);
                msg = ok ? "Đã xóa nhân viên!" : "Không tìm thấy nhân viên!";
                return ok;
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa nhân viên đã có dữ liệu chấm công/lương! " + ex.Message;
                return false;
            }
        }

        public DataTable NapBangChamCong(string thangNam) => dal.LoadBangChamCongDisconnected(thangNam);

        public bool LuuChamCong(out string msg)
        {
            try
            {
                int rows = dal.LuuChamCongDisconnected();
                msg = $"Đã đồng bộ thành công {rows} dòng chấm công về SQL Server (SqlCommandBuilder)!";
                return true;
            }
            catch (Exception ex)
            {
                msg = "Lỗi đồng bộ chấm công: " + ex.Message;
                return false;
            }
        }

        public bool ChotLuong(string thangNam, out string msg) => dal.ChotLuongThang(thangNam, out msg);
        public DataTable LayBangLuong(string thangNam) => dal.GetBangLuong(thangNam);
    }
}
