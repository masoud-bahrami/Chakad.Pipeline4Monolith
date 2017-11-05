using System;
using System.Linq;
using System.Threading.Tasks;
using Chakad.Samples.PhoneBook.Commands;
using Chakad.Samples.PhoneBook.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chakad.Tests
{
    [TestClass]
    public class PhoneBookTests : PhoneBookTestBase
    {
        [TestMethod]
        public async Task CreateListTest()
        {
            Initializer(false);

            var contact = await SendCommand(new CreateContact
            {
                FirstName = "Masoud",
                LastName = "Bahrami",
                Address = "Behbahan"
            });

            Assert.IsTrue(contact.Succeeded);

            var contactsQueryResult =await RunQuery(new ContactsQuery());
            Assert.AreEqual(31, contactsQueryResult.TotalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            Assert.IsTrue(contactsQueryResult.Entities.Select(result => result.Id).Contains(contact.Id));

        }

        [TestMethod]
        public async Task UpdateListTest()
        {
            Initializer(false);

            var contact = await SendCommand(new CreateContact
            {
                FirstName = "Masoud",
                LastName = "Bahrami",
                Address = "Behbahan"
            });

            Assert.IsTrue(contact.Succeeded);

            var contactsQueryResult =await RunQuery(new ContactsQuery());
            Assert.AreEqual(31, contactsQueryResult.TotalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            Assert.AreEqual(contactsQueryResult.Entities.LastOrDefault().Id, contact.Id);

            var newContact =await SendCommand(new UpdateContact
            {
                Id = contact.Id,
                FirstName = "مسعود",
                LastName = "بهرامی",
                Address = "بهبهان"
            });
            Assert.AreEqual(true , newContact.Succeeded);

            var result =await RunQuery(new GetContactQuery
            {
                Id = newContact.Id
            });

            Assert.AreNotEqual(null, result.Entity);

            Assert.AreEqual(newContact.Id , result.Entity.Id);
            Assert.AreEqual("مسعود", result.Entity.FirstName);
            Assert.AreEqual("بهرامی" , result.Entity.LastName);
            Assert.AreEqual("بهبهان" , result.Entity.Address);
        }

        [TestMethod]
        public async Task DeleteContact()
        {
            Initializer();

            var contactsQueryResult =await RunQuery(new ContactsQuery());

            var totalCount = contactsQueryResult.TotalCount;

            Assert.AreNotEqual(29, totalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            var contact =await SendCommand(new DeleteContact
            {
                Id = contactsQueryResult.Entities.FirstOrDefault().Id
            });

            Assert.IsTrue(contact.Succeeded);

            contactsQueryResult =await RunQuery(new ContactsQuery());
            Assert.AreEqual(totalCount-1, contactsQueryResult.TotalCount);
        }

        [TestMethod]
        public async Task ContactQueryTests()
        {
            Initializer();

            var contactsQueryResult = await RunQuery(new ContactsQuery());

            var totalCount = contactsQueryResult.TotalCount;

            Assert.AreEqual(30, totalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            var contactQueryResult = contactsQueryResult.Entities.FirstOrDefault();

            var contact =await RunQuery(new GetContactQuery
            {
                Id = contactQueryResult.Id
            });
            
            Assert.IsNotNull(contact.Entity);

            Assert.AreEqual(contactQueryResult.Id , contact.Entity.Id);
            Assert.AreEqual(contactQueryResult.FirstName , contact.Entity.FirstName);
            Assert.AreEqual(contactQueryResult.LastName , contact.Entity.LastName);
            Assert.AreEqual(contactQueryResult.Address , contact.Entity.Address);
        }
        [TestMethod]
        public async Task MessagePolicyTest()
        {
            Initializer(false);

            var chakadResult =await SendCommand(new TestAttributeCommand());
            Assert.IsFalse(chakadResult.Succeeded);
            
            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud"
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10,
                MinValue = 10
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10,
                MinValue = 10
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10,
                MinValue = 10,
                Range =20 
            });
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.Empty,
                FirstName = "mas",
                MaxValue = 10,
                MinValue = 10,
                Range = 20
            });
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = await SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "mas",
                MaxValue = 10,
                MinValue = 10,
                Range = 20
            });
            Assert.IsTrue(chakadResult.Succeeded);
        }
    }
}
