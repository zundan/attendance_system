using System;
using System.Data;
using System.Windows.Forms;
using BCrypt.Net;

namespace DesktopAttendanceSystem
{
    public partial class Login : Form
    {
        private UsersManager userManager;
        public static UserSession currentUserSession;

        public Login()
        {
            InitializeComponent();
            userManager = new UsersManager();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;

            if (LoginUser(email, password))
            {
                int userId = userManager.GetUserId(email);
                currentUserSession = new UserSession(email, userManager.GetUserRole(email), userId);
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login gagal. Periksa email dan password Anda.");
            }
        }

        private bool LoginUser(string email, string password)
        {
            bool isAuthenticated = userManager.AuthenticateUser(email, password);

            return isAuthenticated;
        }

        private void register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }

    public class UserSession
    {
        public string Email { get; private set; }
        public int UserRole { get; private set; }
        public int UserId { get; private set; }

        public UserSession(string email, int userRole, int userId)
        {
            Email = email;
            UserRole = userRole;
            UserId = userId;
        }

        public void ClearSession()
        {
            Email = null;
            UserRole = 0;
            UserId = 0; 
        }
    }


}
