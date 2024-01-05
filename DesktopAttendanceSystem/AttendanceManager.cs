using System;
using System.Data;

namespace DesktopAttendanceSystem
{
    internal class AttendanceManager
    {
        private DatabaseManager dbManager;

        public AttendanceManager()
        {
            dbManager = new DatabaseManager();
        }

        public void RecordAttendance(int userId, int eventId, string status)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $"INSERT INTO attendance (userid, eventid, status, timestamp) VALUES ({userId}, {eventId}, '{status}', '{timestamp}')";
            dbManager.ExecuteNonQuery(query);
        }

        public void UpdateAttendance(int attendanceId, int userId, int eventId, string status)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $"UPDATE attendance SET userid = {userId}, eventid = {eventId}, status = '{status}', timestamp = '{timestamp}' WHERE id = {attendanceId}";
            dbManager.ExecuteNonQuery(query);
        }

        public void DeleteAttendance(int attendanceId)
        {
            string query = $"DELETE FROM attendance WHERE id = {attendanceId}";
            dbManager.ExecuteNonQuery(query);
        }

        public DataTable GenerateAttendanceReport(int eventId)
        {
            string query = $"SELECT * FROM attendance WHERE eventid = {eventId}";
            return dbManager.ExecuteQuery(query);
        }
    }
}
