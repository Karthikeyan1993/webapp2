using System;
using System.Net;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace webapp2.Test
{
    public class SimpleIntegrationTests
    {
        private readonly Action<IApplicationBuilder> _app;
        private readonly Action<IServiceCollection> _services;

        public SimpleIntegrationTests()
        {
            var environment = CallContextServiceLocator.Locator.ServiceProvider.GetRequiredService<IApplicationEnvironment>();

        }
        public TestServer CreateTestServer()
        {
            return TestServer.Create(app =>
            {
                var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
                //Microsoft.Extensions.Logging.ILoggerFactory loggerFactory = null;
                new Startup(env).Configure(app, env);
            }, services => { services.AddMvc(); });
        }

        [Fact]
        public async void ValuesControllerShouldReturnOkStatuscode()
        {
            var server = CreateTestServer();

            var response = await server.CreateClient().GetAsync("/api/test/");
            var deserialized = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(@"[""value100"",""value200"",""value00""]", deserialized);                        
        }
    }
}