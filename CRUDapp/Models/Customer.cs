using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDapp.Models
{
    public class Customer
    {
        #region Attributes
        private string _customerID;
        private string _companyName;
        private string _contactName;
        private string _contactTitle;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;
        private string _phone;
        private string _fax;
        #endregion

        #region Properties
        public string CustomerID { get { return _customerID; } set { _customerID = value; } }
        public string CompanyName { get { return _companyName; } set { _companyName = value; } }
        public string ContactName { get { return _contactName; } set { _contactName = value; } }
        public string ContactTitle { get { return _contactTitle; } set { _contactTitle = value; } }
        public string Address { get { return _address; } set { _address = value; } }
        public string City { get { return _city; } set { _city = value; } }
        public string Region { get { return _region; } set { _region = value; } }
        public string PostalCode { get { return _postalCode; } set { _postalCode = value; } }
        public string Country { get { return _country; } set { _country = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Fax { get { return _fax; } set { _fax = value; } }
        #endregion

        #region Constructors

        // Constructor vacío
        public Customer()
        {
            _customerID = string.Empty;
            _companyName = string.Empty;
            _contactName = string.Empty;
            _contactTitle = string.Empty;
            _address = string.Empty;
            _city = string.Empty;
            _region = string.Empty;
            _postalCode = string.Empty;
            _country = string.Empty;
            _phone = string.Empty;
            _fax = string.Empty;
        }

        // Constructor completo
        public Customer(string customerID, string companyName, string contactName, string contactTitle,
                        string address, string city, string region, string postalCode,
                        string country, string phone, string fax)
        {
            _customerID = customerID;
            _companyName = companyName;
            _contactName = contactName;
            _contactTitle = contactTitle;
            _address = address;
            _city = city;
            _region = region;
            _postalCode = postalCode;
            _country = country;
            _phone = phone;
            _fax = fax;
        }

        // Constructor por ID
        public Customer(string id)
        {
            string query = @"SELECT CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region,
                             PostalCode, Country, Phone, Fax
                             FROM Customers WHERE CustomerID = @ID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                _customerID = Convert.ToString(row["CustomerID"]);
                _companyName = Convert.ToString(row["CompanyName"]);
                _contactName = Convert.ToString(row["ContactName"]);
                _contactTitle = Convert.ToString(row["ContactTitle"]);
                _address = Convert.ToString(row["Address"]);
                _city = Convert.ToString(row["City"]);
                _region = Convert.ToString(row["Region"]);
                _postalCode = Convert.ToString(row["PostalCode"]);
                _country = Convert.ToString(row["Country"]);
                _phone = Convert.ToString(row["Phone"]);
                _fax = Convert.ToString(row["Fax"]);
            }
        }

        #endregion

        #region Methods

        public static List<Customer> GetAll()
        {
            List<Customer> list = new List<Customer>();

            string query = @"SELECT CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region,
                             PostalCode, Country, Phone, Fax FROM Customers ORDER BY CompanyName";

            SqlCommand command = new SqlCommand(query);
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Customer(
                    Convert.ToString(row["CustomerID"]),
                    Convert.ToString(row["CompanyName"]),
                    Convert.ToString(row["ContactName"]),
                    Convert.ToString(row["ContactTitle"]),
                    Convert.ToString(row["Address"]),
                    Convert.ToString(row["City"]),
                    Convert.ToString(row["Region"]),
                    Convert.ToString(row["PostalCode"]),
                    Convert.ToString(row["Country"]),
                    Convert.ToString(row["Phone"]),
                    Convert.ToString(row["Fax"])
                ));
            }

            return list;
        }

        public bool Add()
        {
            string sql = @"INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City,
                             Region, PostalCode, Country, Phone, Fax)
                           VALUES (@ID, @NAME, @CONTACTNAME, @CONTACTTITLE, @ADDRESS, @CITY, @REGION,
                                   @POSTALCODE, @COUNTRY, @PHONE, @FAX)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@ID", _customerID);
            command.Parameters.AddWithValue("@NAME", _companyName);
            command.Parameters.AddWithValue("@CONTACTNAME", _contactName);
            command.Parameters.AddWithValue("@CONTACTTITLE", _contactTitle);
            command.Parameters.AddWithValue("@ADDRESS", _address);
            command.Parameters.AddWithValue("@CITY", _city);
            command.Parameters.AddWithValue("@REGION", _region);
            command.Parameters.AddWithValue("@POSTALCODE", _postalCode);
            command.Parameters.AddWithValue("@COUNTRY", _country);
            command.Parameters.AddWithValue("@PHONE", _phone);
            command.Parameters.AddWithValue("@FAX", _fax);

            return SQLServerConnection.ExecuteNotQUery(command);
        }

        #endregion
    }
}
