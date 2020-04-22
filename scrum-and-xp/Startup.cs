using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("Chat", typeof(scrum_and_xp.Startup))]
namespace scrum_and_xp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
