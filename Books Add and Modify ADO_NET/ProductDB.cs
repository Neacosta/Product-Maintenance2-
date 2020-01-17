using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Books_Add_and_Modify_ADO_NET
{
    public static class ProductDB
    {
        public static product GetProduct(string code)
        {
            //establish connection string to database
            SqlConnection conn = BooksDB.GetConnection();

            //select statement
            string strSel
                = "SELECT ProductCode, Description, UnitPrice "
                +"FROM Products "
                +"WHERE ProductCode = @Code";

          

            // set database connection and select statement to command.
            SqlCommand cmd = new SqlCommand(strSel, conn);

            //Declare Parameter
            cmd.Parameters.AddWithValue("@Code", code);

            try
            {
                //open database
                conn.Open();

                //data reader is used to get select statement.
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                //if there are results
                if (rdr.Read())
                {
                    product p = new product();
                    p.Code = rdr["ProductCode"].ToString();
                    p.Description = rdr["Description"].ToString();
                    p.Price = (decimal)rdr["UnitPrice"];
                    return p;

                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //Whether failed or succeeded shut down database connection.
                conn.Close();
            }
        }

        public static bool AddProduct(product product)
        {
            //Link Connection to conn object
            SqlConnection conn = BooksDB.GetConnection();

            //set string insertstatement to equal the query syntax
            string insertStatement =
                "INSERT INTO Products " +
                "(ProductCode, Description, UnitPrice) " +
                "Values (@Code, @Description, @Price)";

            //Declare Command and link query and connection
            SqlCommand cmd = new SqlCommand(insertStatement, conn);

            //declare all paramaters
            cmd.Parameters.AddWithValue("@Code", product.Code);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("Price", product.Price);

            try
            {
                //open connection to database
                conn.Open();

                //execute insert statement
                int count = cmd.ExecuteNonQuery();

                //if record was inserted return a true value
                if (count > 0)
                {
                    return true;
                }
                else
                    return false;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }


         }

        public static bool UpdateProduct(product oldProduct, product newProduct)
        {
            //There is an old and new product here. Basically this means that the original value 
            //when the method runs for each properties is stored into an object oldproduct.
            //the values changed by the user will go into newProduct.

            //Cross Check that the old values match in the database prior to changing with the new values.
            SqlConnection conn = BooksDB.GetConnection();
            string updateStatement =
                "UPDATE Products SET " +
                "Description = @NewDescription, " +
                "UnitPrice = @NewPrice " +
                "WHERE ProductCode = @OldCode " +
                "AND Description = @OldDescription " +
                "AND UnitPrice = @OldPrice";
            SqlCommand cmd = new SqlCommand(updateStatement, conn);

            //create paramaters and set to value
            cmd.Parameters.AddWithValue("@NewDescription", newProduct.Description);
            cmd.Parameters.AddWithValue("@NewPrice", newProduct.Price);
            cmd.Parameters.AddWithValue("@OldCode", oldProduct.Code);
            cmd.Parameters.AddWithValue("@OldDescription", oldProduct.Description);
            cmd.Parameters.AddWithValue("@OldPrice", oldProduct.Price);

            try
            {
                //open connection to the database
                conn.Open();

                //execute query and store either 0 for failed or 1 for updated
                int count = cmd.ExecuteNonQuery();

                //if the update happens then return a true otherwise false.
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                //throw sql error.
                throw ex;
            }
            finally
            {
                //close connection to the database
                conn.Close();
            }

        }

        public static bool DeleteProduct(product product)
        {
            //get connection string
            SqlConnection conn = BooksDB.GetConnection();

            //Set delete statement to equal the delete query
            string deleteStatement =
                "DELETE FROM Products " +
                "WHERE ProductCode = @Code " +
                "AND Description = @Description " +
                "AND UnitPrice = @Price ";

            //set cmd to have query and connection
            SqlCommand cmd = new SqlCommand(deleteStatement, conn);

            //declare parameters for the delete query.
            cmd.Parameters.AddWithValue("@Code", product.Code);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            try
            {
                //open the database
                conn.Open();
                
                //execute query
                int count = cmd.ExecuteNonQuery();

                //if row was deleted then return true otherwise false.
                if(count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                //throw sql error
                throw ex;
            }
            finally
            {
                //close connection to the database
                conn.Close();
            }
 
        }

    }
}
