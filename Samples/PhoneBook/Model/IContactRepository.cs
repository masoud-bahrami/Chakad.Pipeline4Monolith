using System.Collections.Generic;

namespace Chakad.Samples.PhoneBook.Model
{
    public interface IContactRepository
    {
        List<Contact> LoadAll();
        Contact Get(int id);
        Contact Add(Contact contact);
        Contact Update(Contact contact);
        void Delete(Contact contact);
        int GetLastId();
    }
}
