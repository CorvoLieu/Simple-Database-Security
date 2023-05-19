using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using System.Numerics;
using System.Diagnostics;

namespace Lab4_Nhom
{
    enum OpMode
    {
        None,
        Them,
        Xoa,
        Sua
    }

    public partial class ManageStaff : Form
    {
        private Account _user;
        private MainMenu _mainMenu;
        private OpMode _currentMode;
        public ManageStaff(Account user, MainMenu mainMenu)
        {
            InitializeComponent();
            _user = user;
            _mainMenu = mainMenu;
            _currentMode = OpMode.None;
        }

        private void loadList(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SEL_ENCRYPT_NHANVIEN";
                cmd.Connection = conn;

                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataTable mydata = new DataTable();
                mydata.Columns.Add("MANV", typeof(string));
                mydata.Columns.Add("HOTEN", typeof(string));
                mydata.Columns.Add("EMAIL", typeof(string));
                mydata.Columns.Add("LUONG", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    byte[] bLuong = (byte[])row["LUONG"];
                    String Hoten = (String)row["HOTEN"];
                    String MaNV = (String)row["MANV"];
                    String Email = (String)row["EMAIL"];

                    // Decrypt Luong
                    // Get keys
                    var k = RSAGenerator.GenerateKeys(RSAGenerator.p, RSAGenerator.q, RSAGenerator.publicKeyFromSeed(MaNV));
                    String strLuong = RSAGenerator.Decrypt(bLuong, k.d, k.n);

                    DataRow drShow = mydata.NewRow();
                    drShow["MANV"] = MaNV;
                    drShow["HOTEN"] = Hoten;
                    drShow["EMAIL"] = Email;
                    drShow["LUONG"] = strLuong;
                    mydata.Rows.Add(drShow);
                }

                this.NVGridView.DataSource = mydata;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void them_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã chuyển sang chế độ Thêm Nhân Viên. Điền đầy đủ các thông tin và nhấn \'Ghi\\Lưu\'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _currentMode = OpMode.Them;
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã chuyển sang chế độ Xóa Nhân Viên. Hãy điền mã Nhân Viên cần xóa và nhấn 'Ghi\\Lưu'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _currentMode = OpMode.Xoa;
        }

        private void sua_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã chuyển sang chế độ Sửa Nhân Viên. Hãy điền mã Nhân Viên cần sửa và nội dung cần sửa. Để trống nếu không sửa dữ liệu ô đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _currentMode = OpMode.Sua;
        }

        private void luu_Click(object sender, EventArgs e)
        {
            switch (_currentMode)
            {
                case OpMode.None:
                    {
                        MessageBox.Show("Hãy chọn Thêm, Xóa hoặc Sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case OpMode.Them:
                    {
                        if (this.textBoxMaNV.Text == "" || this.textBoxHoten.Text == "" || this.textBoxEmail.Text == ""
                            || this.textBoxLuong.Text == "" || this.textBoxTDN.Text == "" || this.textBoxMK.Text == "")
                        {
                            MessageBox.Show("Hãy điền đầy đủ thông tin để Thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }

                        themNV(sender, e);
                        break;
                    }
                case OpMode.Xoa:
                    {
                        if (this.textBoxMaNV.Text == "")
                        {
                            MessageBox.Show("Hãy điền đầy mã Nhân Viên để Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        xoaNV(sender, e);
                        break;
                    }
                case OpMode.Sua:
                    {
                        if (this.textBoxMaNV.Text == "")
                        {
                            MessageBox.Show("Hãy điền đầy mã Nhân Viên để Sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        suaNV(sender, e);
                        break;
                    }
            }
            _currentMode = OpMode.None;
        }

        private void suaNV(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_UPD_NHANVIEN";
                cmd.Parameters.AddWithValue("@MANV", (this.textBoxMaNV.Text == "") ? DBNull.Value : this.textBoxMaNV.Text);
                cmd.Parameters.AddWithValue("@HOTEN", (this.textBoxHoten.Text == "") ? DBNull.Value : this.textBoxHoten.Text);
                cmd.Parameters.AddWithValue("@EMAIL", (this.textBoxEmail.Text == "") ? DBNull.Value : this.textBoxEmail.Text);
                cmd.Parameters.AddWithValue("@TENDN", (this.textBoxTDN.Text == "") ? DBNull.Value : this.textBoxTDN.Text);

                if (this.textBoxMK.Text == "")
                {
                    SqlParameter matkhauParam = new SqlParameter("@MATKHAU", SqlDbType.VarBinary, -1);
                    matkhauParam.Value = DBNull.Value;
                    cmd.Parameters.Add(matkhauParam);
                }
                else
                {
                    // Encode new password using SHA1
                    SHA1 sha = SHA1.Create();
                    byte[] inputBytes = Encoding.ASCII.GetBytes(this.textBoxMK.Text);
                    byte[] newMKBytes = sha.ComputeHash(inputBytes);

                    // Create a SqlParameter for the encrypted MATKHAU value
                    SqlParameter matkhauParam = new SqlParameter("@MATKHAU", SqlDbType.VarBinary, newMKBytes.Length);
                    matkhauParam.Value = newMKBytes;
                    cmd.Parameters.Add(matkhauParam);
                }

                if (this.textBoxLuong.Text == "")
                {
                    SqlParameter luongParam = new SqlParameter("@LUONG", SqlDbType.VarBinary, -1);
                    luongParam.Value = DBNull.Value;
                    cmd.Parameters.Add(luongParam);
                }
                else
                {
                    // Get public key
                    BigInteger exp = getPubKey(this.textBoxMaNV.Text);

                    // Encrypt new Luong to public Key
                    var k = RSAGenerator.GenerateKeys(RSAGenerator.p, RSAGenerator.q, exp);
                    byte[] newLuongBytes = RSAGenerator.Encrypt(this.textBoxLuong.Text, k.e, k.n);

                    // Create a SqlParameter for the encrypted LUONG value
                    SqlParameter luongParam = new SqlParameter("@LUONG", SqlDbType.VarBinary, -1);
                    luongParam.Value = newLuongBytes;
                    cmd.Parameters.Add(luongParam);
                }

                // Create a SqlParameter for the result value
                SqlParameter resultParam = new SqlParameter("@RESULT", SqlDbType.VarChar, 500);
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.Connection = conn;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@RESULT"].Value.ToString() != "SUCCESS")
                {
                    throw new Exception(cmd.Parameters["@RESULT"].Value.ToString());
                }

                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                khong_Click(sender, e);
                loadList(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " (line: " + new StackTrace(ex, true).GetFrame(0).GetFileLineNumber() + ") ");
            }
        }

        private void xoaNV(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_DELETE_NHANVIEN";
                cmd.Parameters.AddWithValue("@MANV", this.textBoxMaNV.Text);

                // Create a SqlParameter for the result value
                SqlParameter resultParam = new SqlParameter("@RESULT", SqlDbType.VarChar, 500);
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.Connection = conn;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@RESULT"].Value.ToString() != "SUCCESS")
                {
                    throw new Exception(cmd.Parameters["@RESULT"].Value.ToString());
                }

                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                khong_Click(sender, e);
                loadList(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void themNV(object sender, EventArgs e)
        {
            string newMaNV = this.textBoxMaNV.Text;
            string newHoTen = this.textBoxHoten.Text;
            string newEmail = this.textBoxEmail.Text;
            string newLuong = this.textBoxLuong.Text;
            string newTDN = this.textBoxTDN.Text;
            string newMK = this.textBoxMK.Text;
            string newPubKey = "";
            byte[] newMKBytes;
            byte[] newLuongBytes;

            // Encode new password using SHA1
            SHA1 sha = SHA1.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(newMK);
            newMKBytes = sha.ComputeHash(inputBytes);

            // Generate Public key
            var exp = RSAGenerator.publicKeyFromSeed(newMaNV);
            newPubKey = exp.ToString();

            // Encrypt new Luong to public Key
            var k = RSAGenerator.GenerateKeys(RSAGenerator.p, RSAGenerator.q, exp);
            newLuongBytes = RSAGenerator.Encrypt(newLuong, k.e, k.n);

            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INS_NHANVIEN";
                cmd.Parameters.AddWithValue("@MANV", newMaNV);
                cmd.Parameters.AddWithValue("@HOTEN", newHoTen);
                cmd.Parameters.AddWithValue("@EMAIL", newEmail);
                cmd.Parameters.AddWithValue("@TENDN", newTDN);
                cmd.Parameters.AddWithValue("@PUBKEY", newPubKey);

                // Create a SqlParameter for the encrypted LUONG value
                SqlParameter luongParam = new SqlParameter("@LUONG", SqlDbType.VarBinary, newLuongBytes.Length);
                luongParam.Value = newLuongBytes;
                cmd.Parameters.Add(luongParam);

                // Create a SqlParameter for the encrypted MATKHAU value
                SqlParameter matkhauParam = new SqlParameter("@MATKHAU", SqlDbType.VarBinary, newMKBytes.Length);
                matkhauParam.Value = newMKBytes;
                cmd.Parameters.Add(matkhauParam);

                // Create a SqlParameter for the result value
                SqlParameter resultParam = new SqlParameter("@RESULT", SqlDbType.VarChar, 500);
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.Connection = conn;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                khong_Click(sender, e);
                loadList(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void khong_Click(object sender, EventArgs e)
        {
            this.textBoxMaNV.Text = "";
            this.textBoxHoten.Text = "";
            this.textBoxEmail.Text = "";
            this.textBoxLuong.Text = "";
            this.textBoxTDN.Text = "";
            this.textBoxMK.Text = "";
        }

        private void thoat_Click(object sender, EventArgs e)
        {
            _mainMenu.Show();
            this.Hide();
        }

        private BigInteger getPubKey(string _maNV)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            conn.Open();

            // Get Public key
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_PUBKEY_NHANVIEN";
            cmd.Parameters.AddWithValue("@MANV", this.textBoxMaNV.Text);
            //cmd.Parameters.AddWithValue("@PUBKEY", "");
            cmd.Connection = conn;

            SqlParameter pubKeyParam = new SqlParameter("@PUBKEY", SqlDbType.VarChar, 512);
            pubKeyParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pubKeyParam);

            cmd.ExecuteNonQuery();
            //foreach (SqlParameter parameter in cmd.Parameters)
            //{
            //    Debug.WriteLine("Parameter Name: {0}, Value: {1}", parameter.ParameterName, parameter.Value);
            //}

            return BigInteger.Parse(cmd.Parameters["@PUBKEY"].Value.ToString());
        }
    }


    public static class RSAGenerator
    {
        static public readonly BigInteger p = BigInteger.Parse("94038577692241242604082056186618759782116583295015176272370802772217738793549");
        static public readonly BigInteger q = BigInteger.Parse("86027383569324492849492993149466160752011170819427622876613844272030430101901");

        public static (BigInteger e, BigInteger d, BigInteger n) GenerateKeys(BigInteger p, BigInteger q, BigInteger e)
        {
            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger d = ModInverse(e, phi);
            return (e, d, n);
        }

        public static byte[] Encrypt(string plaintext, BigInteger e, BigInteger n)
        {
            // Convert plaintext to bytes
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(plaintext);

            // Encrypt the bytes
            BigInteger m = new BigInteger(bytesToEncrypt);
            BigInteger c = BigInteger.ModPow(m, e, n);

            return c.ToByteArray();
        }

        public static string Decrypt(byte[] data, BigInteger d, BigInteger n)
        {
            // Convert the bytes into a BigInteger
            BigInteger encrypted = new BigInteger(data);

            // Decrypt the BigInteger
            BigInteger decrypted = BigInteger.ModPow(encrypted, d, n);

            // Convert the decrypted BigInteger back into a string
            return Encoding.UTF8.GetString(decrypted.ToByteArray());
        }

        public static BigInteger publicKeyFromSeed(string seed)
        {
            int result = 0;
            for (int i = 0; i < seed.Length; i++)
            {
                result += seed[i];
            }
            return new BigInteger(FindClosestPrime(result));
        }

        private static int FindClosestPrime(int value)
        {
            if (value % 2 == 0)
                value--;

            while (!IsPrime(value))
                value -= 2;

            return value;
        }

        private static bool IsPrime(int number)
        {
            if (number == 2)
                return true;
            if (number < 2 || number % 2 == 0)
                return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger t = 0, newt = 1;
            BigInteger r = n, newr = a;

            while (newr != 0)
            {
                BigInteger quotient = r / newr;
                (t, newt) = (newt, t - quotient * newt);
                (r, newr) = (newr, r - quotient * newr);
            }

            if (r > 1)
                throw new Exception("a is not invertible");

            if (t < 0)
                t += n;

            return t;
        }
    }
}
