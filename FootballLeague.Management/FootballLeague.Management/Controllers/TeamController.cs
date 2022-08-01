using FootballLeague.Management.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballLeague.Management.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace FootballLeague.Management.Controllers
{
    public class TeamController : Controller
    {

        // GET: Team
        public ActionResult Display()
        {

            using (var context = new leagueDBContext())
            {
                try
                {
                    var teams = context.Teams.ToList();                
                    return View(teams);

                }
                catch (Exception ex)
                {
                    return View();
                }

            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                using (var context = new leagueDBContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (team.AwayJerseyColor != team.HomeJerseyColor)
                            {
                                team.Created = DateTime.Now;
                                team.Modified = DateTime.Now;
                                context.Teams.Add(team);
                                context.SaveChanges();
                                if (team.TeamID > 0)
                                {
                                    transaction.Commit();
                                    ViewBag.JavaScriptFunction = "ShowSuccessMsg();";
                                }
                                ModelState.Clear();
                                return RedirectToAction("Display");
                            }
                            else
                            {
                                ViewBag.JavaScriptFunction = string.Format("ShowFailure('{0}');");                                 
                                return View();
                            }

                        }
                        catch
                        {
                            //ViewBag.JavaScriptFunction = string.Format("ShowErrorMessage('{0}');", "Sorry!!..Couldnt Edit the Teams..Please try again later");
                            transaction.Rollback();
                            return View();
                        }

                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, Team objTeam)
        {
            if (ModelState.IsValid)
            {
                using (leagueDBContext context = new leagueDBContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            objTeam.Modified = DateTime.Now;
                            context.Entry(objTeam).State = EntityState.Modified;
                            context.SaveChanges();
                            transaction.Commit();
                            ModelState.Clear();
                            return RedirectToAction("Display");

                        }
                        catch (Exception ex)
                        {
                            ViewBag.Success = "Sorry!!..Couldnt Edit the Teams..Please try again later";
                            transaction.Rollback();
                            return View();
                        }

                    }
                }
            }
            return View();
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            using (var context = new leagueDBContext())
            {
                try
                {
                    var Team = context.Teams.Where(x => x.TeamID == id).FirstOrDefault();
                    return View(Team);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new leagueDBContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var team = context.Teams.Find(id);
                        context.Teams.Remove(team);
                        context.SaveChanges();
                        transaction.Commit();
                        return RedirectToAction("Display");
                    }
                    catch
                    {
                        transaction.Rollback();
                        return View();
                    }

                }
            }
        }
    }
}
