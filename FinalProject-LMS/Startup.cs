using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalProject_LMS.Startup))]
namespace FinalProject_LMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
