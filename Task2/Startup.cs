using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Task2.Startup))]
namespace Task2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
