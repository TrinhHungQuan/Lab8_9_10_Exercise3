using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationProject.Pages.Agent
{
    public class IndexModel : PageModel
    {
        public List<AgentInfo> listAgent = new List<AgentInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-SJ19AVB1\\SQLEXPRESS;Initial Catalog=ManagementSE;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Agent";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AgentInfo agentInfo = new AgentInfo();
                                agentInfo.AgentID = reader.GetString(0);
                                agentInfo.AgentName = reader.GetString(1);
                                agentInfo.Address = reader.GetString(2);

                                listAgent.Add(agentInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                 
            }
        }
    }
    public class AgentInfo
    {
        public String AgentID;
        public String AgentName;
        public String Address;
    }
}
