using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FootballLeague.Management.Models;

namespace FootballLeague.Management.Context
{
    public class leagueDBContext:DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<MatchFixture> Fixtures { get; set; }

    }
}