using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PROJECTADM.Startup))]
namespace PROJECTADM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
