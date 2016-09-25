using System.Configuration;

namespace DhrAgentDatabaseUtils
{
    public partial class AgentDatabaseDataContext
    {
        public AgentDatabaseDataContext() : base (ConfigurationManager.ConnectionStrings["SoftphoneAgentDatabaseConnectionString"].ConnectionString)
        {
            OnCreated();
        }

    }
}