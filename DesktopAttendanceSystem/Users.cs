using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DesktopAttendanceSystem
{
    public partial class Users : Form
    {
        private DatabaseManager dbManager;
        private UsersManager userManager;
        public Users()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            userManager = new UsersManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nama = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text.Trim();
            string konfPassword = textBox4.Text.Trim();
            string role = textBox5.Text;

            if (password != konfPassword)
            {
                MessageBox.Show("Password dan konfirmasi password tidak cocok");
            }
            else if (CreateUser(nama, email, password, role))
            {
                MessageBox.Show("Berhasil menambahkan akun.");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal menambahkan akun.");
            }
        }

        private bool CreateUser(string nama, string email, string password, string role)
        {
            try
            {
                if (IsEmailAvailable(email))
                {
                    // Tambahkan user baru
                    userManager.AddUser(nama, email, password, role);
                    return true;
                }
                else
                {
                    MessageBox.Show("Email sudah digunakan. Silakan gunakan email lain.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private bool IsEmailAvailable(string email)
        {
            UsersManager userManager = new UsersManager();
            // Cek apakah email sudah digunakan
            return userManager.IsEmailAvailable(email);
        }

        public void displayData()
        {
            string query = "SELECT ID, nama, email, role FROM users";
            DataTable userData = dbManager.ExecuteQuery(query);

            // Binding data ke DataGridView
            dataGridViewUsers.DataSource = userData;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            displayData();
        }

        private bool UpdateUser(int idUser, string role)
        {
            try
            {
                userManager.UpdateUser(idUser, role);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(textBox6.Text);
            string role = textBox5.Text;

            if (UpdateUser(idUser, role))
            {
                MessageBox.Show("Berhasil mengedit akun.");
                textBox6.Text = "";
                textBox5.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal mengedit akun.");
            }

        }

        private bool DeleteUser(int idUser)
        {
            try
            {
                userManager.RemoveUser(idUser);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(textBox6.Text);

            if (DeleteUser(idUser))
            {
                MessageBox.Show("Berhasil menghapus akun.");
                textBox6.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal menghapus akun.");
            }
        }
    }
}
