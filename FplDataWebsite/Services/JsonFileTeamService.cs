using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FplDataWebsite.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace FplDataWebsite.WebSite.Services 
{
    public class JsonFileTeamService
    {
        public JsonFileTeamService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "teams.json"); }
        }

        public IEnumerable<Team> GetTeams()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Team[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AddRating(string teamId, int rating)
        {
            var teams = GetTeams();
            var query = teams.First(x => x.Id == teamId);
            if(query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Team>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    teams
                    );
            }
        }
    }
}