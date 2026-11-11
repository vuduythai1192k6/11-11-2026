namespace RetailSalesSystem.GUI
{
    partial class FrmSalesMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabSanPham;
        private System.Windows.Forms.TabPage tabBanHang;
        private System.Windows.Forms.TabPage tabThongKe;

        // Tab 1 Controls
        private System.Windows.Forms.TextBox txtMaSP, txtTenSP, txtGiaBan, txtSoLuongTon, txtTimKiemSP;
        private System.Windows.Forms.Button btnThemSP, btnSuaSP, btnXoaSP, btnTimKiemSP, btnLamMoiSP;
        private System.Windows.Forms.DataGridView dgvSanPham;

        // Tab 2 Controls
        private System.Windows.Forms.ComboBox cboNhanVien, cboKhachHang, cboChonSP;
        private System.Windows.Forms.NumericUpDown numSoLuongMua;
        private System.Windows.Forms.Button btnThemGioHang, btnXoaGioHang, btnThanhToan;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Label lblTongTien, lblDiemCong;

        // Tab 3 Controls
        private System.Windows.Forms.DateTimePicker dtpNgayThongKe;
        private System.Windows.Forms.Button btnXemThongKe;
        private System.Windows.Forms.Label lblDoanhThuNgay;
        private System.Windows.Forms.DataGridView dgvTop5SP;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabSanPham = new System.Windows.Forms.TabPage();
            this.tabBanHang = new System.Windows.Forms.TabPage();
            this.tabThongKe = new System.Windows.Forms.TabPage();

            // Khởi tạo Tab 1
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtSoLuongTon = new System.Windows.Forms.TextBox();
            this.txtTimKiemSP = new System.Windows.Forms.TextBox();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.btnSuaSP = new System.Windows.Forms.Button();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.btnLamMoiSP = new System.Windows.Forms.Button();
            this.btnTimKiemSP = new System.Windows.Forms.Button();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();

            // Khởi tạo Tab 2
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.cboChonSP = new System.Windows.Forms.ComboBox();
            this.numSoLuongMua = new System.Windows.Forms.NumericUpDown();
            this.btnThemGioHang = new System.Windows.Forms.Button();
            this.btnXoaGioHang = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblDiemCong = new System.Windows.Forms.Label();

            // Khởi tạo Tab 3
            this.dtpNgayThongKe = new System.Windows.Forms.DateTimePicker();
            this.btnXemThongKe = new System.Windows.Forms.Button();
            this.lblDoanhThuNgay = new System.Windows.Forms.Label();
            this.dgvTop5SP = new System.Windows.Forms.DataGridView();

            this.SuspendLayout();

            // tabMain
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Controls.Add(this.tabSanPham);
            this.tabMain.Controls.Add(this.tabBanHang);
            this.tabMain.Controls.Add(this.tabThongKe);

            // ================= TAB 1: QUẢN LÝ DANH MỤC SẢN PHẨM =================
            this.tabSanPham.Text = "1. Quản lý Sản phẩm (CRUD)";
            this.tabSanPham.BackColor = System.Drawing.Color.White;

            var lbl1 = new System.Windows.Forms.Label() { Text = "Mã SP:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.txtMaSP.Location = new System.Drawing.Point(110, 17); this.txtMaSP.Width = 150;

            var lbl2 = new System.Windows.Forms.Label() { Text = "Tên SP:", Location = new System.Drawing.Point(290, 20), AutoSize = true };
            this.txtTenSP.Location = new System.Drawing.Point(370, 17); this.txtTenSP.Width = 280;

            var lbl3 = new System.Windows.Forms.Label() { Text = "Giá bán:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
            this.txtGiaBan.Location = new System.Drawing.Point(110, 57); this.txtGiaBan.Width = 150;

            var lbl4 = new System.Windows.Forms.Label() { Text = "Tồn kho:", Location = new System.Drawing.Point(290, 60), AutoSize = true };
            this.txtSoLuongTon.Location = new System.Drawing.Point(370, 57); this.txtSoLuongTon.Width = 150;

            this.btnThemSP.Text = "Thêm SP"; this.btnThemSP.Location = new System.Drawing.Point(20, 105); this.btnThemSP.Size = new System.Drawing.Size(100, 35);
            this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);

            this.btnSuaSP.Text = "Sửa SP"; this.btnSuaSP.Location = new System.Drawing.Point(130, 105); this.btnSuaSP.Size = new System.Drawing.Size(100, 35);
            this.btnSuaSP.Click += new System.EventHandler(this.btnSuaSP_Click);

            this.btnXoaSP.Text = "Xóa SP"; this.btnXoaSP.Location = new System.Drawing.Point(240, 105); this.btnXoaSP.Size = new System.Drawing.Size(100, 35);
            this.btnXoaSP.Click += new System.EventHandler(this.btnXoaSP_Click);

            this.btnLamMoiSP.Text = "Làm mới"; this.btnLamMoiSP.Location = new System.Drawing.Point(350, 105); this.btnLamMoiSP.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoiSP.Click += new System.EventHandler(this.btnLamMoiSP_Click);

            this.txtTimKiemSP.Location = new System.Drawing.Point(520, 110); this.txtTimKiemSP.Width = 220;
            this.btnTimKiemSP.Text = "Tìm kiếm"; this.btnTimKiemSP.Location = new System.Drawing.Point(750, 105); this.btnTimKiemSP.Size = new System.Drawing.Size(110, 35);
            this.btnTimKiemSP.Click += new System.EventHandler(this.btnTimKiemSP_Click);

            this.dgvSanPham.Location = new System.Drawing.Point(20, 160);
            this.dgvSanPham.Size = new System.Drawing.Size(860, 360);
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPham_CellClick);

            this.tabSanPham.Controls.AddRange(new System.Windows.Forms.Control[] {
                lbl1, this.txtMaSP, lbl2, this.txtTenSP, lbl3, this.txtGiaBan, lbl4, this.txtSoLuongTon,
                this.btnThemSP, this.btnSuaSP, this.btnXoaSP, this.btnLamMoiSP, this.txtTimKiemSP, this.btnTimKiemSP, this.dgvSanPham
            });

            // ================= TAB 2: LẬP HÓA ĐƠN BÁN HÀNG (TRANSACTION) =================
            this.tabBanHang.Text = "2. Lập Hóa đơn Bán hàng (SqlTransaction)";
            this.tabBanHang.BackColor = System.Drawing.Color.White;

            var lblNV = new System.Windows.Forms.Label() { Text = "Nhân viên:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.cboNhanVien.Location = new System.Drawing.Point(120, 17); this.cboNhanVien.Width = 220; this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            var lblKH = new System.Windows.Forms.Label() { Text = "Khách hàng:", Location = new System.Drawing.Point(370, 20), AutoSize = true };
            this.cboKhachHang.Location = new System.Drawing.Point(480, 17); this.cboKhachHang.Width = 250; this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            var lblChonSP = new System.Windows.Forms.Label() { Text = "Chọn SP:", Location = new System.Drawing.Point(20, 65), AutoSize = true };
            this.cboChonSP.Location = new System.Drawing.Point(120, 62); this.cboChonSP.Width = 320; this.cboChonSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            var lblSL = new System.Windows.Forms.Label() { Text = "Số lượng:", Location = new System.Drawing.Point(460, 65), AutoSize = true };
            this.numSoLuongMua.Location = new System.Drawing.Point(545, 62); this.numSoLuongMua.Width = 80; this.numSoLuongMua.Minimum = 1; this.numSoLuongMua.Maximum = 1000;

            this.btnThemGioHang.Text = "Thêm vào giỏ"; this.btnThemGioHang.Location = new System.Drawing.Point(640, 58); this.btnThemGioHang.Size = new System.Drawing.Size(120, 34);
            this.btnThemGioHang.Click += new System.EventHandler(this.btnThemGioHang_Click);

            this.btnXoaGioHang.Text = "Xóa giỏ"; this.btnXoaGioHang.Location = new System.Drawing.Point(770, 58); this.btnXoaGioHang.Size = new System.Drawing.Size(100, 34);
            this.btnXoaGioHang.Click += new System.EventHandler(this.btnXoaGioHang_Click);

            this.dgvGioHang.Location = new System.Drawing.Point(20, 110);
            this.dgvGioHang.Size = new System.Drawing.Size(850, 320);
            this.dgvGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioHang.ReadOnly = true;

            this.lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Red;
            this.lblTongTien.Location = new System.Drawing.Point(20, 450);
            this.lblTongTien.AutoSize = true;

            this.lblDiemCong.Text = "Điểm tích lũy cộng thêm: +0 điểm";
            this.lblDiemCong.Location = new System.Drawing.Point(20, 485);
            this.lblDiemCong.AutoSize = true;

            this.btnThanhToan.Text = "THANH TOÁN (SqlTransaction)";
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(600, 450);
            this.btnThanhToan.Size = new System.Drawing.Size(270, 55);
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);

            this.tabBanHang.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblNV, this.cboNhanVien, lblKH, this.cboKhachHang, lblChonSP, this.cboChonSP,
                lblSL, this.numSoLuongMua, this.btnThemGioHang, this.btnXoaGioHang,
                this.dgvGioHang, this.lblTongTien, this.lblDiemCong, this.btnThanhToan
            });

            // ================= TAB 3: BÁO CÁO - THỐNG KÊ =================
            this.tabThongKe.Text = "3. Báo cáo - Thống kê";
            this.tabThongKe.BackColor = System.Drawing.Color.White;

            var lblNgay = new System.Windows.Forms.Label() { Text = "Chọn ngày:", Location = new System.Drawing.Point(20, 25), AutoSize = true };
            this.dtpNgayThongKe.Location = new System.Drawing.Point(120, 22); this.dtpNgayThongKe.Width = 200;

            this.btnXemThongKe.Text = "Xem Thống kê"; this.btnXemThongKe.Location = new System.Drawing.Point(340, 18); this.btnXemThongKe.Size = new System.Drawing.Size(140, 35);
            this.btnXemThongKe.Click += new System.EventHandler(this.btnXemThongKe_Click);

            this.lblDoanhThuNgay.Text = "Tổng doanh thu trong ngày (ExecuteScalar): 0 VNĐ";
            this.lblDoanhThuNgay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuNgay.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDoanhThuNgay.Location = new System.Drawing.Point(20, 75);
            this.lblDoanhThuNgay.AutoSize = true;

            var lblTop5 = new System.Windows.Forms.Label() { Text = "Top 5 Sản phẩm bán chạy nhất (SqlDataReader):", Location = new System.Drawing.Point(20, 120), AutoSize = true };
            this.dgvTop5SP.Location = new System.Drawing.Point(20, 150);
            this.dgvTop5SP.Size = new System.Drawing.Size(850, 350);
            this.dgvTop5SP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTop5SP.ReadOnly = true;

            this.tabThongKe.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblNgay, this.dtpNgayThongKe, this.btnXemThongKe, this.lblDoanhThuNgay, lblTop5, this.dgvTop5SP
            });

            // FrmSalesMain
            this.ClientSize = new System.Drawing.Size(910, 575);
            this.Controls.Add(this.tabMain);
            this.Name = "FrmSalesMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÀI 6.1 - HỆ THỐNG QUẢN LÝ BÁN HÀNG & SIÊU THỊ MINI";
            this.Load += new System.EventHandler(this.FrmSalesMain_Load);
            this.ResumeLayout(false);
        }
    }
}
