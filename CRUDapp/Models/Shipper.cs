using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDapp.Models
{
    public class Shipper
    {
        #region Attributes
        private int _shipperId;
        private string _companyName;
        private string _phone;
        #endregion

        #region Properties
        public int ShipperId { get { return _shipperId; } set { _shipperId = value; } }
        public string CompanyName { get { return _companyName; } set { _companyName = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        #endregion

        #region Constructors

        // Constructor vacío
        public Shipper()
        {
            _shipperId = 0;
            _companyName = string.Empty;
            _phone = string.Empty;
        }

        // Constructor completo
        public Shipper(int shipperId, string companyName, string phone)
        {
            _shipperId = shipperId;
            _companyName = companyName;
            _phone = phone;
        }

        // Constructor por ID
        public Shipper(int id)
        {
            string query = @"SELECT ShipperID, CompanyName, Phone FROM Shippers WHERE ShipperID = @ID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                _shipperId = Convert.ToInt32(row["ShipperID"]);
                _companyName = Convert.ToString(row["CompanyName"]);
                _phone = Convert.ToString(row["Phone"]);
            }
        }

        #endregion

        #region Methods

        public static List<Shipper> GetAll()
        {
            List<Shipper> list = new List<Shipper>();

            string query = @"SELECT ShipperID, CompanyName, Phone FROM Shippers ORDER BY CompanyName";

            SqlCommand command = new SqlCommand(query);
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Shipper(
                    Convert.ToInt32(row["ShipperID"]),
                    Convert.ToString(row["CompanyName"]),
                    Convert.ToString(row["Phone"])
                ));
            }

            return list;
        }

        public bool Add()
        {
            string sql = @"INSERT INTO Shippers (CompanyName, Phone) VALUES (@CompanyName, @Phone)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@CompanyName", _companyName);
            command.Parameters.AddWithValue("@Phone", _phone);

            return SQLServerConnection.ExecuteNotQUery(command);
        }

        #endregion
    }
}
