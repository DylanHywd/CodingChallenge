using Xunit;
using Moq;
using TimesheetSystem.Controllers;
using TimesheetSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TimesheetSystem
{

    public class TimesheetControllerTests
    {
        [Fact]
        public void CheckSubmission()
        {
            
            var mockContext = new Mock<TimesheetDbContext>();
            var mockSet = new Mock<DbSet<TimesheetEntry>>();

            // Simulate DbSet behavior
            mockContext.Setup(m => m.TimesheetEntries).Returns(mockSet.Object);

            var controller = new TimesheetController(mockContext.Object);

            //Call the AddEntry method
            controller.AddEntry("Test User", DateTime.Now, "Project X", "Completed task", 5);

            //Verify that an entry was added
            mockSet.Verify(m => m.Add(It.IsAny<TimesheetEntry>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CSVStructureCheck()
        {
            //Example timesheet entry
            var mockEntries = new List<TimesheetEntry>
            {
                new TimesheetEntry {UserName = "Alice", Date = DateTime.Parse("2025-04-01"), Project = "Project A", TaskDescription = "Task 1", HoursWorked = 5 },
                new TimesheetEntry { UserName = "Bob", Date = DateTime.Parse("2025-04-02"), Project = "Project B", TaskDescription = "Task 2", HoursWorked = 8 }
            };

            var mockContext = new Mock<TimesheetDbContext>();
            var mockDbSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<TimesheetEntry>>();

            mockContext.Setup(m => m.TimesheetEntries).Returns(mockDbSet.Object);
            mockDbSet.Setup(m => m.ToList()).Returns(mockEntries);

            var controller = new TimesheetController(mockContext.Object);

            // Call DownloadCSV method
            var result = controller.DownloadCSV() as FileContentResult;

            // Convert result to string
            var csvContent = Encoding.UTF8.GetString(result.FileContents);

            // Check headers
            Assert.Contains("UserName,Date,Project,TaskDescription,HoursWorked", csvContent);

            // Check expected row data
            Assert.Contains("Alice,04/01/2025,Project A,Task 1,5", csvContent);
            Assert.Contains("Bob,04/02/2025,Project B,Task 2,8", csvContent);
        }
    }





}

