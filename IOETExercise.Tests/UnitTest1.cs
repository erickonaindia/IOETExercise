using IOETExercise.Core;
using IOETExercise.Models;
using NUnit.Framework;

namespace IOETExercise.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestExpected()
        {
            var _mockData = new DataObjectFiles()
            {
                Name =  "RENE",
                Details = new System.Collections.Generic.List<Details>()
                {
                    new Details()
                    {
                        WeekDay = WeekDays.MO,
                        StartTime = System.DateTime.ParseExact($"1900-01-01 10:00", "yyyy-MM-dd HH:mm", null),
                        EndTime = System.DateTime.ParseExact($"1900-01-01 12:00", "yyyy-MM-dd HH:mm", null),
                    }
                }
            };
            var result = ReadParseFile.CalculateAmount(_mockData);
            Assert.AreEqual("The amount to pay RENE is: 30 USD", result);
        }
    }
}