using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FplDataWebsite.WebSite.Models;
using FplDataWebsite.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FplDataWebsite.Website.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        public TeamsController(JsonFileTeamService teamsService)
        {
            this.TeamsService = teamsService;
        }

        public JsonFileTeamService TeamsService { get;  }

        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return TeamsService.GetTeams();
        }

        [Route("Rate")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] string TeamId, 
            [FromQuery] int Rating)
        {
            TeamsService.AddRating(TeamId, Rating);
            return Ok();
        }
    }
}
