using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contacts.Model
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public Addresse adress { get; set; }

    }
}