using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Contact
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public AddressInfo Address { get; set; }
        public List<Phone> Phone { get; set; }
        public string Email { get; set; }

        //public override bool Equals(object obj)
        //{
        //    var contact2 = obj as Contact;
        //    if (contact2 == null)
        //    {
        //        return false;
        //    }

        //    return contact2.Name == Name && 
        //        contact2.Email == Email &&
        //        contact2.Address?.City == Address?.City && contact2.Address?.State == Address?.State && contact2.Address?.Street == Address?.Street && contact2.Address?.Zip == Address?.Zip
        //        && contact2.Ph
        //}
    }

    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Middle { get; set; }
    }

    public class AddressInfo
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class Phone
    { 
        public string Number { get; set; }
        public PhoneType Type { get; set; }
    }

    public enum PhoneType
    {
        Home,
        Work,
        Mobile
    }
}
