using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkflowTracker.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace WorkflowTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
