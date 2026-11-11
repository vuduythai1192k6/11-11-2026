using System;
using System.Collections.Generic;
using System.Data;
using RetailSalesSystem.DAL;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.BUS
{
    public class SalesBUS
    {
        private readonly SalesDAL dal = new SalesDAL();

        public DataTable LayDanhSachSanPham(string kw = "") => dal.GetSanPham(kw);
        public DataTable LayDanhSachNhanVien() => dal.GetNhanVien();
        public DataTable LayDanhSachKhachHang() => dal.GetKhachHang();

        public bool ThemSanPham(SanPhamDTO sp, out string msg)
        {
            if (string.IsNullOrWhiteSpace(sp.MaSP) || string.IsNullOrWhiteSpace(sp.TenSP))
            {
                msg = "Mã sản phẩm và Tên sản phẩm không được để trống!";
                return false;
            }
            if (sp.GiaBan <= 0 || sp.SoLuongTon < 0)
            {
                msg = "Giá bán phải > 0 và Số lượng tồn >= 0!";
                return false;
            }
            try
            {
                bool ok = dal.ThemSanPham(sp);
                msg = ok ? "Thêm sản phẩm thành công!" : "Thêm thất bại!";
                return ok;
            }
            catch (Exception ex)
            {
                msg = "Lỗi: " + ex.Message;
                return false;
            }
        }

        public bool SuaSanPham(SanPhamDTO sp, out string msg)
        {
            try
            {
                bool ok = dal.SuaSanPham(sp);
                msg = ok ? "Cập nhật sản phẩm thành công!" : "Không tìm thấy mã sản phẩm!";
                return ok;
            }
            catch (Exception ex)
            {
                msg = "Lỗi: " + ex.Message;
                return false;
            }
        }

        public bool XoaSanPham(string maSP, out string msg)
        {
            try
            {
                bool ok = dal.XoaSanPham(maSP);
                msg = ok ? "Xóa sản phẩm thành công!" : "Không tìm thấy sản phẩm!";
                return ok;
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa sản phẩm đã có trong hóa đơn! Chi tiết: " + ex.Message;
                return false;
            }
        }

        public bool ThanhToan(int maNV, int? maKH, decimal tongTien, List<ChiTietGioHangDTO> gioHang, out string msg)
        {
            if (gioHang == null || gioHang.Count == 0)
            {
                msg = "Giỏ hàng đang trống!";
                return false;
            }
            return dal.ThanhToanHoaDon(maNV, maKH, tongTien, gioHang, out msg);
        }

        public decimal TinhDoanhThuNgay(DateTime ngay) => dal.GetDoanhThuNgay(ngay);
        public DataTable LayTop5SanPhamBanChay() => dal.GetTop5SanPham();
    }
}
