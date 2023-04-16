using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationProject.Pages.Order
{
    public class IndexModel : PageModel
    {
        public List<OrderInfo> listOrder = new List<OrderInfo>();
        public List<OrderIDInfo> listOrderID = new List<OrderIDInfo>();

        public void OnGet(string orderID)
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-SJ19AVB1\\SQLEXPRESS;Initial Catalog=ManagementSE;Integrated Security=True";

                if (!string.IsNullOrEmpty(orderID))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM OrderDetail WHERE OrderID = @OrderID";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@OrderID", orderID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    OrderInfo orderInfo = new OrderInfo();
                                    orderInfo.ID = reader.GetString(0);
                                    orderInfo.OrderID = reader.GetString(1);
                                    orderInfo.ItemID = reader.GetString(2);
                                    orderInfo.Quantity = "" + reader.GetInt32(3);
                                    orderInfo.UnitAmount = reader.GetString(4);

                                    listOrder.Add(orderInfo);
                                }
                            }
                        }
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql1 = "SELECT OrderID FROM Orders";

                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderIDInfo orderIDInfo = new OrderIDInfo();
                                orderIDInfo.OrderID = reader.GetString(0);

                                listOrderID.Add(orderIDInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class OrderInfo
    {
        public String ID;
        public String OrderID;
        public String ItemID;
        public String Quantity;
        public String UnitAmount;
    }

    public class OrderIDInfo
    {
        public String OrderID;
    }
}
