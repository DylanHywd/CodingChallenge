using Xunit;

namespace TimesheetSystem.Models
{
    public class TimesheetRepositoryTests
    {
        
        [Fact]
        public void Empty_List_Check()
        {
            var repository = new TimesheetRepository();

            var result = repository.GetEntries();

            Assert.Empty(result); // Ensure the list is empty when no entries exist
        }
    }
}
