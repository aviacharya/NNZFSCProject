using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NNZFSC.Startup))]
namespace NNZFSC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
