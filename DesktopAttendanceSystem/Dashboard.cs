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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            SetButtonVisibility(Login.currentUserSession.UserRole);
        }

       private void SetButtonVisibility(int userRole)
        {
            // Setel visibilitas tombol berdasarkan peran pengguna
            switch (userRole)
            {
                case 1: // Administrator
                    btnUsers.Visible = true;
                    btnAttendance.Visible = true;
                    btnEvent.Visible = true;
                    break;
                case 2: // Operator/Instructor
                    btnUsers.Visible = false;
                    btnAttendance.Visible = true;
                    btnEvent.Visible = true;
                    break;
                case 3: // Participant
                    btnUsers.Visible = false;
                    btnAttendance.Visible = true;
                    btnEvent.Visible = false;
                    break;
                default:
                    // Setel nilai default sesuai kebutuhan
                    btnUsers.Visible = false;
                    btnAttendance.Visible = false;
                    btnEvent.Visible = false;
                    break;
            }
        }


        private void usersBtn(object sender, EventArgs e)
        {
            Users user = new Users();
            user.Show();
        }

        private void attendanceBtn(object sender, EventArgs e)
        {
            Attendance attend = new Attendance();
            attend.Show();
        }

        private void eventBtn(object sender, EventArgs e)
        {
            Event @event = new Event(); 
            @event.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login.currentUserSession.ClearSession();

            Login loginForm = new Login();
            loginForm.Show();

            this.Close();
        }
    }
}
