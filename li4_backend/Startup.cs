using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(li4_backend.Startup))]
namespace li4_backend
{
    public partial class Startup
    {
      public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
