using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDapp.Models
{
    public class Product
    {

        #region Properties
        private int _productId;
        private string _productName;
        private Supplier _supplierId;
        private Category _categoryId;
        private string _quantityPerUnit;
        private double _unitPrice;
        private double _unitsInStock;
        private double _unitsOnOrder;
        private int _reorderLevel;
        private bool _discontinued;
        #endregion

        #region Properties
        public int ProductId { get { return _productId; } set { _productId = value; } }
        public string ProductName { get { return _productName; } set { _productName = value; } }
        public Supplier SupplierId { get { return _supplierId; } set { _supplierId = value; } }
        public Category CategoryId { get { return _categoryId; } set { _categoryId = value; } }
        public string QuantityPerUnit { get { return _quantityPerUnit; } set { _quantityPerUnit = value; } }
        public double UnitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public double UnitsInStock { get { return _unitsInStock; } set { _unitsInStock = value; } }
        public double UnitsOnOrder { get { return _unitsOnOrder; } set { _unitsOnOrder = value; } }
        public int ReorderLevel { get { return _reorderLevel; } set { _reorderLevel = value; } }
        public bool Discontinued { get { return _discontinued; } set { _discontinued = value; } }
        #endregion

        #region Constructors
        public Product()
        {
            _productId = 0;
            _productName = "";
            _supplierId = new Supplier();
            _categoryId = new Category();
            _quantityPerUnit = "";
            _unitPrice = 0;
            _unitsInStock = 0.00;
            _unitsOnOrder = 0;
            _reorderLevel = 0;
            _discontinued = false;
        }
        public Product(
            int productId,
            string productName,
            Supplier supplierId,
            Category categoryId,
            string quantityPerUnit,
            double unitPrice,
            double unitsInStock,
            double unitsOnOrder,
            int reorderLevel,
            bool discontinued)
        {
            _productId = productId;
            _productName = productName;
            _supplierId = supplierId;
            _categoryId = categoryId;
            _quantityPerUnit = quantityPerUnit;
            _unitPrice = unitPrice;
            _unitsInStock = unitsInStock;
            _unitsOnOrder = unitsOnOrder;
            _reorderLevel = reorderLevel;
            _discontinued = discontinued;
        }

        public Product(int productId)
        {
            //query
            string query = @"Select ProductID, ProductName, SupplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, ReorderLevel, Discontinued from Products Where ProductID = @ID";

            //COMMAND
            SqlCommand command = new SqlCommand(query);
            //parameters
            command.Parameters.AddWithValue("@ID", productId);
            //execute command
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                _productId = Convert.ToInt32(row["ProductID"]);
                _productName = Convert.ToString(row["ProductName"]);
                _supplierId = new Supplier(Convert.ToInt32(row["SupplierID"]));
                _categoryId = new Category(Convert.ToInt32(row["CategoryID"]));
                _quantityPerUnit = Convert.ToString(row["QuantityPerUnit"]);
                _unitPrice = Convert.ToDouble(row["UnitPrice"]);
                _unitsInStock = Convert.ToDouble(row["UnitsInStock"]);
                _unitsOnOrder = Convert.ToDouble(row["UnitsOnOrder"]);
                _reorderLevel = Convert.ToInt32(row["ReorderLevel"]);
                _discontinued = Convert.ToBoolean(row["Discontinued"]);
            }
        }
        #endregion

        #region Methods
        public static List<Product> GetAll()
        {
            //list
            List<Product> list = new List<Product>();

            //query
            string query = @"Select ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued from Products Order By ProductName";

            //command
            SqlCommand command = new SqlCommand(query);

            //datatable
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    list.Add(new Product(
                    Convert.ToInt32(row["ProductID"]),
                    Convert.ToString(row["ProductName"]),
                    new Supplier(Convert.ToInt32(row["SupplierID"])),
                    new Category(Convert.ToInt32(row["CategoryID"])),
                    Convert.ToString(row["QuantityPerUnit"]),
                    Convert.ToDouble(row["UnitPrice"]),
                    Convert.ToDouble(row["UnitsInStock"]),
                    Convert.ToDouble(row["UnitsOnOrder"]),
                    Convert.ToInt32(row["ReorderLevel"]),
                    Convert.ToBoolean(row["Discontinued"])
                    ));

                }
            }

            return list;
        }

        #endregion

        #region add
        public bool Add()
        {
            //list
            List<Product> list = new List<Product>();

            //sql
            string sql = @"INSERT INTO Products VALUES
        (
            @NAME,
            @SUPPLIERID,
            @CATEGORYID,
            @QTYPERUNIT,
            @UNITPRICE,
            @STOCK,
            @ONORDER,
            @REORDER,
            @DISCONTINUED
        )";

            //command
            var command = new SqlCommand(sql);

            //parameters
            command.Parameters.AddWithValue("@NAME", _productName);
            command.Parameters.AddWithValue("@SUPPLIERID", _supplierId.SupplierID);
            command.Parameters.AddWithValue("@CATEGORYID", _categoryId.CategoryId);
            command.Parameters.AddWithValue("@QTYPERUNIT", _quantityPerUnit);
            command.Parameters.AddWithValue("@UNITPRICE", _unitPrice);
            command.Parameters.AddWithValue("@STOCK", _unitsInStock);
            command.Parameters.AddWithValue("@ONORDER", _unitsOnOrder);
            command.Parameters.AddWithValue("@REORDER", _reorderLevel);
            command.Parameters.AddWithValue("@DISCONTINUED", _discontinued);

            return SQLServerConnection.ExecuteNotQUery(command);
        }
        #endregion

    }
}
