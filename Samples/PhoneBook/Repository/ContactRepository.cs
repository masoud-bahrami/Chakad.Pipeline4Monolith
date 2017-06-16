using System.Collections.Generic;
using System.Linq;
using Chakad.Samples.PhoneBook.Model;

namespace Chakad.Sample.PhoneBook.Repository
{
    public class ContactRepository : IContactRepository
    {
        private static List<Contact> _onTheFlyDataSource;
        private void GiveMeSomeContacts()
        {
            for (var i = 0; i < 30; i++)
            {
                _onTheFlyDataSource.Add(new Contact
                {
                    Id = i,
                    FirstName = "Contact ",
                    LastName = $" {i} ",
                    Address = "Address " + i
                });
            }
        }

        public ContactRepository(bool generatSampleContact = true)
        {
            _onTheFlyDataSource = new List<Contact>();

            if (generatSampleContact)
                GiveMeSomeContacts();
        }
        public List<Contact> LoadAll()
        {
            return _onTheFlyDataSource;
        }

        public Contact Get(int id)
        {
            return _onTheFlyDataSource.Find(contact => contact.Id == id);
        }

        public Contact Add(Contact contact)
        {
            _onTheFlyDataSource.Add(contact);
            return contact;
        }

        public Contact Update(Contact contact)
        {
            var cntct = _onTheFlyDataSource.FirstOrDefault(_contact => _contact.Id == contact.Id);

            if (cntct != null)
            {
                _onTheFlyDataSource.FirstOrDefault(_contact => _contact.Id == contact.Id).FirstName = contact.FirstName;
                _onTheFlyDataSource.FirstOrDefault(_contact => _contact.Id == contact.Id).LastName = contact.LastName;
                _onTheFlyDataSource.FirstOrDefault(_contact => _contact.Id == contact.Id).Address = contact.Address;
            }

            return contact;
        }

        public void Delete(Contact contact)
        {
            _onTheFlyDataSource.Remove(contact);
        }

        public int GetLastId()
        {

            return _onTheFlyDataSource.Any()?
                _onTheFlyDataSource.Max(contact => contact.Id):0;
        }
    }
}
