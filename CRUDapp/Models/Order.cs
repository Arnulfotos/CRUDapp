using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDapp.Models
{
    public class Order
    {
        #region Attributes

        private int _orderID;
        private Customer _customer;
        private Employee _employee;
        private DateTime? _orderDate;
        private DateTime? _requiredDate;
        private DateTime? _shippedDate;
        private Shipper _shipVia;
        private decimal _freight;
        private string _shipName;
        private string _shipAddress;
        private string _shipCity;
        private string _shipRegion;
        private string _shipPostalCode;
        private string _shipCountry;

        #endregion

        #region Properties

        public int OrderID { get => _orderID; set => _orderID = value; }
        public Customer Customer { get => _customer; set => _customer = value; }
        public Employee Employee { get => _employee; set => _employee = value; }
        public DateTime? OrderDate { get => _orderDate; set => _orderDate = value; }
        public DateTime? RequiredDate { get => _requiredDate; set => _requiredDate = value; }
        public DateTime? ShippedDate { get => _shippedDate; set => _shippedDate = value; }
        public Shipper ShipVia { get => _shipVia; set => _shipVia = value; }
        public decimal Freight { get => _freight; set => _freight = value; }
        public string ShipName { get => _shipName; set => _shipName = value; }
        public string ShipAddress { get => _shipAddress; set => _shipAddress = value; }
        public string ShipCity { get => _shipCity; set => _shipCity = value; }
        public string ShipRegion { get => _shipRegion; set => _shipRegion = value; }
        public string ShipPostalCode { get => _shipPostalCode; set => _shipPostalCode = value; }
        public string ShipCountry { get => _shipCountry; set => _shipCountry = value; }

        #endregion

        #region Constructors

        public Order()
        {
            _orderID = 0;
            _customer = null;
            _employee = null;
            _orderDate = null;
            _requiredDate = null;
            _shippedDate = null;
            _shipVia = null;
            _freight = 0;
            _shipName = "";
            _shipAddress = "";
            _shipCity = "";
            _shipRegion = "";
            _shipPostalCode = "";
            _shipCountry = "";
        }

        public Order(int id)
        {
            string query = @"SELECT * FROM Orders WHERE OrderID = @ID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                _orderID = Convert.ToInt32(row["OrderID"]);
                _customer = row["CustomerID"] == DBNull.Value ? null : new Customer(Convert.ToString(row["CustomerID"]));
                _employee = row["EmployeeID"] == DBNull.Value ? null : new Employee(Convert.ToInt32(row["EmployeeID"]));
                _orderDate = row["OrderDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["OrderDate"]);
                _requiredDate = row["RequiredDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["RequiredDate"]);
                _shippedDate = row["ShippedDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["ShippedDate"]);
                _shipVia = row["ShipVia"] == DBNull.Value ? null : new Shipper(Convert.ToInt32(row["ShipVia"]));
                _freight = row["Freight"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Freight"]);
                _shipName = Convert.ToString(row["ShipName"]);
                _shipAddress = Convert.ToString(row["ShipAddress"]);
                _shipCity = Convert.ToString(row["ShipCity"]);
                _shipRegion = Convert.ToString(row["ShipRegion"]);
                _shipPostalCode = Convert.ToString(row["ShipPostalCode"]);
                _shipCountry = Convert.ToString(row["ShipCountry"]);
            }
        }

        public Order(int orderID, Customer customer, Employee employee, DateTime? orderDate,
            DateTime? requiredDate, DateTime? shippedDate, Shipper shipVia, decimal freight,
            string shipName, string shipAddress, string shipCity, string shipRegion,
            string shipPostalCode, string shipCountry)
        {
            _orderID = orderID;
            _customer = customer;
            _employee = employee;
            _orderDate = orderDate;
            _requiredDate = requiredDate;
            _shippedDate = shippedDate;
            _shipVia = shipVia;
            _freight = freight;
            _shipName = shipName;
            _shipAddress = shipAddress;
            _shipCity = shipCity;
            _shipRegion = shipRegion;
            _shipPostalCode = shipPostalCode;
            _shipCountry = shipCountry;
        }

        #endregion

        #region Methods

        public static List<Order> GetAll()
        {
            List<Order> list = new List<Order>();

            string query = @"SELECT * FROM Orders ORDER BY OrderDate DESC";
            SqlCommand command = new SqlCommand(query);
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Order(
                    Convert.ToInt32(row["OrderID"]),
                    row["CustomerID"] == DBNull.Value ? null : new Customer(Convert.ToString(row["CustomerID"])),
                    row["EmployeeID"] == DBNull.Value ? null : new Employee(Convert.ToInt32(row["EmployeeID"])),
                    row["OrderDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["OrderDate"]),
                    row["RequiredDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["RequiredDate"]),
                    row["ShippedDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["ShippedDate"]),
                    row["ShipVia"] == DBNull.Value ? null : new Shipper(Convert.ToInt32(row["ShipVia"])),
                    row["Freight"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Freight"]),
                    Convert.ToString(row["ShipName"]),
                    Convert.ToString(row["ShipAddress"]),
                    Convert.ToString(row["ShipCity"]),
                    Convert.ToString(row["ShipRegion"]),
                    Convert.ToString(row["ShipPostalCode"]),
                    Convert.ToString(row["ShipCountry"])
                ));
            }

            return list;
        }

        public bool Add()
        {
            string sql = @"INSERT INTO Orders (CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate,
                                                ShipVia, Freight, ShipName, ShipAddress, ShipCity,
                                                ShipRegion, ShipPostalCode, ShipCountry)
                           VALUES (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate,
                                   @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity,
                                   @ShipRegion, @ShipPostalCode, @ShipCountry)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@CustomerID", _customer != null ? _customer.CustomerID : (object)DBNull.Value);
            command.Parameters.AddWithValue("@EmployeeID", _employee != null ? _employee.EmployeeID : (object)DBNull.Value);
            command.Parameters.AddWithValue("@OrderDate", _orderDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@RequiredDate", _requiredDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShippedDate", _shippedDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipVia", _shipVia != null ? _shipVia.ShipperId : (object)DBNull.Value);
            command.Parameters.AddWithValue("@Freight", _freight);
            command.Parameters.AddWithValue("@ShipName", _shipName);
            command.Parameters.AddWithValue("@ShipAddress", _shipAddress);
            command.Parameters.AddWithValue("@ShipCity", _shipCity);
            command.Parameters.AddWithValue("@ShipRegion", _shipRegion);
            command.Parameters.AddWithValue("@ShipPostalCode", _shipPostalCode);
            command.Parameters.AddWithValue("@ShipCountry", _shipCountry);

            return SQLServerConnection.ExecuteNotQUery(command);
        }

        #endregion
    }
}
