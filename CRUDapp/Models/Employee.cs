using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDapp.Models
{
    public class Employee
    {
        #region Attributes

        private int _employeeID;
        private string _lastName;
        private string _firstName;
        private string _title;
        private string _titleOfCourtesy;
        private DateTime? _birthDate;
        private DateTime? _hireDate;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;
        private string _homePhone;
        private string _extension;
        private byte[] _photo;
        private string _notes;
        private Employee _reportsTo;
        private string _photoPath;

        #endregion

        #region Properties

        public int EmployeeID { get => _employeeID; set => _employeeID = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string Title { get => _title; set => _title = value; }
        public string TitleOfCourtesy { get => _titleOfCourtesy; set => _titleOfCourtesy = value; }
        public DateTime? BirthDate { get => _birthDate; set => _birthDate = value; }
        public DateTime? HireDate { get => _hireDate; set => _hireDate = value; }
        public string Address { get => _address; set => _address = value; }
        public string City { get => _city; set => _city = value; }
        public string Region { get => _region; set => _region = value; }
        public string PostalCode { get => _postalCode; set => _postalCode = value; }
        public string Country { get => _country; set => _country = value; }
        public string HomePhone { get => _homePhone; set => _homePhone = value; }
        public string Extension { get => _extension; set => _extension = value; }
        public byte[] Photo { get => _photo; set => _photo = value; }
        public string Notes { get => _notes; set => _notes = value; }
        public Employee ReportsTo { get => _reportsTo; set => _reportsTo = value; }
        public string PhotoPath { get => _photoPath; set => _photoPath = value; }

        #endregion

        #region Constructors

        public Employee()
        {
            _employeeID = 0;
            _lastName = "";
            _firstName = "";
            _title = "";
            _titleOfCourtesy = "";
            _birthDate = null;
            _hireDate = null;
            _address = "";
            _city = "";
            _region = "";
            _postalCode = "";
            _country = "";
            _homePhone = "";
            _extension = "";
            _photo = null;
            _notes = "";
            _reportsTo = null;
            _photoPath = "";
        }

        public Employee(int employeeID, string lastName, string firstName, string title, string titleOfCourtesy,
            DateTime? birthDate, DateTime? hireDate, string address, string city, string region,
            string postalCode, string country, string homePhone, string extension,
            byte[] photo, string notes, Employee reportsTo, string photoPath)
        {
            _employeeID = employeeID;
            _lastName = lastName;
            _firstName = firstName;
            _title = title;
            _titleOfCourtesy = titleOfCourtesy;
            _birthDate = birthDate;
            _hireDate = hireDate;
            _address = address;
            _city = city;
            _region = region;
            _postalCode = postalCode;
            _country = country;
            _homePhone = homePhone;
            _extension = extension;
            _photo = photo;
            _notes = notes;
            _reportsTo = reportsTo;
            _photoPath = photoPath;
        }

        public Employee(int id)
        {
            string query = @"SELECT * FROM Employees WHERE EmployeeID = @ID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                _employeeID = Convert.ToInt32(row["EmployeeID"]);
                _lastName = Convert.ToString(row["LastName"]);
                _firstName = Convert.ToString(row["FirstName"]);
                _title = Convert.ToString(row["Title"]);
                _titleOfCourtesy = Convert.ToString(row["TitleOfCourtesy"]);
                _birthDate = row["BirthDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["BirthDate"]);
                _hireDate = row["HireDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["HireDate"]);
                _address = Convert.ToString(row["Address"]);
                _city = Convert.ToString(row["City"]);
                _region = Convert.ToString(row["Region"]);
                _postalCode = Convert.ToString(row["PostalCode"]);
                _country = Convert.ToString(row["Country"]);
                _homePhone = Convert.ToString(row["HomePhone"]);
                _extension = Convert.ToString(row["Extension"]);
                _photo = row["Photo"] == DBNull.Value ? null : (byte[])row["Photo"];
                _notes = Convert.ToString(row["Notes"]);
                _reportsTo = row["ReportsTo"] == DBNull.Value ? null : new Employee(Convert.ToInt32(row["ReportsTo"]));
                _photoPath = Convert.ToString(row["PhotoPath"]);
            }
        }

        #endregion

        #region Methods

        public static List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();

            string query = @"SELECT * FROM Employees ORDER BY LastName";

            SqlCommand command = new SqlCommand(query);
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Employee(
                    Convert.ToInt32(row["EmployeeID"]),
                    Convert.ToString(row["LastName"]),
                    Convert.ToString(row["FirstName"]),
                    Convert.ToString(row["Title"]),
                    Convert.ToString(row["TitleOfCourtesy"]),
                    row["BirthDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["BirthDate"]),
                    row["HireDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["HireDate"]),
                    Convert.ToString(row["Address"]),
                    Convert.ToString(row["City"]),
                    Convert.ToString(row["Region"]),
                    Convert.ToString(row["PostalCode"]),
                    Convert.ToString(row["Country"]),
                    Convert.ToString(row["HomePhone"]),
                    Convert.ToString(row["Extension"]),
                    row["Photo"] == DBNull.Value ? null : (byte[])row["Photo"],
                    Convert.ToString(row["Notes"]),
                    row["ReportsTo"] == DBNull.Value ? null : new Employee(Convert.ToInt32(row["ReportsTo"])),
                    Convert.ToString(row["PhotoPath"])
                ));
            }

            return list;
        }

        public bool Add()
        {
            string sql = @"INSERT INTO Employees
                        (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region,
                         PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath)
                        VALUES
                        (@LastName, @FirstName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate, @Address, @City,
                         @Region, @PostalCode, @Country, @HomePhone, @Extension, @Photo, @Notes, @ReportsTo, @PhotoPath)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@LastName", _lastName);
            command.Parameters.AddWithValue("@FirstName", _firstName);
            command.Parameters.AddWithValue("@Title", _title);
            command.Parameters.AddWithValue("@TitleOfCourtesy", _titleOfCourtesy);
            command.Parameters.AddWithValue("@BirthDate", _birthDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@HireDate", _hireDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Address", _address);
            command.Parameters.AddWithValue("@City", _city);
            command.Parameters.AddWithValue("@Region", _region);
            command.Parameters.AddWithValue("@PostalCode", _postalCode);
            command.Parameters.AddWithValue("@Country", _country);
            command.Parameters.AddWithValue("@HomePhone", _homePhone);
            command.Parameters.AddWithValue("@Extension", _extension);
            command.Parameters.AddWithValue("@Photo", _photo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Notes", _notes);
            command.Parameters.AddWithValue("@ReportsTo", _reportsTo != null ? _reportsTo.EmployeeID : (object)DBNull.Value);
            command.Parameters.AddWithValue("@PhotoPath", _photoPath);

            return SQLServerConnection.ExecuteNotQUery(command);
        }

        #endregion
    }
}
