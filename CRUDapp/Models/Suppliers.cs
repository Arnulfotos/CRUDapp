using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDapp.Models
{
    public class Suppliers
    {
        #region Attributes

        private int _supplierID;
        private string _companyName;
        private string _contactTitle;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;
        private string _phone;
        private string _fax;
        private string _homePage;

        #endregion

        public int SupplierID { get => _supplierID; set => _supplierID = value; }
        public string CompanyName { get => _companyName; set => _companyName = value; }
        public string ContactTitle { get => _contactTitle; set => _contactTitle = value; }
        public string Address { get => _address; set => _address = value; }
        public string City { get => _city; set => _city = value; }
        public string Region { get => _region; set => _region = value; }
        public string PostalCode { get => _postalCode; set => _postalCode = value; }
        public string Country { get => _country; set => _country = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Fax { get => _fax; set => _fax = value; }
        public string HomePage { get => _homePage; set => _homePage = value; }




        public Suppliers()
        {
        }

        // Constructor con parámetros
        public Suppliers(int supplierID, string companyName, string contactTitle, string address,
                         string city, string region, string postalCode, string country,
                         string phone, string fax, string homePage)
        {
            _supplierID = supplierID;
            _companyName = companyName;
            _contactTitle = contactTitle;
            _address = address;
            _city = city;
            _region = region;
            _postalCode = postalCode;
            _country = country;
            _phone = phone;
            _fax = fax;
            _homePage = homePage;
        }





        public static List<Suppliers> GetAll()
        {

            List<Suppliers> list = new List<Suppliers>();

            string query = @"SELECT * FROM Suppliers Order By City";
            SqlCommand command = new SqlCommand(query);

            DataTable table = SQLServerConnection.ExecuteQuery(command);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {

                    list.Add(new Suppliers(
    Convert.ToInt32(row["SupplierID"]),
    Convert.ToString(row["CompanyName"]),
    Convert.ToString(row["ContactTitle"]),
    Convert.ToString(row["Address"]),
    Convert.ToString(row["City"]),
    Convert.ToString(row["Region"]),
    Convert.ToString(row["PostalCode"]),
    Convert.ToString(row["Country"]),
    Convert.ToString(row["Phone"]),
    Convert.ToString(row["Fax"]),
    Convert.ToString(row["HomePage"])
));




                }


            }


            return list;


        }



    }




}
