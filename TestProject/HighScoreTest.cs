using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityAssets;

namespace TestProject
{
    [TestFixture]
    public class HighScoreTest
    {
        private HighScore HighScore { get; set; }

        [SetUp]
        public void SetUp()
        {
            HighScore = new HighScore { Count = 2 };
            HighScore.InitScores();
        }

        [Test]
        public void AddTopScore_Removes_Lowest_Score_Then_Adds_New_Score()
        {
            // Arrange
            const string NAME = "myName";
            const int SCORE = 123;
            var expected = new SortedList { { string.Empty, 0 }, { NAME, SCORE } };

            // Act
            HighScore.AddTopScore(NAME, SCORE);
            var actual = HighScore.Scores;

            // Assert
            Assert.Contains(expected, actual);
        }
    }
}
