using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationProject.Pages.FilterItem
{
    public class IndexModel : PageModel
    {
        public List<ItemInfo> listItems1 = new List<ItemInfo>();
        public List<ItemInfo> listItems2 = new List<ItemInfo>();
       
        public void OnGet(string sortOrder)
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-SJ19AVB1\\SQLEXPRESS;Initial Catalog=ManagementSE;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT Item.ItemID, Item.ItemName, SUM(OrderDetail.Quantity) AS TotalSold FROM OrderDetail JOIN Item ON OrderDetail.ItemID = Item.ItemID GROUP BY Item.ItemID, Item.ItemName ";
                    switch (sortOrder)
                    {
                        case "1":
                            sql += "ORDER BY TotalSold DESC";
                            break;
                        case "2":
                            sql += "ORDER BY TotalSold ASC";
                            break;
                        case "3":
                            sql += "HAVING SUM(OrderDetail.Quantity) >= 5";
                            break;
                        case "4":
                            sql += "HAVING SUM(OrderDetail.Quantity) < 5";
                            break;
                        default:
                            break;
                    }
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemInfo itemInfo = new ItemInfo();
                                itemInfo.ItemID = reader.GetString(0);
                                itemInfo.ItemName = reader.GetString(1);
                                itemInfo.Quantity = "" + reader.GetInt32(2);

                                listItems1.Add(itemInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

    }

    public class ItemInfo
    {
        public String ItemID;
        public String ItemName;
        public String Quantity;
    }


}
