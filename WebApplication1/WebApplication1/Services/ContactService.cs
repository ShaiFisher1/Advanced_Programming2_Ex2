using System;
using System.Collections.Generic;
namespace WebApplication1.Services
{
    public class ContactService : IContactService
    {
        private static List<Contact> contacts;

        public ContactService(List<Contact> Conlist)
        {
           contacts = Conlist;
        }

        public Contact Get(string Contactid)
        {
            return contacts.Find(x => x.id == Contactid);
        }

        public void Edit(Contact contact)
        {
            Contact temp = Get(contact.id);
            temp.lastdate = DateTime.Now.ToString();
            temp.last = contact.last;
        }

        public bool Delete(string id)
        {
            Contact c = Get(id);
            if (c != null)
            {
                contacts.Remove(c);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Contact> GetAll()
        {
            return contacts;
        }

        public void Add(string id, string nickName, string service)
        {
            Contact contact = new Contact() { id = id, last = "", lastdate = "", name = nickName, server = service };
            contacts.Add(contact);
        }
    }
}
