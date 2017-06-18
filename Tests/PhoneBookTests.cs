using System;
using System.Linq;
using Chakad.Samples.PhoneBook.Commands;
using Chakad.Samples.PhoneBook.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chakad.Tests
{
    [TestClass]
    public class PhoneBookTests : PhoneBookTestBase
    {
        [TestMethod]
        public void CreateListTest()
        {
            Initializer(false);

            var contact = SendCommand(new CreateContact
            {
                FirstName = "Masoud",
                LastName = "Bahrami",
                Address = "Behbahan"
            });

            Assert.IsTrue(contact.Succeeded);

            var contactsQueryResult = RunQuery(new ContactsQuery());
            Assert.AreEqual(1, contactsQueryResult.TotalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            Assert.AreEqual(contactsQueryResult.Entities.FirstOrDefault().Id, contact.Id);

        }

        [TestMethod]
        public void UpdateListTest()
        {
            Initializer(false);

            var contact = SendCommand(new CreateContact
            {
                FirstName = "Masoud",
                LastName = "Bahrami",
                Address = "Behbahan"
            });

            Assert.IsTrue(contact.Succeeded);

            var contactsQueryResult = RunQuery(new ContactsQuery());
            Assert.AreEqual(1, contactsQueryResult.TotalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            Assert.AreEqual(contactsQueryResult.Entities.FirstOrDefault().Id, contact.Id);

            var newContact = SendCommand(new UpdateContact
            {
                Id = contact.Id,
                FirstName = "مسعود",
                LastName = "بهرامی",
                Address = "بهبهان"
            });
            Assert.AreEqual(true , newContact.Succeeded);

            var result = RunQuery(new GetContactQuery
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
        public void DeleteContact()
        {
            Initializer();

            var contactsQueryResult = RunQuery(new ContactsQuery());

            var totalCount = contactsQueryResult.TotalCount;

            Assert.AreNotEqual(1, totalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            var contact = SendCommand(new DeleteContact
            {
                Id = contactsQueryResult.Entities.FirstOrDefault().Id
            });

            Assert.IsTrue(contact.Succeeded);

            contactsQueryResult = RunQuery(new ContactsQuery());
            Assert.AreEqual(totalCount-1, contactsQueryResult.TotalCount);
        }

        [TestMethod]
        public void ContactQueryTests()
        {
            Initializer();

            var contactsQueryResult = RunQuery(new ContactsQuery());

            var totalCount = contactsQueryResult.TotalCount;

            Assert.AreNotEqual(1, totalCount);

            Assert.AreNotEqual(null, contactsQueryResult.Entities);

            var contactQueryResult = contactsQueryResult.Entities.FirstOrDefault();

            var contact = RunQuery(new GetContactQuery
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
        public void MessagePolicyTest()
        {
            Initializer(false);

            var chakadResult = SendCommand(new TestAttributeCommand());
            Assert.IsFalse(chakadResult.Succeeded);
            
            chakadResult = SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud"
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10,
                MinValue = 10
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10,
                MinValue = 10
            }, new TimeSpan(0, 0, 10, 0));
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = SendCommand(new TestAttributeCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "masoud",
                MaxValue = 10,
                MinValue = 10,
                Range =20 
            });
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = SendCommand(new TestAttributeCommand
            {
                Id = Guid.Empty,
                FirstName = "mas",
                MaxValue = 10,
                MinValue = 10,
                Range = 20
            });
            Assert.IsFalse(chakadResult.Succeeded);

            chakadResult = SendCommand(new TestAttributeCommand
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
