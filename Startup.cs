using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScrumProject.Startup))]
namespace ScrumProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
