using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FplDataWebsite.WebSite.Models;
using FplDataWebsite.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FplDataWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileTeamService TeamService;
        public IEnumerable<Team> Teams { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileTeamService teamService)
        {
             TeamService= teamService;
            _logger = logger;
        }

        public void OnGet()
        {
            Teams = TeamService.GetTeams();
        }
    }
}
