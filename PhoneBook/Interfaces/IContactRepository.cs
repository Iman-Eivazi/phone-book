using System.Data;

namespace PhoneBook.Interfaces
{
    public interface IContactRepository
    {
        // This method use for getting all contacts at a same time; 
        DataTable GetAllContacts(); // Read all query
    }
}
