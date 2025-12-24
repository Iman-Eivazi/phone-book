using PhoneBook.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhoneBook.Repositories
{
    internal class PhoneBookRepository : IPhoneBookRepository
    {
        #region Variables

        const string _connectionString = "Data Source = .; Initial Catalog = PhoneBookDb; Integrated Security = True;";

        #endregion

        public DataTable GetAllContacts()
        {
            //string readQuery = ;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Contact;", connection);

                DataTable contactsList = new DataTable();

                try
                {
                    adapter.Fill(contactsList);

                    return contactsList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading contacts: {ex.Message}",
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                    return new DataTable();
                }
            }
        }
    }
}
