using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FootballLeague.Management.Context;
using System.Data.Entity;

namespace FootballLeague.Management.Models
{
    [Table("Match_Fixtures")]
    public class MatchFixture
    {
        [Key]
        public int FixtureID { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }

        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }
        public DateTime Created { get; set; }

        [NotMapped]
        public string HomeTeam { get; set; }
        [NotMapped]
        public string AwayTeam { get; set; }
        [NotMapped]
        public string HomeJerseyColor { get; set; }
        [NotMapped]
        public string AwayJerseyColor { get; set; }
        [NotMapped]
        public string GroundName { get; set; }
    }

    public class GenerateFixture
    {
        public List<MatchFixture> GetMatches(DateTime startDate)
        {
            List<MatchFixture> MatchesList = new List<MatchFixture>();
            using (var context = new leagueDBContext())
            {
                try
                {

                    List<Team> teams = context.Teams.ToList();
                    var count = teams.Count();
                    //int day = 0;
                    foreach (Team team in teams)
                    {
                        foreach (Team oppTeam in teams)
                        {
                            if (team != oppTeam)
                            {

                                MatchFixture playingTeams = new MatchFixture();
                                playingTeams.HomeTeamID = team.TeamID;
                                playingTeams.AwayTeamID = oppTeam.TeamID;
                                //playingTeams.MatchDate = startDate.AddDays(day);
                                //day++;
                                MatchesList.Add(playingTeams);
                                //playingTeams = new MatchFixture();
                                //playingTeams.HomeTeamID = oppTeam.TeamID;
                                //playingTeams.AwayTeamID = team.TeamID;
                                //playingTeams.MatchDate = startDate.AddDays(1);
                                //MatchesList.Add(playingTeams);
                                //day++;

                            }
                        }
                    }
                    
                    return MatchesList;
                    //var rnd = new Random();
                    //return MatchesList.OrderBy(item=>rnd.Next()).ToList();
                }
                catch
                {
                    return MatchesList;
                }
            }
        }

        public List<MatchFixture> SetupGame(List<MatchFixture> fixtures,DateTime startdate)
        {
            using (var context = new leagueDBContext())
            {
                List<MatchFixture> matchList = new List<MatchFixture>();
                try
                {
                    List<Team> teams = context.Teams.ToList();
                    int day = 0;
                    var query  = (from F in fixtures
                                join t in teams
                                on F.HomeTeamID equals t.TeamID
                                join t2 in teams
                                on F.AwayTeamID equals t2.TeamID
                                select new
                                {
                                    HomeTeam = t.TeamName,
                                    HomeTeamID = t.TeamID,                                    
                                    AwayTeam = t2.TeamName,
                                    AwayTeamID = t2.TeamID,
                                    HomeTeamJerseyColor = t.HomeJerseyColor,
                                    AwayTeamJerseyColor = (t2.HomeJerseyColor.ToLower() == t.HomeJerseyColor.ToLower() ? t2.AwayJerseyColor : t2.HomeJerseyColor),
                                    Ground = t.GroundName,                                       
                                });
                    matchList = query.Select((p) => new MatchFixture
                                {
                                    
                                    HomeTeamID = p.HomeTeamID,
                                    HomeTeam = p.HomeTeam,
                                    HomeJerseyColor = p.HomeTeamJerseyColor,
                                    AwayTeamID = p.AwayTeamID,
                                    AwayTeam = p.AwayTeam,
                                    AwayJerseyColor=p.AwayTeamJerseyColor,
                                    GroundName = p.Ground,    
                                                                 
                                }
                                ).ToList();
                    foreach (var item in matchList)
                    {
                        item.MatchDate = startdate.AddDays(day);
                        day++;
                    }

                    return matchList;
                }
                catch (Exception ex)
                {
                    return matchList;
                }
            }




        }
    }

}