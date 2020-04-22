using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(scrum_and_xp.Startup))]
namespace scrum_and_xp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
