using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookStoreMVC.Startup))]
namespace BookStoreMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
