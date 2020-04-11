using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GitHubController> Logger;

        public GitHubController(ILogger<GitHubController> logger, IConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {
            var github = new GitHubClient(new ProductHeaderValue("Transparency"));
            

            var tokenAuth = new Credentials(Configuration["GitHub:ServiceApiKey"]);
            github.Credentials = tokenAuth;

            var user = await github.User.Get("Microhive");
            Console.WriteLine(user.Followers + " folks love the half ogre!");

            var createIssue = new NewIssue("this thing doesn't work");
            var issue = await github.Issue.Create("Microhive", "Transparency", createIssue).ConfigureAwait(true);

            return $"{user.Name} has {user.PublicRepos} public repositories - go check out their profile at {user.Url}";
        }
    }
}
