using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimesheetSystem.Models;

namespace TimesheetSystem.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly TimesheetDbContext _context; // Declare context


        public TimesheetController(TimesheetDbContext context)
        {
            _context = context; // Correctly assign the injected DbContext

           
        }

        public ActionResult Index()
        {
            var entries = _context.TimesheetEntries.ToList(); // Fetch timesheet entries from the database
            return View(entries); // Pass data to the view
        }

        [HttpPost]
        public ActionResult AddEntry(string userName, DateTime date, string project, string taskDescription, int hoursWorked)
        {
            var entry = new TimesheetEntry
            {
                UserName = userName,
                Date = date,
                Project = project,
                TaskDescription = taskDescription,
                HoursWorked = hoursWorked
                
            };

            

            _context.TimesheetEntries.Add(entry); // Add entry to database
            _context.SaveChanges(); // Save changes to database


            // Group total hours per user per day
            var dailyTotalHours = _context.TimesheetEntries
                .GroupBy(e => new { e.UserName, e.Date }) // Group by UserName and Date
                .Select(g => new
                {
                    UserName = g.Key.UserName,
                    Date = g.Key.Date,
                    TotalHours = g.Sum(e => e.HoursWorked)
                })
                .ToList();

            // Update each user's entries with their corresponding daily total hours
            foreach (var entry1 in _context.TimesheetEntries)
            {
                var matchingTotal = dailyTotalHours.FirstOrDefault(t => t.UserName == entry1.UserName && t.Date == entry1.Date);
                if (matchingTotal != null)
                {
                    entry1.TotalHoursWorked = matchingTotal.TotalHours;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Timesheet/DownloadCSV")]
        public IActionResult DownloadCSV()
        {
            var entries = _context.TimesheetEntries.ToList();

            var csvBuilder = new System.Text.StringBuilder();
            csvBuilder.AppendLine("UserName,Date,Project,TaskDescription,HoursWorked,TotalHoursWorked");

            foreach (var entry in entries)
            {
                csvBuilder.AppendLine($"{entry.UserName},{entry.Date.ToShortDateString()},{entry.Project},{entry.TaskDescription},{entry.HoursWorked},{entry.TotalHoursWorked}");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csvBuilder.ToString());
            return File(bytes, "text/csv", "TimesheetEntries.csv");
        }
    }
}