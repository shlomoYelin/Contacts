using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contacts.Model
{
    public class Addresse
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}