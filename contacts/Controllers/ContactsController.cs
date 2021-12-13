using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using contacts.Model;
using contacts.Services;

namespace contacts.Controllers
{
    public class ContactsController : ApiController
    {
        // GET: api/Contacts
        public string Get()
        {
            Dal dal = new Dal();
            string res = dal.GetAllContacts();
            dal.Conn.Close();
            return res;
        }

        // GET: api/Contacts/5
        public string Get(int id)
        {
            Dal dal = new Dal();
            string res = dal.GetContact(id);
            dal.Conn.Close();
            return res;
        }

        // POST: api/Contacts
        public int Post([FromBody] Contact contact)
        {
            Dal dal = new Dal();

            int addressId = dal.ManageAddress(contact.adress);
            contact.adress.Id = addressId;

            int NewId = dal.AddContact(contact);
            dal.Conn.Close();
            return NewId;
        }

        // PUT: api/Contacts/5
        public void Put(int id, [FromBody] Contact contact)
        {
            Dal dal = new Dal();

            int addressId = dal.ManageAddress(contact.adress);
            contact.adress.Id = addressId;

            dal.UpdateContact(contact);
            dal.Conn.Close();
        }

        // DELETE: api/Contacts/5
        public void Delete(int id)
        {
            Dal dal = new Dal();
            dal.DeleteContact(id);
            dal.Conn.Close();
        }
    }
}
