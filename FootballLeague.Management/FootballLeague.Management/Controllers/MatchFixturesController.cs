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
                DateTime startDate = Convert.ToDateTime(form["SelectedDate"]);
                return RedirectToAction("Display" ,new { startDate = startDate });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveFixture(IEnumerable<MatchFixture> fixturelist)
        {
            if (ModelState.IsValid)
            {
                using (leagueDBContext context = new leagueDBContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach(MatchFixture match in fixturelist)
                            {
                                match.Created = DateTime.Now;
                                context.Entry(match).State = EntityState.Modified;
                                context.SaveChanges();                                
                            }
                            transaction.Commit();
                            ModelState.Clear();
                            return RedirectToAction("Create");

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
    }
}
