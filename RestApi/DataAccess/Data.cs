using AdventureWorksModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IData
    {
        List<Contact> GetAllContacts();
        Contact GetContact(int id);
        Contact Create(Contact newContact);
        bool Update(Contact contact);
        bool Delete(int id);
        List<CallListItem> GetCallList();
    }

    /// <summary>
    /// This class is not being used and does not work because I could not figure out how to resolve a runtime error 
    /// "The type or namespace name could not be found" when calling it from the main Web App.
    /// As a workaround, I have created the MockData class that implements the same interface so that I can at least test the front end.
    /// At some point after doing the above, I decided to try LiteDB instead, so now there are 3 classes that implement the IData interface.
    /// </summary>
    public class Data : IData
    {
        /// <summary>
        /// Convert database object from generated model into the form used by the API
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private Contact CustomerToContact(Customer customer)
        {
            var address = customer.CustomerAddresses.FirstOrDefault()?.Address;
            return new Contact()
            {
                Email = customer.EmailAddress,
                Name = new Name()
                {
                    First = customer.FirstName,
                    Middle = customer.MiddleName,
                    Last = customer.LastName
                },
                Address = new AddressInfo()
                {
                    Street = address?.AddressLine1,
                    City = address?.City,
                    State = address?.StateProvince,
                    Zip = address?.PostalCode
                }
            };
        }

        private Customer ContactToCustomer(Contact newContact)
        {
            var addresses = new List<CustomerAddress>()
                {
                    new CustomerAddress()
                    {
                        Address = new Address()
                        {
                            AddressLine1 = newContact.Address.Street,
                            City = newContact.Address.City,
                            StateProvince = newContact.Address.State,
                            PostalCode = newContact.Address.Zip,
                        }
                    }
                };

            var customer = new Customer()
            {
                EmailAddress = newContact.Email,
                FirstName = newContact.Name.First,
                MiddleName = newContact.Name.Middle,
                LastName = newContact.Name.Last,
                Phone = newContact.Phone.First().Number,
                // todo: there's no column for phone type
                CustomerAddresses = addresses
            };

            return customer;
        }

        public List<Contact> GetAllContacts()
        {
            using (var dbContext = new AdventureWorksDataModel())
            {
                var customers = dbContext.Customers.ToList();
                return customers.Select(s =>
                    CustomerToContact(s)).ToList();
            }
        }

        public Contact GetContact(int id)
        {
            using (var dbContext = new AdventureWorksDataModel())
            {
                var customer = dbContext.Customers.FirstOrDefault(c => c.CustomerID == id);
                if (customer == null)
                {
                    return null;
                }
                
                return CustomerToContact(customer);
            }
        }

        public Contact Create(Contact newContact)
        {
            using (var dbContext = new AdventureWorksDataModel())
            {
                var customer = ContactToCustomer(newContact);

                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();

                return newContact;
            }
        }

        public bool Update(Contact contact)
        {
            using (var dbContext = new AdventureWorksDataModel())
            {
                var existing = dbContext.Customers.Where(c => c.EmailAddress == contact.Email).FirstOrDefault();
                if (existing != null)
                {
                    existing.EmailAddress = contact.Email;
                    existing.FirstName = contact.Name.First;
                    existing.MiddleName = contact.Name.Middle;
                    existing.LastName = contact.Name.Last;
                    existing.Phone = contact.Phone.First().Number;

                    var address = existing.CustomerAddresses.FirstOrDefault()?.Address;
                    if (address != null)
                    {
                        address.AddressLine1 = contact.Address.Street;
                        address.City = contact.Address.City;
                        address.PostalCode = contact.Address.Zip;
                        address.StateProvince = contact.Address.State;
                    }

                    dbContext.SaveChanges();

                    return true;
                }                
            }

            return false;
        }


        public bool Delete(int id)
        {
            using (var dbContext = new AdventureWorksDataModel())
            {
                var existing = dbContext.Customers.Where(c => c.CustomerID == id).FirstOrDefault();
                if (existing != null)
                {
                    dbContext.Customers.Remove(existing);
                    return true;
                }
            }

            return false;
        }

        public List<CallListItem> GetCallList()
        {
            using (var dbContext = new AdventureWorksDataModel())
            {
                var customers = dbContext.Customers
                    // todo: no way to tell if it's a home phone with this schema
                    .Where(c => c.Phone != null)
                    .ToList();
                return customers.Select(s =>
                    new CallListItem()
                    {
                        Name = new Name()
                        {
                            First = s.FirstName,
                            Middle = s.MiddleName,
                            Last = s.LastName
                        },
                        Phone = s.Phone
                    }).ToList();
            }

        }
    }
}

