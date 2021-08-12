using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// This is a third data class that I started on because I couldn't get the EF model one to work 
    /// and I wanted to have more than just MockData class
    /// </summary>
    public class LiteDBData : IData
    {
        private string FilePath;
        public const string TableName = "contacts";

        public LiteDBData(string path)
        {
            FilePath = path;
        }

        public Contact Create(Contact newContact)
        {
            using (var db = new LiteDatabase(FilePath))
            {
                var table = db.GetCollection<Contact>(TableName);
                table.Insert(newContact);
            }
            return newContact;
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(FilePath))
            {
                var table = db.GetCollection<Contact>(TableName);
                table.Delete(id);
            }

            return true;
        }

        /// <summary>
        /// This is used for testing and probably should not be called from anywhere else
        /// </summary>
        internal void DeleteAll()
        {
            using (var db = new LiteDatabase(FilePath))
            {
                db.DropCollection(TableName);
            }
        }

        public List<Contact> GetAllContacts()
        {
            using (var db = new LiteDatabase(FilePath))
            {
                var table = db.GetCollection<Contact>(TableName);
                return table.Query().ToList();
            }
        }

        public List<CallListItem> GetCallList()
        {
            using (var db = new LiteDatabase(FilePath))
            {
                var table = db.GetCollection<Contact>(TableName);
                var allContacts = table.Query()
                    .ToList()
                    // the extra ToList is because LiteDB doesn't like the Any() clause
                    .Where(c => c.Phone.Any(p => PhoneType.Home == p.Type))
                    .ToList();

                var callList = allContacts.Select(s => new CallListItem()
                {
                    Name = s.Name,
                    Phone = s.Phone.Where(p => p.Type == PhoneType.Home)
                        .Select(n => n.Number).FirstOrDefault()
                })
                .ToList();

                return callList;
            }
        }

        public Contact GetContact(int id)
        {
            using (var db = new LiteDatabase(FilePath))
            {
                var table = db.GetCollection<Contact>(TableName);
                return table.Query().
                    Where(c => c.Id == id)
                    .FirstOrDefault();
            }
        }

        public bool Update(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
