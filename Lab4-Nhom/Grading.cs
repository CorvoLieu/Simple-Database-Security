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
using System.Numerics;
using System.Diagnostics;

namespace Lab4_Nhom
{
    public partial class Grading : Form
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

        public Grading(Account user, MainMenu menu)
        {
            InitializeComponent();
            User = user;
            _menu = menu;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mssv_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string mssv, mahp, diem;

            mssv = this.mssv.Text;
            mahp = this.mahp.Text;
            diem = this.diem.Text;

            addDiem(mssv, mahp, diem, User.PublicKey);
        }

        private void addDiem(string mssv, string mahp, string diem, string publicKey)
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
                cmd.CommandText = "SP_INS_BANGDIEM";
                cmd.Parameters.AddWithValue("@MASV", mssv);
                cmd.Parameters.AddWithValue("@MAHP", mahp);
                //cmd.Parameters.AddWithValue("@DIEM", diem);
                //cmd.Parameters.AddWithValue("@PUBKEY", publicKey);
                cmd.Parameters.Add("@RESULT", SqlDbType.VarChar, -1);
                cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                cmd.Connection = connect;

                // Ecrypt diem
                var k = RSAGenerator.GenerateKeys(RSAGenerator.p, RSAGenerator.q, BigInteger.Parse(publicKey));
                byte[] diemByte = RSAGenerator.Encrypt(diem, k.e, k.n);

                // Create DIEM parameter
                SqlParameter diemParam = new SqlParameter("@DIEM", SqlDbType.VarBinary, -1);
                diemParam.Value = diemByte;
                cmd.Parameters.Add(diemParam);

                // Send to server
                cmd.ExecuteNonQuery();

                // Retrieve the value of the error message output parameter
                var errorMessage = cmd.Parameters["@RESULT"].Value;

                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    Debug.WriteLine("Parameter Name: {0}, Value: {1}", parameter.ParameterName, parameter.Value);
                }

                if (errorMessage == DBNull.Value)
                {
                    MessageBox.Show("Thêm điểm thành công", "Thông báo", MessageBoxButtons.OK);
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

        private void button20_Click(object sender, EventArgs e)
        {
            _menu.Show();
            this.Hide();
        }
    }
}
