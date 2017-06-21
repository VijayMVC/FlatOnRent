using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentFlat.Web.Startup))]
namespace RentFlat.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
