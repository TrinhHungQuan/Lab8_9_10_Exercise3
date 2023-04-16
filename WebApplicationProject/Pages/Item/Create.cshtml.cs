using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationProject.Pages.Item
{
    public class CreateModel : PageModel
    {
        public ItemInfo itemInfo = new ItemInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
            itemInfo.ItemID = Request.Form["id"];
            itemInfo.ItemName = Request.Form["name"];
            itemInfo.Size = Request.Form["size"];

            if(itemInfo.ItemName.Length ==0 || itemInfo.ItemName.Length == 0 || itemInfo.Size.Length==0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //Save the item into database
            try
            {
                String connectionString = "Data Source=LAPTOP-SJ19AVB1\\SQLEXPRESS;Initial Catalog=ManagementSE;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Item(ItemId, ItemName, Size) VALUES (@ItemId, @ItemName, @Size); ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ItemId", itemInfo.ItemID);
                        command.Parameters.AddWithValue("@ItemName", itemInfo.ItemName);
                        command.Parameters.AddWithValue("@Size", itemInfo.Size);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            itemInfo.ItemID = ""; itemInfo.ItemName = ""; itemInfo.Size = "";
            successMessage = "New Item added correctly";

            Response.Redirect("/Item/Index");
        }
    }
}
