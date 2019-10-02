using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem.Local;
using Microsoft.Owin;
using Owin;
using Task2.App_Start;

[assembly: OwinStartupAttribute(typeof(Task2.Startup))]
namespace Task2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            app.Map("/ckfinder/connector", SetupConnector);
            ConfigureAuth(app);
        }

        private static void SetupConnector(IAppBuilder app)
        {
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();

            var customAuthenticator = new CKFinderAuthenticator();

            connectorBuilder
                .LoadConfig()
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration((request, config) => { config.LoadConfig(); });

            var connector = connectorBuilder.Build(connectorFactory);

            app.UseConnector(connector);
        }
    }
}
