using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TingParser.Services;

namespace Crawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ICrawlerService, CrawlerService>()
                .AddSingleton<ILogtingParserService, LogtingParserService>()
                .BuildServiceProvider();

            var crawlerService = serviceProvider.GetService<ICrawlerService>();
            await crawlerService.Crawl();
        }
    }
}