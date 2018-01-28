using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SantaClouse.Startup))]
namespace SantaClouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
