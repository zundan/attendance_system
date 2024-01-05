using System;
using System.Data;
using System.Windows.Forms;
using BCrypt.Net;

namespace DesktopAttendanceSystem
{
    internal class UsersManager : IDisposable
    {
        private DatabaseManager dbManager;

        public UsersManager()
        {
            dbManager = new DatabaseManager();
        }

        public bool AuthenticateUser(string email, string password)
        {
            string query = $"SELECT * FROM users WHERE email='{email}'";
            DataTable result = dbManager.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                string hashedPasswordFromDatabase = result.Rows[0]["password"].ToString();

                bool isAuthenticated = BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDatabase);

                if (!isAuthenticated)
                {
                    MessageBox.Show($"Authentication failed. Email: {email}, Password: {password}, Hashed Password: {hashedPasswordFromDatabase}");
                }

                return isAuthenticated;
            }

            return false;
        }



        public void AddUser(string nama, string email, string password, string role)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            string query = $"INSERT INTO users (nama, email, password, role) VALUES ('{nama}', '{email}', '{hashedPassword}', '{role}')";
            dbManager.ExecuteNonQuery(query);
        }

        public void UpdateUser(int userId, string newRole)
        {
            string query = $"UPDATE users SET role = '{newRole}' WHERE id = {userId}";
            dbManager.ExecuteNonQuery(query);
        }


        public void RemoveUser(int userId)
        {
            string query = $"DELETE FROM Users WHERE ID = {userId}";
            dbManager.ExecuteNonQuery(query);
        }

        public int GetUserRole(string email)
        {
            string query = $"SELECT role FROM users WHERE email = '{email}'";
            DataTable result = dbManager.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0]["role"]);
            }

            return 0; 
        }

        public bool IsEmailAvailable(string email)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE email = '{email}'";
            int userCount = Convert.ToInt32(dbManager.ExecuteQuery(query).Rows[0][0]);

            return userCount == 0;
        }

        public int GetUserId(string email)
        {
            string query = $"SELECT id FROM users WHERE email = '{email}'";
            DataTable result = dbManager.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0]["id"]);
            }

            return 0;
        }

        public void Dispose()
        {
            dbManager.Dispose();
        }


    }
}
