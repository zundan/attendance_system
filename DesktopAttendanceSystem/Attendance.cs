using DesktopAttendanceSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DesktopAttendanceSystem
{
    public partial class Attendance : Form
    {
        public static UserSession currentUserSession;
        private DatabaseManager dbManager;
        private AttendanceManager attendanceManager;
        public Attendance()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            attendanceManager = new AttendanceManager();

            FillComboBoxWithUsers();
            FillComboBoxWithEvents();
        }

        private void FillComboBoxWithUsers()
        {
            DataTable usersTable = GetUsersWithRole();

            comboBox1.DisplayMember = "nama";
            comboBox1.ValueMember = "id"; 

            comboBox1.DataSource = usersTable;
        }

        private DataTable GetUsersWithRole()
        {
            string query = "SELECT id, nama FROM users WHERE role = 3";
            return dbManager.ExecuteQuery(query);
        }

        private void FillComboBoxWithEvents()
        {
            DataTable usersTable = GetEvents();


            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";

            comboBox2.DataSource = usersTable;
        }

        private DataTable GetEvents()
        {
            int currentUserRole = Login.currentUserSession.UserRole;
            if (currentUserRole == 1)
            {
                string query1 = "SELECT id, name FROM events";
                return dbManager.ExecuteQuery(query1);
            } else if(currentUserRole == 2)
            {
                int currentUserId = Login.currentUserSession.UserId;
                string query2 = $"SELECT id, name FROM events WHERE iduser = {currentUserId}";
                return dbManager.ExecuteQuery(query2);
            }

            return new DataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRowView selectedUser = (DataRowView)comboBox1.SelectedItem;
            int userId = Convert.ToInt32(selectedUser["id"]);
            DataRowView selectedEvent = (DataRowView)comboBox2.SelectedItem;
            int eventId = Convert.ToInt32(selectedEvent["id"]);
            string status = GetSelectedStatus();

            if (CreateAttendance(userId, eventId, status))
            {
                MessageBox.Show("Berhasil menambahkan attendance.");
            }
            else
            {
                MessageBox.Show("Gagal menambahkan attendance.");
            }
            
        }

        private string GetSelectedStatus()
        {
            if (radioButton1.Checked)
                return "Hadir";
            else if (radioButton2.Checked)
                return "Izin";
            else if (radioButton3.Checked)
                return "Status";
            else if (radioButton4.Checked)
                return "Absen";
            else
                return "";  
        }

        private bool CreateAttendance(int userId, int eventId, string status)
        {
            try
            {
                attendanceManager.RecordAttendance(userId, eventId, status);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public void displayData()
        {
            int currentUserRole = Login.currentUserSession.UserRole;
            string query;

            if (currentUserRole == 1)
            {
                query = "SELECT attendance.id, users.nama AS UserName, events.name AS EventName, attendance.status, attendance.timestamp " +
                    "FROM attendance " +
                    "INNER JOIN users ON attendance.userid = users.id " +
                    "INNER JOIN events ON attendance.eventid = events.id";
            }
            else if (currentUserRole == 2)
            {
                int currentUserId = Login.currentUserSession.UserId;

                // Add WHERE clause to filter by currentUserId
                query = $"SELECT attendance.id, users.nama AS UserName, events.name AS EventName, attendance.status, attendance.timestamp " +
                        $"FROM attendance " +
                        $"INNER JOIN users ON attendance.userid = users.id " +
                        $"INNER JOIN events ON attendance.eventid = events.id " +
                        $"WHERE events.iduser = {currentUserId}";
            }
            else
            {
                // Handle other roles or scenarios as needed
                return;
            }

            DataTable attendanceData = dbManager.ExecuteQuery(query);

            // Binding data ke DataGridView
            dataGridViewAttendance.DataSource = attendanceData;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            displayData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRowView selectedUser = (DataRowView)comboBox1.SelectedItem;
            int userId = Convert.ToInt32(selectedUser["id"]);
            DataRowView selectedEvent = (DataRowView)comboBox2.SelectedItem;
            int eventId = Convert.ToInt32(selectedEvent["id"]);
            string status = GetSelectedStatus();
            int attendId = Convert.ToInt32(textBox1.Text);

            if (UpdateAttendance(attendId, userId, eventId, status))
            {
                MessageBox.Show("Berhasil mengedit attendance.");
            }
            else
            {
                MessageBox.Show("Gagal mengedit attendance.");
            }
        }

        private bool UpdateAttendance(int attendId, int userId, int eventId, string status)
        {
            try
            {
                attendanceManager.UpdateAttendance(attendId, userId, eventId, status);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private bool DeleteAttendance(int idAttendance)
        {
            try
            {
                attendanceManager.DeleteAttendance(idAttendance);
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
            int idAttendance = Convert.ToInt32(textBox1.Text);

            if (DeleteAttendance(idAttendance))
            {
                MessageBox.Show("Berhasil menghapus attendance.");
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal menghapus attendance.");
            }
        }
    }
}

