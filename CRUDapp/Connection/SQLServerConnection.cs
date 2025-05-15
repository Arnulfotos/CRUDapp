using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

public class SQLServerConnection
{

    #region Attributes

    private static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    public static SqlConnection connection = new SqlConnection(connectionString);


    #endregion




    #region Methods



    public static bool Open()
    {
        bool connected = true;
        if (connection.State != ConnectionState.Open)
        {
            try
            {
                connection.Open();
            }
            catch (SqlException ex) 
            {
                Console.WriteLine(ex.Message);
                connected = false;

            }
        }
            return connected;
    }

    public static DataTable ExecuteQuery(SqlCommand command) // PURO SELECT
    {
        DataTable table = new DataTable();

        if (Open())
        {
            command.Connection = connection;    
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                adapter.Fill(table);
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            connection.Close();
        }

      


        return table;


    }


    public static bool ExecuteNotQUery(SqlCommand command) // PURO SELECT
    {
        DataTable table = new DataTable();
        bool executed = false;
        if (Open())
        {

            command.Connection = connection;
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0) executed = true;
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        connection.Close();


        return executed;



        #endregion


    }







}

