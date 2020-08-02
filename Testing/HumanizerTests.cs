using EzWorkout.Services;
using System;
using Xunit;

namespace Testing
{
    public class HumanizerTests
    {
        [Fact]
        public void DistanceFromMTest_01()
        {
            string dist = Humanizer.DistanceFromM(1001);

            Assert.Equal("1km 1m ", dist);
        }
        [Fact]
        public void DistanceFromMTest_02()
        {
            string dist = Humanizer.DistanceFromM(0);

            Assert.Equal("0m ", dist);
        }
    }
}
