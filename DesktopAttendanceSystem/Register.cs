using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAttendanceSystem
{
    public partial class Register : Form
    {
        private DatabaseManager dbManager;
        private UsersManager userManager;
        public Register()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            userManager = new UsersManager();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string nama = textNama.Text;
            string email = textEmail.Text;
            string password = textPassword.Text.Trim();
            string konfPassword = textKonfPassword.Text.Trim();

            if (password != konfPassword)
            {
                MessageBox.Show("Password dan Konfirmasi Password tidak cocok");
            }
            else if (RegisterUser(nama, email, password))
            {
                MessageBox.Show("Registrasi berhasil! Silakan login.");
            }
            else
            {
                MessageBox.Show("Registrasi gagal. Periksa kembali informasi registrasi Anda.");
            }
        }
        private bool RegisterUser(string nama, string email, string password)
        {
            try
            {

                if (IsEmailAvailable(email))
                {
                    // Tambahkan user baru
                    userManager.AddUser(nama, email, password, "3"); // Sesuaikan dengan nilai default role
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
            // Cek apakah email sudah digunakan
            return userManager.IsEmailAvailable(email);
        }

    }
}
