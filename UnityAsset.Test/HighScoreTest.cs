using System.Linq;
using NUnit.Framework;
using UnityAssets;

namespace UnityAsset.Test
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
            var expected = new Score { Name = NAME, Value = SCORE };

            // Act
            HighScore.AddTopScore(NAME, SCORE);
            var actual = HighScore.Scores;

            // Assert
            var result = actual.Any(s => s.Name == expected.Name && s.Value == expected.Value);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsNewTopScore_Returns_True_If_Score_Is_Greater_Than_Any_Score()
        {
            // Arrange
            const int SCORE = 123;

            // Act
            var actual = HighScore.IsNewTopScore(SCORE);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsNewTopScore_Returns_False_If_Score_Is_Not_Greater_Than_Any_Score()
        {
            // Arrange
            const int SCORE = 0;

            // Act
            var actual = HighScore.IsNewTopScore(SCORE);

            // Assert
            Assert.IsFalse(actual);
        }
    }
}
