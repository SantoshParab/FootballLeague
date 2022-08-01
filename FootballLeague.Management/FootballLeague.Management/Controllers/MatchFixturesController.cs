using FootballLeague.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballLeague.Management.Models;
using System.Data.Entity;
using FootballLeague.Management.Context;

namespace FootballLeague.Management.Controllers
{
    public class MatchFixturesController : Controller
    {
        // GET: MatchFixtures
        public ActionResult Display(DateTime startDate)
        {
            GenerateFixture objGenerateFixture = new GenerateFixture();
            List<MatchFixture> matchList = objGenerateFixture.GetMatches(startDate);
            matchList.Shufflelist();
            List<MatchFixture> fixtures = objGenerateFixture.SetupGame(matchList, startDate);
           Session["fixturesList"] = fixtures;
            return View(fixtures);
           
        }

        // GET: MatchFixtures/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MatchFixtures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MatchFixtures/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                // TODO: Add insert logic here
                string selectedDate = (form["SelectedDate"]);
                string[] selectedDateArray = selectedDate.Split('/');
                DateTime startDate = new DateTime(Convert.ToInt32(selectedDateArray[2]), Convert.ToInt32(selectedDateArray[0]), Convert.ToInt32(selectedDateArray[1]),0,0,0);
                return RedirectToAction("Display" ,new { startDate = startDate });
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: MatchFixtures/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MatchFixtures/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MatchFixtures/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MatchFixtures/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //public ActionResult Save(FormCollection form)
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult Save()
        {
            if (ModelState.IsValid)
            {
                List<MatchFixture> listFixture =  Session["fixturesList"] as List<MatchFixture> ;
                using (leagueDBContext context = new leagueDBContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (MatchFixture match in listFixture)
                            {
                                MatchFixture objmatch = new MatchFixture();

                                objmatch.HomeTeamID = match.HomeTeamID;
                                objmatch.AwayTeamID = match.AwayTeamID;
                                objmatch.MatchDate = match.MatchDate;
                                objmatch.Created = DateTime.Now;

                                context.Fixtures.Add(objmatch);
                                context.SaveChanges();
                            }
                            transaction.Commit();
                            ModelState.Clear();
                            return RedirectToAction("Display","Team");

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
    }
}
