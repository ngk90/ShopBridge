
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Repositories;
using Xunit;

namespace ShopBridge.UnitTest.test
{
    public class StartupTest
    {
        [Fact]
        public void StartupTest1()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<MyStartup>().Build();
            Assert.NotNull(webHost);
            Assert.NotNull(webHost.Services.GetRequiredService<IInventoryRepository>());
        }
    }

    public class MyStartup : Startup
    {
        public MyStartup(IConfiguration config) : base(config) { }
    }
}
