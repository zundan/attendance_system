using System;

namespace DesktopAttendanceSystem
{
    internal class EventManager : IDisposable
    {
        private DatabaseManager dbManager;

        public EventManager()
        {
            dbManager = new DatabaseManager();
        }

        public void AddEvent(string name, int iduser, DateTime date, string location)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            string query = $"INSERT INTO events (name, iduser, date, location) VALUES ('{name}', '{iduser}', '{formattedDate}', '{location}')";
            dbManager.ExecuteNonQuery(query);
        }

        public void EditEvent(int eventId, string name, int iduser, DateTime date, string location)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            string query = $"UPDATE events SET name='{name}', iduser='{iduser}', date='{formattedDate}', location='{location}' WHERE id={eventId}";
            dbManager.ExecuteNonQuery(query);
        }

        public void RemoveEvent(int eventId)
        {
            string query = $"DELETE FROM events WHERE id = {eventId}";
            dbManager.ExecuteNonQuery(query);
        }

        public void Dispose()
        {
            dbManager.Dispose();
        }
    }
}
