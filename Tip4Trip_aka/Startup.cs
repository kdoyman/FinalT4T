using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tip4Trip_aka.Startup))]
namespace Tip4Trip_aka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
