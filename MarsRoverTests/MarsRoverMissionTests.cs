using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRover.Tests
{
    [TestClass()]
    public class MarsRoverMissionTests
    {
        [TestMethod()]
        public void TestDoInstructions()
        {
            // arrange
            string[] input_data =
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };

            string[] expected_output =
            {
                "1 3 N",
                "5 1 E"
            };

            MarsRoverMission mission = new MarsRoverMission();

            string[] actual_output = mission.DoInstructions(input_data);

            // assert
            CollectionAssert.AreEqual(expected_output, actual_output);
        }
    }
}