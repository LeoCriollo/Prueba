using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoleEcIntranet.Startup))]
namespace DoleEcIntranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
