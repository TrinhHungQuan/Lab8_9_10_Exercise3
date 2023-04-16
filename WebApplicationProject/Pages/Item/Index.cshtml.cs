using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationProject.Pages.Item
{
    public class IndexModel : PageModel
    {
        public List<ItemInfo> listItems = new List<ItemInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-SJ19AVB1\\SQLEXPRESS;Initial Catalog=ManagementSE;Integrated Security=True";
                
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Item";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ItemInfo itemInfo = new ItemInfo();
                                itemInfo.ItemID = reader.GetString(0);
                                itemInfo.ItemName = reader.GetString(1);
                                itemInfo.Size = "" + reader.GetInt32(2);

                                listItems.Add(itemInfo);
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
        public String Size;
    }
}
