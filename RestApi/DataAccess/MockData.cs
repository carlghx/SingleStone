using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// This is a mock testing class that I'm using since I can't get the EF data access to work
    /// </summary>
    public class MockData : IData
    {
        public Contact Create(Contact newContact)
        {
            return newContact;
        }

        public bool Delete(int id)
        {
            return true;
        }

        public List<Contact> GetAllContacts()
        {
            return new List<Contact>()
            {
                new Contact()
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
                },

                new Contact()
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
                }
            };
        }

        public Contact GetContact(int id)
        { 
            if (id == 1)
            {
                return new Contact()
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
            }

            return null;
        }

        public List<CallListItem> GetCallList()
        {
            return new List<CallListItem>()
            {
                new CallListItem()
                {
                    Phone = "123-4567",
                    Name = new Name()
                    {
                        First = "N1",
                        Middle = "M1",
                        Last = "L1"
                    }
                },
                new CallListItem()
                {
                    Phone = "440-5372",
                    Name = new Name()
                    {
                        First = "N2",
                        Middle = "M2",
                        Last = "L2"
                    }
                }
            };
        }

        public bool Update(Contact contact)
        {
            return true;
        }
    }
}
