using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Go2MusicStore.Startup))]
namespace Go2MusicStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
