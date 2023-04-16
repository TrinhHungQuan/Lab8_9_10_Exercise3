using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebApplicationProject.Pages.Item;

namespace WebApplicationProject.Pages.Agent
{
    
    public class CreateModel : PageModel
    {
        public AgentInfo agentInfo = new AgentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            agentInfo.AgentID = Request.Form["ID"];
            agentInfo.AgentName = Request.Form["name"];
            agentInfo.Address = Request.Form["Address"];

            if (agentInfo.AgentName.Length == 0 || agentInfo.AgentID.Length == 0 || agentInfo.Address.Length == 0)
            {
                errorMessage = "All the field are required";
                return;
            }

            //Save the new Agent into the database
            try
            {
                String connectionString = "Data Source=LAPTOP-SJ19AVB1\\SQLEXPRESS;Initial Catalog=ManagementSE;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Agent(AgentID, AgentName, Address) VALUES (@AgentID, @AgentName, @Address); ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AgentId", agentInfo.AgentID);
                        command.Parameters.AddWithValue("@AgentName", agentInfo.AgentName);
                        command.Parameters.AddWithValue("@Address", agentInfo.Address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            agentInfo.AgentID = ""; agentInfo.AgentName = ""; agentInfo.Address = "";
            successMessage = "New Agent Added Correctly";

            Response.Redirect("/Agent/Index");
        }
    }
}
