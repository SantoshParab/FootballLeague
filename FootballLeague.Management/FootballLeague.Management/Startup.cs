using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballLeague.Management.Startup))]
namespace FootballLeague.Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
