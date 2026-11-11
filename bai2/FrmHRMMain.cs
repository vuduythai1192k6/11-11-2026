using System;
using System.Windows.Forms;
using HRMPayrollSystem.BUS;
using HRMPayrollSystem.DTO;

namespace HRMPayrollSystem.GUI
{
    public partial class FrmHRMMain : Form
    {
        private readonly HRMBUS bus = new HRMBUS();

        public FrmHRMMain()
        {
            InitializeComponent();
        }

        private void FrmHRMMain_Load(object sender, EventArgs e)
        {
            try
            {
                cboPhongBan.DataSource = bus.LayPhongBan();
                cboPhongBan.DisplayMember = "TenPB";
                cboPhongBan.ValueMember = "MaPB";

                LoadDanhSachNV();
                dgvChamCong.DataSource = bus.NapBangChamCong(txtThangNamCC.Text.Trim());
                dgvBangLuong.DataSource = bus.LayBangLuong(txtThangNamLuong.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa kết nối được CSDL HRMDB. Hãy chạy file ScriptDB.sql trước!\nChi tiết: " + ex.Message,
                    "Cảnh báo kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDanhSachNV()
        {
            dgvNhanVien.DataSource = bus.LayNhanVien();
        }

        // ================= SỰ KIỆN TAB 1: HỒ SƠ NHÂN SỰ =================
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString();
                if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out DateTime ns))
                    dtpNgaySinh.Value = ns;
                cboPhongBan.SelectedValue = row.Cells["MaPB"].Value;
                txtLuongCoBan.Text = row.Cells["LuongCoBan"].Value?.ToString();
                txtHeSoLuong.Text = row.Cells["HeSoLuong"].Value?.ToString();
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtLuongCoBan.Text, out decimal lcb) || !double.TryParse(txtHeSoLuong.Text, out double hsl))
            {
                MessageBox.Show("Lương cơ bản và Hệ số lương phải là số!");
                return;
            }
            var nv = new NhanVienHRMDTO
            {
                HoTen = txtHoTen.Text.Trim(),
                CCCD = txtCCCD.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                MaPB = Convert.ToInt32(cboPhongBan.SelectedValue),
                LuongCoBan = lcb,
                HeSoLuong = hsl
            };
            if (bus.ThemNhanVien(nv, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachNV();
            }
            else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaNV.Text, out int maNV))
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên trên lưới!");
                return;
            }
            var nv = new NhanVienHRMDTO
            {
                MaNV = maNV,
                HoTen = txtHoTen.Text.Trim(),
                CCCD = txtCCCD.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                MaPB = Convert.ToInt32(cboPhongBan.SelectedValue),
                LuongCoBan = Convert.ToDecimal(txtLuongCoBan.Text),
                HeSoLuong = Convert.ToDouble(txtHeSoLuong.Text)
            };
            if (bus.SuaNhanVien(nv, out string msg))
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachNV();
            }
            else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaNV.Text, out int maNV))
            {
                if (bus.XoaNhanVien(maNV, out string msg))
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNV();
                }
                else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear(); txtHoTen.Clear(); txtCCCD.Clear(); txtLuongCoBan.Clear(); txtHeSoLuong.Clear(); txtTimKiemNV.Clear();
            LoadDanhSachNV();
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = bus.LayNhanVien(txtTimKiemNV.Text.Trim());
        }

        // ================= SỰ KIỆN TAB 2: CHẤM CÔNG DISCONNECTED =================
        private void btnNapChamCong_Click(object sender, EventArgs e)
        {
            dgvChamCong.DataSource = bus.NapBangChamCong(txtThangNamCC.Text.Trim());
        }

        private void btnLuuChamCong_Click(object sender, EventArgs e)
        {
            dgvChamCong.EndEdit();
            if (bus.LuuChamCong(out string msg))
                MessageBox.Show(msg, "Đồng bộ Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(msg, "Lỗi Đồng bộ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ================= SỰ KIỆN TAB 3: TÍNH LƯƠNG & CHỐT LƯƠNG =================
        private void btnChotLuong_Click(object sender, EventArgs e)
        {
            string thang = txtThangNamLuong.Text.Trim();
            if (bus.ChotLuong(thang, out string msg))
            {
                MessageBox.Show(msg, "Chốt lương Thành công (Committed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBangLuong.DataSource = bus.LayBangLuong(thang);
                dgvChamCong.DataSource = bus.NapBangChamCong(thang);
            }
            else
            {
                MessageBox.Show(msg, "Chốt lương Thất bại (Rolled Back)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemBangLuong_Click(object sender, EventArgs e)
        {
            dgvBangLuong.DataSource = bus.LayBangLuong(txtThangNamLuong.Text.Trim());
        }
    }
}
