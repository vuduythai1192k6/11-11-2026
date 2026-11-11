namespace HRMPayrollSystem.GUI
{
    partial class FrmHRMMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabNhanSu;
        private System.Windows.Forms.TabPage tabChamCong;
        private System.Windows.Forms.TabPage tabTinhLuong;

        // Tab 1 Controls
        private System.Windows.Forms.TextBox txtMaNV, txtHoTen, txtCCCD, txtLuongCoBan, txtHeSoLuong, txtTimKiemNV;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Button btnThemNV, btnSuaNV, btnXoaNV, btnLamMoiNV, btnTimKiemNV;
        private System.Windows.Forms.DataGridView dgvNhanVien;

        // Tab 2 Controls
        private System.Windows.Forms.TextBox txtThangNamCC;
        private System.Windows.Forms.Button btnNapChamCong, btnLuuChamCong;
        private System.Windows.Forms.DataGridView dgvChamCong;

        // Tab 3 Controls
        private System.Windows.Forms.TextBox txtThangNamLuong;
        private System.Windows.Forms.Button btnXemBangLuong, btnChotLuong;
        private System.Windows.Forms.DataGridView dgvBangLuong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabNhanSu = new System.Windows.Forms.TabPage();
            this.tabChamCong = new System.Windows.Forms.TabPage();
            this.tabTinhLuong = new System.Windows.Forms.TabPage();

            this.txtMaNV = new System.Windows.Forms.TextBox() { ReadOnly = true };
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.txtHeSoLuong = new System.Windows.Forms.TextBox();
            this.txtTimKiemNV = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.btnThemNV = new System.Windows.Forms.Button();
            this.btnSuaNV = new System.Windows.Forms.Button();
            this.btnXoaNV = new System.Windows.Forms.Button();
            this.btnLamMoiNV = new System.Windows.Forms.Button();
            this.btnTimKiemNV = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();

            this.txtThangNamCC = new System.Windows.Forms.TextBox() { Text = "11/2026" };
            this.btnNapChamCong = new System.Windows.Forms.Button();
            this.btnLuuChamCong = new System.Windows.Forms.Button();
            this.dgvChamCong = new System.Windows.Forms.DataGridView();

            this.txtThangNamLuong = new System.Windows.Forms.TextBox() { Text = "11/2026" };
            this.btnXemBangLuong = new System.Windows.Forms.Button();
            this.btnChotLuong = new System.Windows.Forms.Button();
            this.dgvBangLuong = new System.Windows.Forms.DataGridView();

            this.SuspendLayout();

            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Controls.Add(this.tabNhanSu);
            this.tabMain.Controls.Add(this.tabChamCong);
            this.tabMain.Controls.Add(this.tabTinhLuong);

            // ================= TAB 1: HỒ SƠ NHÂN SỰ =================
            this.tabNhanSu.Text = "1. Hồ sơ Nhân sự (3-Tier CRUD)";
            this.tabNhanSu.BackColor = System.Drawing.Color.White;

            var lbl1 = new System.Windows.Forms.Label() { Text = "Mã NV:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.txtMaNV.Location = new System.Drawing.Point(110, 17); this.txtMaNV.Width = 100;

            var lbl2 = new System.Windows.Forms.Label() { Text = "Họ tên:", Location = new System.Drawing.Point(230, 20), AutoSize = true };
            this.txtHoTen.Location = new System.Drawing.Point(310, 17); this.txtHoTen.Width = 220;

            var lbl3 = new System.Windows.Forms.Label() { Text = "CCCD:", Location = new System.Drawing.Point(550, 20), AutoSize = true };
            this.txtCCCD.Location = new System.Drawing.Point(620, 17); this.txtCCCD.Width = 200;

            var lbl4 = new System.Windows.Forms.Label() { Text = "Ngày sinh:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
            this.dtpNgaySinh.Location = new System.Drawing.Point(110, 57); this.dtpNgaySinh.Width = 150; this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            var lbl5 = new System.Windows.Forms.Label() { Text = "Phòng ban:", Location = new System.Drawing.Point(280, 60), AutoSize = true };
            this.cboPhongBan.Location = new System.Drawing.Point(370, 57); this.cboPhongBan.Width = 220; this.cboPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            var lbl6 = new System.Windows.Forms.Label() { Text = "Lương CB:", Location = new System.Drawing.Point(20, 100), AutoSize = true };
            this.txtLuongCoBan.Location = new System.Drawing.Point(110, 97); this.txtLuongCoBan.Width = 150;

            var lbl7 = new System.Windows.Forms.Label() { Text = "Hệ số:", Location = new System.Drawing.Point(280, 100), AutoSize = true };
            this.txtHeSoLuong.Location = new System.Drawing.Point(370, 97); this.txtHeSoLuong.Width = 100;

            this.btnThemNV.Text = "Thêm NV"; this.btnThemNV.Location = new System.Drawing.Point(20, 140); this.btnThemNV.Size = new System.Drawing.Size(100, 35);
            this.btnThemNV.Click += new System.EventHandler(this.btnThemNV_Click);

            this.btnSuaNV.Text = "Sửa / Chuyển PB"; this.btnSuaNV.Location = new System.Drawing.Point(130, 140); this.btnSuaNV.Size = new System.Drawing.Size(140, 35);
            this.btnSuaNV.Click += new System.EventHandler(this.btnSuaNV_Click);

            this.btnXoaNV.Text = "Xóa NV"; this.btnXoaNV.Location = new System.Drawing.Point(280, 140); this.btnXoaNV.Size = new System.Drawing.Size(100, 35);
            this.btnXoaNV.Click += new System.EventHandler(this.btnXoaNV_Click);

            this.btnLamMoiNV.Text = "Làm mới"; this.btnLamMoiNV.Location = new System.Drawing.Point(390, 140); this.btnLamMoiNV.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoiNV.Click += new System.EventHandler(this.btnLamMoiNV_Click);

            this.txtTimKiemNV.Location = new System.Drawing.Point(530, 145); this.txtTimKiemNV.Width = 200;
            this.btnTimKiemNV.Text = "Tìm kiếm"; this.btnTimKiemNV.Location = new System.Drawing.Point(740, 140); this.btnTimKiemNV.Size = new System.Drawing.Size(120, 35);
            this.btnTimKiemNV.Click += new System.EventHandler(this.btnTimKiemNV_Click);

            this.dgvNhanVien.Location = new System.Drawing.Point(20, 190);
            this.dgvNhanVien.Size = new System.Drawing.Size(860, 330);
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);

            this.tabNhanSu.Controls.AddRange(new System.Windows.Forms.Control[] {
                lbl1, this.txtMaNV, lbl2, this.txtHoTen, lbl3, this.txtCCCD, lbl4, this.dtpNgaySinh,
                lbl5, this.cboPhongBan, lbl6, this.txtLuongCoBan, lbl7, this.txtHeSoLuong,
                this.btnThemNV, this.btnSuaNV, this.btnXoaNV, this.btnLamMoiNV, this.txtTimKiemNV, this.btnTimKiemNV, this.dgvNhanVien
            });

            // ================= TAB 2: CHẤM CÔNG DISCONNECTED MODEL =================
            this.tabChamCong.Text = "2. Chấm công Ngắt kết nối (Disconnected Model)";
            this.tabChamCong.BackColor = System.Drawing.Color.White;

            var lblThangCC = new System.Windows.Forms.Label() { Text = "Tháng/Năm (MM/yyyy):", Location = new System.Drawing.Point(20, 25), AutoSize = true };
            this.txtThangNamCC.Location = new System.Drawing.Point(200, 22); this.txtThangNamCC.Width = 120;

            this.btnNapChamCong.Text = "1. Nạp Bảng Chấm Công (Fill)";
            this.btnNapChamCong.Location = new System.Drawing.Point(340, 18); this.btnNapChamCong.Size = new System.Drawing.Size(230, 36);
            this.btnNapChamCong.Click += new System.EventHandler(this.btnNapChamCong_Click);

            this.btnLuuChamCong.Text = "2. Lưu Chấm Công (adapter.Update)";
            this.btnLuuChamCong.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLuuChamCong.ForeColor = System.Drawing.Color.White;
            this.btnLuuChamCong.Location = new System.Drawing.Point(590, 18); this.btnLuuChamCong.Size = new System.Drawing.Size(280, 36);
            this.btnLuuChamCong.Click += new System.EventHandler(this.btnLuuChamCong_Click);

            var lblHuongDan = new System.Windows.Forms.Label()
            {
                Text = "Hướng dẫn: Sửa trực tiếp cột [SoNgayCong] và [SoGioOT] trên lưới bên dưới rồi bấm 'Lưu Chấm Công'.",
                ForeColor = System.Drawing.Color.DarkBlue,
                Location = new System.Drawing.Point(20, 68),
                AutoSize = true
            };

            this.dgvChamCong.Location = new System.Drawing.Point(20, 100);
            this.dgvChamCong.Size = new System.Drawing.Size(860, 420);
            this.dgvChamCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabChamCong.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblThangCC, this.txtThangNamCC, this.btnNapChamCong, this.btnLuuChamCong, lblHuongDan, this.dgvChamCong
            });

            // ================= TAB 3: TÍNH LƯƠNG & CHỐT LƯƠNG =================
            this.tabTinhLuong.Text = "3. Tính Lương & Chốt Lương (SqlTransaction)";
            this.tabTinhLuong.BackColor = System.Drawing.Color.White;

            var lblThangLuong = new System.Windows.Forms.Label() { Text = "Tháng/Năm (MM/yyyy):", Location = new System.Drawing.Point(20, 25), AutoSize = true };
            this.txtThangNamLuong.Location = new System.Drawing.Point(200, 22); this.txtThangNamLuong.Width = 120;

            this.btnChotLuong.Text = "CHỐT LƯƠNG THÁNG (SqlTransaction)";
            this.btnChotLuong.BackColor = System.Drawing.Color.DarkGreen;
            this.btnChotLuong.ForeColor = System.Drawing.Color.White;
            this.btnChotLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChotLuong.Location = new System.Drawing.Point(340, 18); this.btnChotLuong.Size = new System.Drawing.Size(300, 38);
            this.btnChotLuong.Click += new System.EventHandler(this.btnChotLuong_Click);

            this.btnXemBangLuong.Text = "Xem Bảng Lương";
            this.btnXemBangLuong.Location = new System.Drawing.Point(660, 18); this.btnXemBangLuong.Size = new System.Drawing.Size(180, 38);
            this.btnXemBangLuong.Click += new System.EventHandler(this.btnXemBangLuong_Click);

            this.dgvBangLuong.Location = new System.Drawing.Point(20, 80);
            this.dgvBangLuong.Size = new System.Drawing.Size(860, 440);
            this.dgvBangLuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBangLuong.ReadOnly = true;

            this.tabTinhLuong.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblThangLuong, this.txtThangNamLuong, this.btnChotLuong, this.btnXemBangLuong, this.dgvBangLuong
            });

            // FrmHRMMain
            this.ClientSize = new System.Drawing.Size(910, 575);
            this.Controls.Add(this.tabMain);
            this.Name = "FrmHRMMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÀI 6.2 - HỆ THỐNG QUẢN LÝ NHÂN SỰ & TÍNH LƯƠNG";
            this.Load += new System.EventHandler(this.FrmHRMMain_Load);
            this.ResumeLayout(false);
        }
    }
}
