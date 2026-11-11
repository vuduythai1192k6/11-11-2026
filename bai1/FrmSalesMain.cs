using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RetailSalesSystem.BUS;
using RetailSalesSystem.DTO;

namespace RetailSalesSystem.GUI
{
    public partial class FrmSalesMain : Form
    {
        private readonly SalesBUS bus = new SalesBUS();
        private readonly List<ChiTietGioHangDTO> gioHang = new List<ChiTietGioHangDTO>();

        public FrmSalesMain()
        {
            InitializeComponent();
        }

        private void FrmSalesMain_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa kết nối được CSDL SalesDB. Hãy chạy file ScriptDB.sql trước!\nChi tiết: " + ex.Message, 
                    "Cảnh báo kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAllData()
        {
            DataTable dtSP = bus.LayDanhSachSanPham();
            dgvSanPham.DataSource = dtSP;

            cboChonSP.DataSource = dtSP.Copy();
            cboChonSP.DisplayMember = "TenSP";
            cboChonSP.ValueMember = "MaSP";

            cboNhanVien.DataSource = bus.LayDanhSachNhanVien();
            cboNhanVien.DisplayMember = "TenNV";
            cboNhanVien.ValueMember = "MaNV";

            DataTable dtKH = bus.LayDanhSachKhachHang();
            DataRow rowKhachLe = dtKH.NewRow();
            rowKhachLe["MaKH"] = DBNull.Value;
            rowKhachLe["TenKH"] = "--- Khách lẻ (Không tích điểm) ---";
            dtKH.Rows.InsertAt(rowKhachLe, 0);
            cboKhachHang.DataSource = dtKH;
            cboKhachHang.DisplayMember = "TenKH";
            cboKhachHang.ValueMember = "MaKH";

            CapNhatLuoiGioHang();
            LoadThongKe();
        }

        // ================= SỰ KIỆN TAB 1: CRUD SẢN PHẨM =================
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaSP"].Value?.ToString();
                txtTenSP.Text = row.Cells["TenSP"].Value?.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value?.ToString();
            }
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtGiaBan.Text, out decimal gia) || !int.TryParse(txtSoLuongTon.Text, out int ton))
            {
                MessageBox.Show("Giá bán và số lượng tồn phải là số hợp lệ!");
                return;
            }
            var sp = new SanPhamDTO { MaSP = txtMaSP.Text.Trim(), TenSP = txtTenSP.Text.Trim(), GiaBan = gia, SoLuongTon = ton };
            if (bus.ThemSanPham(sp, out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllData();
            }
            else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtGiaBan.Text, out decimal gia) || !int.TryParse(txtSoLuongTon.Text, out int ton))
            {
                MessageBox.Show("Giá bán và số lượng tồn phải là số hợp lệ!");
                return;
            }
            var sp = new SanPhamDTO { MaSP = txtMaSP.Text.Trim(), TenSP = txtTenSP.Text.Trim(), GiaBan = gia, SoLuongTon = ton };
            if (bus.SuaSanPham(sp, out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllData();
            }
            else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bus.XoaSanPham(txtMaSP.Text.Trim(), out string msg))
                {
                    MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                }
                else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoiSP_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear(); txtTenSP.Clear(); txtGiaBan.Clear(); txtSoLuongTon.Clear(); txtTimKiemSP.Clear();
            LoadAllData();
        }

        private void btnTimKiemSP_Click(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = bus.LayDanhSachSanPham(txtTimKiemSP.Text.Trim());
        }

        // ================= SỰ KIỆN TAB 2: BÁN HÀNG & TRANSACTION =================
        private void btnThemGioHang_Click(object sender, EventArgs e)
        {
            if (cboChonSP.SelectedItem is DataRowView row)
            {
                string maSP = row["MaSP"].ToString();
                string tenSP = row["TenSP"].ToString();
                decimal donGia = Convert.ToDecimal(row["GiaBan"]);
                int slMua = (int)numSoLuongMua.Value;

                var existing = gioHang.FirstOrDefault(x => x.MaSP == maSP);
                if (existing != null) existing.SoLuong += slMua;
                else gioHang.Add(new ChiTietGioHangDTO { MaSP = maSP, TenSP = tenSP, DonGia = donGia, SoLuong = slMua });

                CapNhatLuoiGioHang();
            }
        }

        private void btnXoaGioHang_Click(object sender, EventArgs e)
        {
            gioHang.Clear();
            CapNhatLuoiGioHang();
        }

        private void CapNhatLuoiGioHang()
        {
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = gioHang.ToList();
            decimal tong = gioHang.Sum(x => x.ThanhTien);
            int diem = (int)(tong / 100000m);
            lblTongTien.Text = $"Tổng tiền: {tong:N0} VNĐ";
            lblDiemCong.Text = $"Điểm tích lũy cộng thêm: +{diem} điểm";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int maNV = Convert.ToInt32(cboNhanVien.SelectedValue);
            int? maKH = (cboKhachHang.SelectedValue == null || cboKhachHang.SelectedValue == DBNull.Value)
                ? (int?)null
                : Convert.ToInt32(cboKhachHang.SelectedValue);
            decimal tongTien = gioHang.Sum(x => x.ThanhTien);

            if (bus.ThanhToan(maNV, maKH, tongTien, gioHang, out string msg))
            {
                MessageBox.Show(msg, "Giao dịch Thành công (Committed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gioHang.Clear();
                LoadAllData();
            }
            else
            {
                MessageBox.Show(msg, "Giao dịch Thất bại (Rolled Back)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= SỰ KIỆN TAB 3: BÁO CÁO THỐNG KÊ =================
        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            decimal dtNgay = bus.TinhDoanhThuNgay(dtpNgayThongKe.Value);
            lblDoanhThuNgay.Text = $"Tổng doanh thu ngày {dtpNgayThongKe.Value:dd/MM/yyyy} (ExecuteScalar): {dtNgay:N0} VNĐ";
            dgvTop5SP.DataSource = bus.LayTop5SanPhamBanChay();
        }
    }
}
