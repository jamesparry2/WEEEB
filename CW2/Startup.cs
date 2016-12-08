using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CW2.Startup))]
namespace CW2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
