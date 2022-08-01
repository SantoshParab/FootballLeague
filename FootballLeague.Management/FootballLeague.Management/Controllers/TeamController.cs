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
            ViewBag.JavaScriptFunction = TempData["JavascriptFunction"];
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
            string message = "";
            if (ModelState.IsValid)
            {
                using (var context = new leagueDBContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            Team savedTeam = context.Teams.FirstOrDefault(x => x.TeamName == team.TeamName.ToLower());
                            if (!string.IsNullOrEmpty(savedTeam.TeamName))
                            {
                                message = "Team Name already registered";
                                ViewBag.JavaScriptFunction = string.Format("ShowFailure('{0}');", message);
                                return View();
                            }
                            else
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
                                        message = "New Team Registered Successfully";
                                        ViewBag.JavaScriptFunction = string.Format("ShowSuccessMsg('{0}');", message);
                                    }
                                    ModelState.Clear();
                                    return View();
                                    //return RedirectToAction("Display");
                                }
                                else
                                {

                                    return View();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            message = "Error occured while registering..Please try again later";
                            ViewBag.JavaScriptFunction = string.Format("ShowFailure('{0}');", message);
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
            string message = "";
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
                            message = "Team Details Changes Succesfully";
                            TempData["JavascriptFunction"] = string.Format("ShowSuccessMsg('{0}');", message);
                            return RedirectToAction("Display");
                        }
                        catch (Exception ex)
                        {
                            message = "Error occured while registering..Please try again later";
                            ViewBag.JavaScriptFunction = string.Format("ShowFailure('{0}');", message);
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
