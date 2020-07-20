using System.Threading.Tasks;

namespace Crawler
{
    interface ICrawlerService
    {
        public Task Crawl();
    }
}