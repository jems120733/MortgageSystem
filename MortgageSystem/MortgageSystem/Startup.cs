using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MortgageSystem.Startup))]
namespace MortgageSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
