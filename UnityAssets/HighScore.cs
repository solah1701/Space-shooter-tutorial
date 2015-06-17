using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityAssets
{
    public class Score : EqualityComparer<Score>, IComparable
    {
        public int Value;
        public DateTime Date;
        public string Name;
        public int CompareTo(object obj)
        {
            var testObj = obj as Score;
            if (testObj == null) throw new ArgumentException("Object is not a Score");
            var result = testObj.Value.CompareTo(Value);
            return result == 0 ? testObj.Date.CompareTo(Date) : result;
        }

        public override bool Equals(Score x, Score y)
        {
            return x.Value == y.Value && x.Name == y.Name && x.Date == y.Date;
        }

        public override int GetHashCode(Score obj)
        {
            if (obj != null) return obj.Value;
            throw new ArgumentException("Object not of type Score");
        }
    }

    public class HighScore
    {
        private readonly List<Score> _scores;
        private readonly SortedList _scoresold;
        private readonly string _emptyKey = string.Empty;

        public HighScore()
        {
            _scores = new List<Score>();
        }

        public int Count { get; set; }

        public List<Score> Scores { get { return _scores; } }

        public void InitScores()
        {
            for (var i = 0; i < Count; i++)
            {
                _scores.Add(new Score { Value = 0, Date = DateTime.MinValue, Name = string.Empty });
            }
        }

        public bool IsNewTopScore(int score)
        {
            return _scores.Any(s => s.Value < score);
        }

        public void AddTopScore(string name, int score)
        {
            _scores.RemoveAt(Count - 1);
            _scores.Add(new Score { Name = name, Value = score });
            _scores.Sort();
        }
    }
}
