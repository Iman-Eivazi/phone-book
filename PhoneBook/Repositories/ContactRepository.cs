using PhoneBook.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhoneBook.Repositories
{
    internal class ContactRepository : IContactRepository
    {
        #region Variables
        // This is the path to our data-base
        const string _connectionString = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = PhoneBookDb; Integrated Security = True;";

        #endregion

        #region Methods
        // Get all of the contacts and represent them as a DataTable
        public DataTable GetAllContacts()
        {
            DataTable contactsList = new DataTable();

            // It represent a sql connection, so we can use it to transfer datas.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Somehow, it caries datas
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Contacts;", connection);

                // Make a variable so we can keep our datas in program life-cycle

                try
                {
                    // Fill recieved datas.
                    adapter.Fill(contactsList);

                    return contactsList;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Database Error: {sqlEx.Message}\n\nConnection String: {_connectionString}",
                                    "SQL Server Connection Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return new DataTable();
                }
                catch (Exception ex)
                {
                    // If anything goes wrong, handle the error and show the error message.
                    MessageBox.Show($"Error while loading contacts: {ex.Message}",
                                    "Database Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return new DataTable(); // return an empty table in DataTable type.
                }
            }
        }

        // It provides a searching method by conditional read query in sql
        // It gets a parameter which it may contains everything such as name, numner, email, etc...
        public DataTable SearchContact(string parameter)
        {
            string readQuery = "SELECT * FROM Contacts " +
                               "WHERE FirstName LIKE @parameter " +
                               "OR LastName LIKE @parameter " +
                               "OR Email LIKE @parameter " +
                               "OR ContactNumber LIKE @parameter " +
                               "OR GroupName LIKE @parameter " +
                               "OR Address LIKE @parameter " +
                               "OR Relationship LIKE @parameter " +
                               "OR Note LIKE @parameter";

            DataTable searchedConact = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(readQuery, connection);

                adapter.SelectCommand.Parameters.AddWithValue("@parameter", $"%{parameter}%");

                try
                {
                    adapter.Fill(searchedConact);

                    return searchedConact;  
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error searching contacts: {ex.Message}",
                                    "Database Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return new DataTable();
                }
            }
        }


        // "SELECT ContactId, FirstName, LastName, Email, " +
        // "ContactNumber, GroupName, Note, Relationship " +
        // "FROM Contacts " +
        // some bullshit
        #endregion
    }
}
