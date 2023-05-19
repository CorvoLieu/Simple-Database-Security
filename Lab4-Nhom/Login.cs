using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

namespace Lab4_Nhom
{ 
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection();
            try
            {
                // Connect to SQL server
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                connect.Open();

                // Create new login command
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_LOGIN";
                cmd.Parameters.AddWithValue("@USERNAME", textBox1.Text);
                cmd.Connection = connect;

                // Convert input string to byte by encode SHA1
                byte[] passByte;
                SHA1 sha = SHA1.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(textBox2.Text);
                passByte = sha.ComputeHash(inputBytes);

                // Create a SqlParameter for the encrypted PASSWORD value
                SqlParameter matkhauParam = new SqlParameter("@PASSWORD", SqlDbType.VarBinary, passByte.Length);
                matkhauParam.Value = passByte;
                cmd.Parameters.Add(matkhauParam);

                // Add output parameter
                SqlParameter pubkey = new SqlParameter("@PUBLICKEY", SqlDbType.VarChar, 20);
                pubkey.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pubkey);
                // Add output parameter
                pubkey = new SqlParameter("@MANV", SqlDbType.VarChar, 20);
                pubkey.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pubkey);
                // Add output parameter
                pubkey = new SqlParameter("@RESULT", SqlDbType.VarChar, 500);
                pubkey.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pubkey);

                // Send to server
                cmd.ExecuteNonQuery();

                string acc = cmd.Parameters["@RESULT"].Value.ToString();

                // Process response
                if (acc == "NONE")
                {
                    MessageBox.Show("Tài khoản không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Chào mừng Nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Account user = new Account(cmd.Parameters["@PUBLICKEY"].Value.ToString(), cmd.Parameters["@MANV"].Value.ToString());

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        Debug.WriteLine("Parameter Name: {0}, Value: {1}", parameter.ParameterName, parameter.Value);
                    }

                    MainMenu mainMenu = new MainMenu(user);
                    mainMenu.Show();
                    this.Hide();
                }
                connect.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class Account
    {
        private string _publicKey;
        public string PublicKey
        {
            get { return _publicKey; }
            set { _publicKey = value; }
        }

        private string _emp_id;
        public string EmpId
        {
            get { return _emp_id; }
            set { _emp_id = value; }
        }

        public Account(string key, string id)
        {
            _publicKey = key;
            _emp_id = id;
        }
    }
}