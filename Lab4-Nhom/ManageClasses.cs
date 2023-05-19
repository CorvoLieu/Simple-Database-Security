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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lab4_Nhom
{
    public partial class ManageClasses : Form
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

        public ManageClasses(Account user, MainMenu menu)
        {
            InitializeComponent();
            User = user;
            PreId = "";
            _menu = menu;
        }


        private void Manage_Load(object sender, EventArgs e)
        {
            // Call the stored procedure and populate the ListBox
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            // Connection string for SQL Server
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            // Query to call the stored procedure
            string query = "SP_SHOW_LOP";

            // Create a new SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void displayContent(string id, string name, string emp)
        {
            try
            {
                this.malop.Text = id;
                this.tenlop.Text = name;
                this.nhanvien.Text = emp;
                this.malop.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addClasses(string id, string name, string emp)
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
                cmd.CommandText = "SP_INS_CLASSES";
                cmd.Parameters.AddWithValue("@MALOP", id);
                cmd.Parameters.AddWithValue("@TENLOP", name);
                cmd.Parameters.AddWithValue("@MANV", emp);
                cmd.Parameters.AddWithValue("@RESULT", "");
                cmd.Connection = connect;

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                string errorMessage = cmd.Parameters["@RESULT"].Value.ToString();

                if(errorMessage != null)
                {
                    MessageBox.Show("Thêm lớp thành công", "Thông báo", MessageBoxButtons.OK);
                    PopulateListBox();
                }
                else
                {
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeClasses(string id)
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
                cmd.CommandText = "SP_DEL_CLASSES";
                cmd.Parameters.AddWithValue("@MALOP", id);
                cmd.Parameters.AddWithValue("@RESULT", "");
                cmd.Connection = connect;

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                var errorMessage = cmd.Parameters["@RESULT"].Value;

                if (errorMessage != DBNull.Value)
                {
                    MessageBox.Show("Xoá lớp thành công", "Thông báo", MessageBoxButtons.OK);
                    PopulateListBox();
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

        private void alterClasses(string prev_id, string id, string name, string emp)
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
                cmd.CommandText = "SP_ALT_CLASSES";
                cmd.Parameters.AddWithValue("@MALOP_OLD", prev_id);
                cmd.Parameters.AddWithValue("@MALOP", id);
                cmd.Parameters.AddWithValue("@TENLOP", name);
                cmd.Parameters.AddWithValue("@MANV", emp);
                cmd.Parameters.AddWithValue("@RESULT", "");
                cmd.Connection = connect;

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                var errorMessage = cmd.Parameters["@RESULT"].Value;

                if (errorMessage != DBNull.Value)
                {
                    MessageBox.Show("Sửa lớp thành công", "Thông báo", MessageBoxButtons.OK);
                    PopulateListBox();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string id, name, emp;

            id = this.malop.Text;
            name = this.tenlop.Text;
            emp = this.nhanvien.Text;

            addClasses(id, name, emp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id;

            id = this.malop.Text;

            removeClasses(id);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    string malop = selectedRow.Cells["Mã lớp"].Value.ToString();
                    string tenlop = selectedRow.Cells["Tên lớp"].Value.ToString();
                    string nhanvien = selectedRow.Cells["Mã nhân viên"].Value.ToString();
                    PreId = malop;

                    displayContent(malop, tenlop, nhanvien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id, name, emp;

            id = this.malop.Text;
            name = this.tenlop.Text;
            emp = this.nhanvien.Text;

            alterClasses(PreId, id, name, emp);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _menu.Show();
            this.Hide();
        }
    }
}
