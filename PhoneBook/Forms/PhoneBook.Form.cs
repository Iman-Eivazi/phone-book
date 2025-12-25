using PhoneBook.Interfaces;
using PhoneBook.Repositories;
using System;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class PhoneBookForm : Form
    {
        #region Variables
        // Dependecy Injection
        IContactRepository contact;

        #endregion

        #region Methods
        // Constructor
        public PhoneBookForm()
        {
            InitializeComponent();

            // Initialize DI
            contact = new ContactRepository();
        }

        private void PhoneBookForm_Load(object sender, EventArgs e)
        {
            LoadContacts();
        }

        private void LoadContacts()
        {
            try
            {
                // Hide ContactId column (not for display)
                contactListDgv.Columns[0].Visible = false;

                // Prevent auto-generating extra columns
                contactListDgv.AutoGenerateColumns = false;

                // Fetch data from repository and bind to DataGridView
                contactListDgv.DataSource = contact.GetAllContacts();


                // Init the property name, so every record in db will have a column
                contactListDgv.Columns[0].DataPropertyName = "ContactId";
                contactListDgv.Columns[1].DataPropertyName = "FirstName";
                contactListDgv.Columns[2].DataPropertyName = "LastName";
                contactListDgv.Columns[3].DataPropertyName = "ContactNumber";
                contactListDgv.Columns[4].DataPropertyName = "Email";
                contactListDgv.Columns[5].DataPropertyName = "GroupName";
                contactListDgv.Columns[6].DataPropertyName = "Relationship";
                contactListDgv.Columns[7].DataPropertyName = "Address";
                contactListDgv.Columns[8].DataPropertyName = "Note";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading contacts: {ex.Message}",
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void searchTextBox_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
        }


        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            try 
            {
                contactListDgv.DataSource = contact.SearchContact(searchTextBox.Text.ToLower());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}",
                                "Search Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}

// I.E