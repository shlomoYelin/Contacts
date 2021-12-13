using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using contacts.Model;
using Newtonsoft.Json;

namespace contacts.Services
{
    public class Dal
    {
        public string ConnStr { get; set; }
        public SqlConnection Conn { get; set; }
        public SqlCommand Cmd { get; set; }

        public Dal()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["dbContact"].ConnectionString;
            Conn = new SqlConnection();
            Conn.ConnectionString = ConnStr;
            Conn.Open();
            Cmd = new SqlCommand();
            Cmd.Connection = Conn;

        }

        public string GetAllContacts()
        {
            string Query =
                "SELECT *, Contacts.Id as ContactID FROM Contacts" +
                " inner join Addresses on" +
                " Contacts.AdressId=Addresses.Id";

            Cmd.CommandText = Query;
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            return JsonConvert.SerializeObject(Dt);
        }

        public string GetContact(int ContactId)
        {
            string Query = 
                "SELECT * FROM Contacts inner join" +
                " Addresses on Contacts.AdressId=Addresses.Id " +
                "where Contacts.Id=" + ContactId;
            Cmd.CommandText = Query;
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            return JsonConvert.SerializeObject(Dt);
        }

        public int AddContact(Contact contact)
        {
            string Query = "Insert into[Contacts] (FirstName, LastName, Tel, AdressId) " +
                "values(N'" + contact.FirstName + "'," +
                "N'"+contact.LastName+"'," +
                "'"+contact.Tel+"'," +
                ""+contact.adress.Id + ")"+
                " select @@IDENTITY";
            Cmd.CommandText = Query;
            return Convert.ToInt32(Cmd.ExecuteScalar());
        }

        public void UpdateContact(Contact contact)
        {
            string Query = "update [Contacts] set " +
                "FirstName=N'" + contact.FirstName + "'," +
                "LastName=N'" + contact.LastName + "'," +
                "Tel='" + contact.Tel + "'," +
                "AdressId=" + contact.adress.Id +
                " where Id="+contact.Id;
            Cmd.CommandText = Query;
            Cmd.ExecuteNonQuery();
        }

        public void DeleteContact(int ContactId)
        {
            string Query = "DELETE FROM [Contacts] WHERE Id=" + ContactId;
            Cmd.CommandText = Query;
            Cmd.ExecuteNonQuery();
        }

       
        public int ManageAddress(Addresse addresse)
        {
            int addressesId = addresse.Id;
            string Query = "select  * from Addresses where Id=" + addresse.Id;
            Cmd.CommandText = Query;
            SqlDataReader Dr = Cmd.ExecuteReader();
            if(Dr.Read()) 
            {
                 Query = "update [Addresses] set " +
                    "CityName=N'" + addresse.CityName + "',"+
                    "StreetName=N'" + addresse.StreetName + "'," +
                    " HouseNumber=" + addresse.HouseNumber + "," +
                    " ApartmentNumber=" + addresse.ApartmentNumber;
                Cmd.CommandText = Query;
                Conn.Close();
                Conn.Open();
                Cmd.ExecuteNonQuery();
            }
            else
            {
                Conn.Close();
                Conn.Open();
                Query = "Insert into[Addresses] (CityName, StreetName, HouseNumber, ApartmentNumber) " +
               "values(N'" + addresse.CityName + "'," +
               "N'" + addresse.StreetName + "'," +
               "" + addresse.HouseNumber + "," +
               "" + addresse.ApartmentNumber + ") " +
               "select @@IDENTITY";
                Cmd.CommandText = Query;
               addressesId =Convert.ToInt32(Cmd.ExecuteScalar());
            }
            //Conn.Close();
            return addressesId;
        }

        public SqlDataReader ExecuteReader(string Query)
        {
            Cmd.CommandText = Query;
            return Cmd.ExecuteReader();
        }
       
        public void ExecuteNonQuery(string Query)
        {
            Cmd.CommandText = Query;
            Cmd.ExecuteNonQuery();
        }

       
        public DataTable DataTable(string Query)
        {
            Cmd.CommandText = Query;
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            return Dt;
        }


        public object ExecuteScalar(string Query)
        {
            Cmd.CommandText = Query;
            return Cmd.ExecuteScalar();
        }

    }
}


