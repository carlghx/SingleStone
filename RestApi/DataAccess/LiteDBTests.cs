using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    [TestFixture]
    public class LiteDBTests
    {
        private LiteDBData Data()
        {
            return new LiteDBData(@"C:\Temp\NUnitUseOnly.db");
        }

        [SetUp]
        public void Setup()
        {
            Data().DeleteAll();
        }

        private Contact TestContact1 = new Contact()
        {
            Phone = new List<Phone>()
            {
                new Phone()
                {
                    Number = "123-4567",
                    Type = PhoneType.Home
                }
            },
            Name = new Name()
            {
                First = "N1",
                Middle = "M1",
                Last = "L1"
            },
            Email = "test@gmail.com",
            Address = new AddressInfo()
            {
                Street = "Address1",
                City = "City1",
                State = "VA",
                Zip = "22901"
            }
        };
        private Contact TestContact2 = new Contact()
        {
            Phone = new List<Phone>()
            {
                new Phone()
                {
                    Number = "789-1001",
                    Type = PhoneType.Mobile
                }
            },
            Name = new Name()
            {
                First = "N2",
                Middle = "M2",
                Last = "L2"
            },
            Email = "test@hotmail.com",
            Address = new AddressInfo()
            {
                Street = "Address2",
                City = "City2",
                State = "VA",
                Zip = "22902"
            }
        };


        [Test]
        public void TestCreate1()
        {
            Data().Create(TestContact1);
            var firstContact = Data().GetAllContacts().FirstOrDefault();
            Assert.IsNotNull(firstContact);
            firstContact.Should().BeEquivalentTo(TestContact1);

            var contactById = Data().GetContact(1);
            Assert.IsNotNull(contactById);
            contactById.Should().BeEquivalentTo(TestContact1);

            Data().Delete(1);
            var contactAfterDelete = Data().GetContact(1);
            Assert.IsNull(contactAfterDelete);
        }

        [Test]
        public void TestCreate2()
        {
            Data().Create(TestContact1);
            Data().Create(TestContact2);
            var contacts = Data().GetAllContacts();
            Assert.AreEqual(2, contacts.Count);

            contacts.First().Should().BeEquivalentTo(TestContact1);
        }

        [Test]
        public void TestCallList()
        {
            Data().Create(TestContact1);
            Data().Create(TestContact2);
            var callItems  = Data().GetCallList();
            Assert.AreEqual(1, callItems.Count);

            Assert.AreEqual("123-4567", callItems.First().Phone);
        }
    }

}
