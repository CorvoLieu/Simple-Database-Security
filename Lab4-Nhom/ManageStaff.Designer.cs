namespace Lab4_Nhom
{
    partial class ManageStaff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sceneLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxMK = new System.Windows.Forms.TextBox();
            this.textBoxTDN = new System.Windows.Forms.TextBox();
            this.mk = new System.Windows.Forms.Label();
            this.tdn = new System.Windows.Forms.Label();
            this.textBoxLuong = new System.Windows.Forms.TextBox();
            this.luong = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxHoten = new System.Windows.Forms.TextBox();
            this.email = new System.Windows.Forms.Label();
            this.hoten = new System.Windows.Forms.Label();
            this.textBoxMaNV = new System.Windows.Forms.TextBox();
            this.manv = new System.Windows.Forms.Label();
            this.listNV = new System.Windows.Forms.GroupBox();
            this.thoat = new System.Windows.Forms.Button();
            this.khong = new System.Windows.Forms.Button();
            this.luu = new System.Windows.Forms.Button();
            this.sua = new System.Windows.Forms.Button();
            this.xoa = new System.Windows.Forms.Button();
            this.them = new System.Windows.Forms.Button();
            this.NVGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.listNV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NVGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // sceneLabel
            // 
            this.sceneLabel.AutoSize = true;
            this.sceneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sceneLabel.Location = new System.Drawing.Point(256, 18);
            this.sceneLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sceneLabel.Name = "sceneLabel";
            this.sceneLabel.Size = new System.Drawing.Size(276, 26);
            this.sceneLabel.TabIndex = 0;
            this.sceneLabel.Text = "DANH MỤC NHÂN VIÊN";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxMK);
            this.groupBox1.Controls.Add(this.textBoxTDN);
            this.groupBox1.Controls.Add(this.mk);
            this.groupBox1.Controls.Add(this.tdn);
            this.groupBox1.Controls.Add(this.textBoxLuong);
            this.groupBox1.Controls.Add(this.luong);
            this.groupBox1.Controls.Add(this.textBoxEmail);
            this.groupBox1.Controls.Add(this.textBoxHoten);
            this.groupBox1.Controls.Add(this.email);
            this.groupBox1.Controls.Add(this.hoten);
            this.groupBox1.Controls.Add(this.textBoxMaNV);
            this.groupBox1.Controls.Add(this.manv);
            this.groupBox1.Location = new System.Drawing.Point(14, 59);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(789, 173);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin nhân viên";
            // 
            // textBoxMK
            // 
            this.textBoxMK.Location = new System.Drawing.Point(448, 130);
            this.textBoxMK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxMK.Name = "textBoxMK";
            this.textBoxMK.PasswordChar = '*';
            this.textBoxMK.Size = new System.Drawing.Size(324, 23);
            this.textBoxMK.TabIndex = 1;
            // 
            // textBoxTDN
            // 
            this.textBoxTDN.Location = new System.Drawing.Point(120, 130);
            this.textBoxTDN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTDN.Name = "textBoxTDN";
            this.textBoxTDN.Size = new System.Drawing.Size(226, 23);
            this.textBoxTDN.TabIndex = 1;
            // 
            // mk
            // 
            this.mk.AutoSize = true;
            this.mk.Location = new System.Drawing.Point(370, 135);
            this.mk.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mk.Name = "mk";
            this.mk.Size = new System.Drawing.Size(57, 15);
            this.mk.TabIndex = 0;
            this.mk.Text = "Mật khẩu";
            // 
            // tdn
            // 
            this.tdn.AutoSize = true;
            this.tdn.Location = new System.Drawing.Point(19, 134);
            this.tdn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tdn.Name = "tdn";
            this.tdn.Size = new System.Drawing.Size(85, 15);
            this.tdn.TabIndex = 0;
            this.tdn.Text = "Tên đăng nhập";
            this.tdn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxLuong
            // 
            this.textBoxLuong.Location = new System.Drawing.Point(448, 80);
            this.textBoxLuong.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxLuong.Name = "textBoxLuong";
            this.textBoxLuong.Size = new System.Drawing.Size(324, 23);
            this.textBoxLuong.TabIndex = 1;
            // 
            // luong
            // 
            this.luong.AutoSize = true;
            this.luong.Location = new System.Drawing.Point(370, 84);
            this.luong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.luong.Name = "luong";
            this.luong.Size = new System.Drawing.Size(41, 15);
            this.luong.TabIndex = 0;
            this.luong.Text = "Lương";
            this.luong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(120, 80);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(226, 23);
            this.textBoxEmail.TabIndex = 1;
            // 
            // textBoxHoten
            // 
            this.textBoxHoten.Location = new System.Drawing.Point(448, 29);
            this.textBoxHoten.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxHoten.Name = "textBoxHoten";
            this.textBoxHoten.Size = new System.Drawing.Size(324, 23);
            this.textBoxHoten.TabIndex = 1;
            // 
            // email
            // 
            this.email.AutoSize = true;
            this.email.Location = new System.Drawing.Point(19, 83);
            this.email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(36, 15);
            this.email.TabIndex = 0;
            this.email.Text = "Email";
            this.email.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hoten
            // 
            this.hoten.AutoSize = true;
            this.hoten.Location = new System.Drawing.Point(370, 33);
            this.hoten.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.hoten.Name = "hoten";
            this.hoten.Size = new System.Drawing.Size(43, 15);
            this.hoten.TabIndex = 0;
            this.hoten.Text = "Họ tên";
            this.hoten.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMaNV
            // 
            this.textBoxMaNV.Location = new System.Drawing.Point(120, 29);
            this.textBoxMaNV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxMaNV.Name = "textBoxMaNV";
            this.textBoxMaNV.Size = new System.Drawing.Size(226, 23);
            this.textBoxMaNV.TabIndex = 1;
            // 
            // manv
            // 
            this.manv.AutoSize = true;
            this.manv.Location = new System.Drawing.Point(19, 32);
            this.manv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.manv.Name = "manv";
            this.manv.Size = new System.Drawing.Size(43, 15);
            this.manv.TabIndex = 0;
            this.manv.Text = "Mã NV";
            this.manv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listNV
            // 
            this.listNV.Controls.Add(this.thoat);
            this.listNV.Controls.Add(this.khong);
            this.listNV.Controls.Add(this.luu);
            this.listNV.Controls.Add(this.sua);
            this.listNV.Controls.Add(this.xoa);
            this.listNV.Controls.Add(this.them);
            this.listNV.Controls.Add(this.NVGridView);
            this.listNV.Location = new System.Drawing.Point(14, 239);
            this.listNV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listNV.Name = "listNV";
            this.listNV.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listNV.Size = new System.Drawing.Size(789, 267);
            this.listNV.TabIndex = 2;
            this.listNV.TabStop = false;
            // 
            // thoat
            // 
            this.thoat.Location = new System.Drawing.Point(684, 215);
            this.thoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.thoat.Name = "thoat";
            this.thoat.Size = new System.Drawing.Size(88, 27);
            this.thoat.TabIndex = 1;
            this.thoat.Text = "Thoát";
            this.thoat.UseVisualStyleBackColor = true;
            this.thoat.Click += new System.EventHandler(this.thoat_Click);
            // 
            // khong
            // 
            this.khong.Location = new System.Drawing.Point(548, 215);
            this.khong.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.khong.Name = "khong";
            this.khong.Size = new System.Drawing.Size(88, 27);
            this.khong.TabIndex = 1;
            this.khong.Text = "Không";
            this.khong.UseVisualStyleBackColor = true;
            this.khong.Click += new System.EventHandler(this.khong_Click);
            // 
            // luu
            // 
            this.luu.Location = new System.Drawing.Point(413, 215);
            this.luu.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.luu.Name = "luu";
            this.luu.Size = new System.Drawing.Size(88, 27);
            this.luu.TabIndex = 1;
            this.luu.Text = "Ghi/Lưu";
            this.luu.UseVisualStyleBackColor = true;
            this.luu.Click += new System.EventHandler(this.luu_Click);
            // 
            // sua
            // 
            this.sua.Location = new System.Drawing.Point(278, 215);
            this.sua.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sua.Name = "sua";
            this.sua.Size = new System.Drawing.Size(88, 27);
            this.sua.TabIndex = 1;
            this.sua.Text = "Sửa";
            this.sua.UseVisualStyleBackColor = true;
            this.sua.Click += new System.EventHandler(this.sua_Click);
            // 
            // xoa
            // 
            this.xoa.Location = new System.Drawing.Point(142, 215);
            this.xoa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.xoa.Name = "xoa";
            this.xoa.Size = new System.Drawing.Size(88, 27);
            this.xoa.TabIndex = 1;
            this.xoa.Text = "Xóa";
            this.xoa.UseVisualStyleBackColor = true;
            this.xoa.Click += new System.EventHandler(this.xoa_Click);
            // 
            // them
            // 
            this.them.Location = new System.Drawing.Point(7, 215);
            this.them.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.them.Name = "them";
            this.them.Size = new System.Drawing.Size(88, 27);
            this.them.TabIndex = 1;
            this.them.Text = "Thêm";
            this.them.UseVisualStyleBackColor = true;
            this.them.Click += new System.EventHandler(this.them_Click);
            // 
            // NVGridView
            // 
            this.NVGridView.AllowUserToAddRows = false;
            this.NVGridView.AllowUserToDeleteRows = false;
            this.NVGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NVGridView.Location = new System.Drawing.Point(7, 14);
            this.NVGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NVGridView.Name = "NVGridView";
            this.NVGridView.ReadOnly = true;
            this.NVGridView.Size = new System.Drawing.Size(775, 183);
            this.NVGridView.TabIndex = 0;
            // 
            // ManageStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 519);
            this.Controls.Add(this.listNV);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sceneLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ManageStaff";
            this.Text = "DSNV";
            this.Load += new System.EventHandler(this.loadList);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.listNV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NVGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sceneLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label manv;
        private System.Windows.Forms.TextBox textBoxMK;
        private System.Windows.Forms.TextBox textBoxTDN;
        private System.Windows.Forms.Label mk;
        private System.Windows.Forms.Label tdn;
        private System.Windows.Forms.TextBox textBoxLuong;
        private System.Windows.Forms.Label luong;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxHoten;
        private System.Windows.Forms.Label email;
        private System.Windows.Forms.Label hoten;
        private System.Windows.Forms.TextBox textBoxMaNV;
        private System.Windows.Forms.GroupBox listNV;
        private System.Windows.Forms.DataGridView NVGridView;
        private System.Windows.Forms.Button thoat;
        private System.Windows.Forms.Button khong;
        private System.Windows.Forms.Button luu;
        private System.Windows.Forms.Button sua;
        private System.Windows.Forms.Button xoa;
        private System.Windows.Forms.Button them;
    }
}