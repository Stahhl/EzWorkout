using EzWorkout.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Testing
{
    public class DbTests
    {
        [Fact]
        public async Task ResetDb()
        {
            Constants.DropDb();
        }
        [Fact]
        public async Task SeedDb()
        {
            Constants.DropDb();

            var db = new WorkoutDb();

            await db.InitializeAsync();
            await db.SeedData();
        }
        [Fact]
        public async Task GetWorkoutsTest_01()
        {
            var db = new WorkoutDb();

            var workouts = await db.GetItemsAsync();

            Assert.Single(workouts);
        }
    }
}
