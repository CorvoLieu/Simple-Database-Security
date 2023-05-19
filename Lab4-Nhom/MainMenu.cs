using Lab4_Nhom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_Nhom
{
    public partial class MainMenu : Form
    {
        private Account _user;
        public Account User
        {
            get { return _user; }
            set { _user = value; }
        }

        public MainMenu(Account user)
        {
            InitializeComponent();
            User = user;
        }

        private void ManageClasses_Click(object sender, EventArgs e)
        {
            Form form = new ManageClasses(User, this);
            form.Show();
            this.Hide();
        }
        
        private void ManageStaff_Click(object sender, EventArgs e)
        {
            Form form = new ManageStaff(User, this);
            form.Show();
            this.Hide();
        }

        private void Students_Click(object sender, EventArgs e)
        {
            StudentList form = new StudentList(User, this);
            form.Show();
            this.Hide();
        }

        private void AddGrade_Click(object sender, EventArgs e)
        {
            Grading form = new Grading(User, this);
            form.Show();
            this.Hide();
        }
    }
}
