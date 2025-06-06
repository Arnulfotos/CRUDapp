using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDapp.Models
{
    public class OrderDetail
    {
        #region Attributes

        private Order _order;
        private Product _product;
        private decimal _unitPrice;
        private short _quantity;
        private float _discount;

        #endregion

        #region Properties

        public Order Order { get => _order; set => _order = value; }
        public Product Product { get => _product; set => _product = value; }
        public decimal UnitPrice { get => _unitPrice; set => _unitPrice = value; }
        public short Quantity { get => _quantity; set => _quantity = value; }
        public float Discount { get => _discount; set => _discount = value; }

        #endregion

        #region Constructors

        public OrderDetail()
        {
            _order = null;
            _product = null;
            _unitPrice = 0;
            _quantity = 1;
            _discount = 0;
        }

        public OrderDetail(Order order, Product product, decimal unitPrice, short quantity, float discount)
        {
            _order = order;
            _product = product;
            _unitPrice = unitPrice;
            _quantity = quantity;
            _discount = discount;
        }

        public OrderDetail(int orderId, int productId)
        {
            string query = @"SELECT * FROM [Order Details] WHERE OrderID = @OrderID AND ProductID = @ProductID";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@OrderID", orderId);
            command.Parameters.AddWithValue("@ProductID", productId);

            DataTable table = SQLServerConnection.ExecuteQuery(command);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                _order = new Order(Convert.ToInt32(row["OrderID"]));
                _product = new Product(Convert.ToInt32(row["ProductID"]));
                _unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                _quantity = Convert.ToInt16(row["Quantity"]);
                _discount = Convert.ToSingle(row["Discount"]);
            }
        }

  

        #endregion

        #region Methods

        public static List<OrderDetail> GetAll()
        {
            List<OrderDetail> list = new List<OrderDetail>();

            string query = @"SELECT * FROM [Order Details]";
            SqlCommand command = new SqlCommand(query);
            DataTable table = SQLServerConnection.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new OrderDetail(
                    new Order(Convert.ToInt32(row["OrderID"])),
                    new Product(Convert.ToInt32(row["ProductID"])),
                    Convert.ToDecimal(row["UnitPrice"]),
                    Convert.ToInt16(row["Quantity"]),
                    Convert.ToSingle(row["Discount"])
                ));
            }

            return list;
        }

        public static List<OrderDetail> GetAllFromOrder(int id)
        {
            List<OrderDetail> list = new List<OrderDetail>();

            string query = @"SELECT * FROM [Order Details] WHERE OrderID = @OrderID";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@OrderID", id);

            DataTable table = SQLServerConnection.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new OrderDetail(
                    new Order(Convert.ToInt32(row["OrderID"])),
                    new Product(Convert.ToInt32(row["ProductID"])),
                    Convert.ToDecimal(row["UnitPrice"]),
                    Convert.ToInt16(row["Quantity"]),
                    Convert.ToSingle(row["Discount"])
                ));
            }

            return list;
        }

        public bool Add()
        {
            string sql = @"INSERT INTO [Order Details] 
                           (OrderID, ProductID, UnitPrice, Quantity, Discount)
                           VALUES 
                           (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@OrderID", _order.OrderID);
            command.Parameters.AddWithValue("@ProductID", _product.ProductId);
            command.Parameters.AddWithValue("@UnitPrice", _unitPrice);
            command.Parameters.AddWithValue("@Quantity", _quantity);
            command.Parameters.AddWithValue("@Discount", _discount);

            return SQLServerConnection.ExecuteNotQUery(command);
        }

        #endregion
    }
}
