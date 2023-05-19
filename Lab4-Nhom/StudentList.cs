using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using System.Security.Cryptography;


namespace Lab4_Nhom
{
    public partial class StudentList : Form
    {
        private Account _user;
        public Account User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _pre_id;
        public string PreId
        {
            get { return _pre_id; }
            set { _pre_id = value; }
        }

        private MainMenu _menu;
        private string _malop;

        public StudentList(Account user, MainMenu menu)
        {
            InitializeComponent();
            User = user;
            _menu = menu;
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            PopulateClassList();
        }

        private void PopulateClassList()
        {
            try
            {
                // Connect to SQL server
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                connect.Open();

                // Create new login command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SHOW_LOP_PHUTRACH";
                //cmd.Parameters.AddWithValue("@MANV", User.EmpId);
                SqlParameter manvParam = new SqlParameter("@MANV", SqlDbType.VarChar, 20);
                manvParam.Value = User.EmpId;
                cmd.Parameters.Add(manvParam);

                cmd.Connection = connect;

                // Send to server
                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateStudentList(string classId)
        {
            try
            {
                // Connect to SQL server
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                connect.Open();

                // Create new login command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SHOW_STUDENT_LIST";
                cmd.Parameters.AddWithValue("@MALOP", classId);
                cmd.Connection = connect;

                // Send to server
                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);
                dataGridView2.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    _malop = selectedRow.Cells["Mã lớp"].Value.ToString();

                    PopulateStudentList(_malop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void displayContent(string mssv, string hoten, string ngaysinh, string diachi, string tendn)
        {
            try
            {
                this.mssv.Text = mssv;
                this.hoten.Text = hoten;
                this.ngaysinh.Text = ngaysinh;
                this.diachi.Text = diachi;
                this.tendn.Text = tendn;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                    string mssv = selectedRow.Cells["MSSV"].Value.ToString();
                    string hoten = selectedRow.Cells["Họ tên"].Value.ToString();
                    string ngaysinh = selectedRow.Cells["Ngày sinh"].Value.ToString();
                    string diachi = selectedRow.Cells["Địa chỉ"].Value.ToString();
                    string tendn = selectedRow.Cells["Tên đăng nhập"].Value.ToString();
                    PreId = mssv;

                    displayContent(mssv, hoten, ngaysinh, diachi, tendn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mssv = this.mssv.Text;
            string hoten = this.hoten.Text;
            string ngaysinh = this.ngaysinh.Text;
            string diachi = this.diachi.Text;
            string tendn = this.tendn.Text;
            string matkhau = this.matkhau.Text;

            addStudent(mssv, hoten, ngaysinh, diachi, tendn, matkhau);
        }

        private void addStudent(string mssv, string hoten, string ngaysinh, string diachi, string tendn, string matkhau)
        {
            try
            {
                // Connect to SQL server
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                connect.Open();

                // Create new login command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INS_SINHVIEN";
                cmd.Parameters.AddWithValue("@MASV", mssv);
                cmd.Parameters.AddWithValue("@HOTEN", hoten);
                cmd.Parameters.AddWithValue("@NGAYSINH", ngaysinh);
                cmd.Parameters.AddWithValue("@MALOP", _malop);
                cmd.Parameters.AddWithValue("@DIACHI", diachi);
                cmd.Parameters.AddWithValue("@TENDN", tendn);
                //cmd.Parameters.AddWithValue("@MATKHAU", matkhau);
                cmd.Parameters.Add("@RESULT", SqlDbType.VarChar, -1);
                cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                cmd.Connection = connect;

                byte[] matkhauBytes;

                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(matkhau);
                    matkhauBytes = md5.ComputeHash(inputBytes);
                }

                SqlParameter matkhauParam = new SqlParameter("@MATKHAU", SqlDbType.VarBinary, -1);
                matkhauParam.Value = matkhauBytes;
                cmd.Parameters.Add(matkhauParam);

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                var errorMessage = cmd.Parameters["@RESULT"].Value;

                if (errorMessage == DBNull.Value)
                {
                    MessageBox.Show("Thêm sinh viên thành công", "Thông báo", MessageBoxButtons.OK);
                    PopulateStudentList(_malop);
                }
                else
                {
                    throw new Exception(errorMessage.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mssv = this.mssv.Text;

            removeStudent(mssv);
        }

        private void removeStudent(string mssv)
        {
            try
            {
                // Connect to SQL server
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                connect.Open();

                // Create new login command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_DEL_SINHVIEN";
                cmd.Parameters.AddWithValue("@MASV", mssv);
                cmd.Parameters.AddWithValue("@RESULT", "");
                cmd.Connection = connect;

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                var errorMessage = cmd.Parameters["@RESULT"].Value;

                if (errorMessage != DBNull.Value)
                {
                    MessageBox.Show("Xoá sinh viên thành công", "Thông báo", MessageBoxButtons.OK);
                    PopulateStudentList(_malop);
                }
                else
                {
                    throw new Exception(errorMessage.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _menu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mssv = this.mssv.Text;
            string hoten = this.hoten.Text;
            string ngaysinh = this.ngaysinh.Text;
            string diachi = this.diachi.Text;
            string tendn = this.tendn.Text;
            string matkhau = this.matkhau.Text;

            editStudent(mssv, hoten, ngaysinh, diachi, tendn, matkhau);
        }

        private void editStudent(string mssv, string hoten, string ngaysinh, string diachi, string tendn, string matkhau)
        {
            try
            {
                // Connect to SQL server
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                connect.Open();

                // Create new login command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ALT_SINHVIEN";
                cmd.Parameters.Add("@MASV_OLD", SqlDbType.VarChar, 20).Value = PreId;
                cmd.Parameters.Add("@MASV", SqlDbType.VarChar, 20).Value = mssv;
                cmd.Parameters.Add("@HOTEN", SqlDbType.VarChar, 100).Value = hoten;
                cmd.Parameters.Add("@NGAYSINH", SqlDbType.DateTime).Value = ngaysinh;
                cmd.Parameters.Add("@MALOP", SqlDbType.VarChar, 20).Value = _malop;
                cmd.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 200).Value = diachi;
                cmd.Parameters.Add("@TENDN", SqlDbType.NVarChar, 100).Value = tendn;
                cmd.Parameters.Add("@RESULT", SqlDbType.VarChar, -1);
                cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                cmd.Connection = connect;

                SqlParameter matkhauParam = new SqlParameter("@MATKHAU", SqlDbType.VarBinary, -1);
                if (matkhau == "")
                {
                    matkhauParam.Value = DBNull.Value;
                }
                else
                {
                    byte[] matkhauBytes;
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] inputBytes = Encoding.UTF8.GetBytes(matkhau);
                        matkhauBytes = md5.ComputeHash(inputBytes);
                    }

                    matkhauParam.Value = matkhauBytes;
                }
                cmd.Parameters.Add(matkhauParam);

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                var errorMessage = cmd.Parameters["@RESULT"].Value;

                if (errorMessage == DBNull.Value)
                {
                    MessageBox.Show("Sửa sinh viên thành công", "Thông báo", MessageBoxButtons.OK);
                    PopulateStudentList(_malop);
                }
                else
                {
                    throw new Exception(errorMessage.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
