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
        // This is the path to our data-base
        const string _connectionString = "Data Source = .; Initial Catalog = PhoneBookDb; Integrated Security = True;";

        #endregion

        #region Methods
        public DataTable GetAllContacts()
        {
            // It represent a sql connection, so we can use it to transfer datas.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Somehow, it caries datas
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Contact;", connection);

                // Make a variable so we can keep our datas in program life-cycle
                DataTable contactsList = new DataTable();

                try
                {
                    // Fill recieved datas.
                    adapter.Fill(contactsList);

                    return contactsList;
                }
                catch (Exception ex)
                {
                    // If anything goes wrong, handle the error and show the error message.
                    MessageBox.Show($"Error loading contacts: {ex.Message}",
                                    "Database Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return new DataTable(); // return an empty table in DataTable type.
                }
            }
        }



        // some bullshit
        #endregion
    }
}
