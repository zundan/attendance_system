using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DesktopAttendanceSystem
{
    public partial class Event : Form
    {
        private DatabaseManager dbManager;
        private EventManager eventManager;
        public Event()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            eventManager = new EventManager();
            FillComboBoxWithUsers();
        }

        private void FillComboBoxWithUsers()
        {
            // Hanya menambahkan pengguna dengan peran 2 ke ComboBox
            DataTable usersTable = GetUsersWithRole2();

            // Set DisplayMember dan ValueMember pada ComboBox
            comboBox1.DisplayMember = "nama";
            comboBox1.ValueMember = "id"; // Sesuaikan dengan nama kolom ID yang benar

            // Tambahkan data ke ComboBox
            comboBox1.DataSource = usersTable;
        }

        private DataTable GetUsersWithRole2()
        {
            string query = "SELECT id, nama FROM users WHERE role = 2";
            return dbManager.ExecuteQuery(query);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;

            // Ambil nilai dari comboBox1 (asumsi comboBox1 menyimpan ID pengguna)
            DataRowView selectedUser = (DataRowView)comboBox1.SelectedItem;
            int userId = Convert.ToInt32(selectedUser["id"]); // Sesuaikan dengan nama kolom ID yang benar

            // Ambil nilai dari textBox1 dan textBox2
            string eventName = textBox1.Text;
            string eventLocation = textBox2.Text;

            // Panggil metode AddEvent dari EventManager untuk menambahkan event ke database
            if (CreateEvent(eventName, userId, selectedDate, eventLocation))
            {
                MessageBox.Show("Berhasil menambahkan event.");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal menambahkan event.");
            }

        }

        private bool CreateEvent(string eventName, int userId, DateTime selectedDate, string eventLocation)
        {
            try
            {
                eventManager.AddEvent(eventName, userId, selectedDate, eventLocation);
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
            DateTime selectedDate = dateTimePicker1.Value;
            DataRowView selectedUser = (DataRowView)comboBox1.SelectedItem;
            int userId = Convert.ToInt32(selectedUser["id"]);

            string eventName = textBox1.Text;
            string eventLocation = textBox2.Text;
            int eventId = Convert.ToInt32(textBox3.Text);

            if (UpdateEvent(eventId, eventName, userId, selectedDate, eventLocation))
            {
                MessageBox.Show("Berhasil mengubah event.");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal mengubah event.");
            }
        }
        private bool UpdateEvent(int eventId, string eventName, int userId, DateTime selectedDate, string eventLocation)
        {
            try
            {
                eventManager.EditEvent(eventId, eventName, userId, selectedDate, eventLocation);
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
            string query = "SELECT e.id, e.name, u.nama as nama_operator, e.date, e.location FROM events e JOIN users u ON e.iduser = u.id";
            DataTable userData = dbManager.ExecuteQuery(query);

            // Binding data ke DataGridView
            dataGridViewEvents.DataSource = userData;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            displayData();
        }

        private bool DeleteEvent(int idEvent)
        {
            try
            {
                eventManager.RemoveEvent(idEvent);
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
            int idEvent = Convert.ToInt32(textBox3.Text);

            if (DeleteEvent(idEvent))
            {
                MessageBox.Show("Berhasil menghapus event.");
                textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("Gagal menghapus event.");
            }
        }
    }
}
