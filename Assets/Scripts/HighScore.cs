using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
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
    public Text highScoreText;
    public int count;

    private List<Score> _scores;
    private readonly string _emptyKey = string.Empty;
    private const string KEY_PREFIX = "TopScore";
    public HighScoreInput highScoreInput;
    private int newScore;

    void Awake()
    {
        _scores = new List<Score>();
    }

    public void InitScores()
    {
        for (var i = 0; i < count; i++)
        {
            _scores.Add(new Score { Value = 0, Date = DateTime.MinValue, Name = _emptyKey });
        }
        LoadScores();
        highScoreText.text = string.Empty;
        //DisplayText();
    }

    public bool IsNewTopScore(int score)
    {
        return _scores.Any(s => s.Value < score);
    }

    public void AddTopScore(int score)
    {
        newScore = score;
        highScoreInput.ToggleShow();
    }

    public void OnAddTopScore()
    {
        highScoreInput.ToggleShow();
        _scores.RemoveAt(count - 1);
        _scores.Add(new Score { Name = highScoreInput.inputField.text, Date = DateTime.Now, Value = newScore });
        _scores.Sort();
        PersistScores();
    }

    private void PersistScores()
    {
        for (var index = 0; index < count; index++)
        {
            PlayerPrefs.SetInt(string.Format("{0}Value{1}", KEY_PREFIX, index), _scores[index].Value);
            PlayerPrefs.SetString(string.Format("{0}Name{1}", KEY_PREFIX, index), _scores[index].Name);
            PlayerPrefs.SetString(string.Format("{0}Date{1}", KEY_PREFIX, index), _scores[index].Date.ToString(CultureInfo.InvariantCulture));
        }
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        Debug.Log(_scores.Count);
        for (var index = 0; index < count; index++)
        {
            _scores[index].Value = PlayerPrefs.GetInt(string.Format("{0}Value{1}", KEY_PREFIX, index));
            _scores[index].Name = PlayerPrefs.GetString(string.Format("{0}Name{1}", KEY_PREFIX, index));
            var dateVal = PlayerPrefs.GetString(string.Format("{0}Date{1}", KEY_PREFIX, index));
            _scores[index].Date = dateVal == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PlayerPrefs.GetString(string.Format("{0}Date{1}", KEY_PREFIX, index)));
        }
    }

    public void DisplayText()
    {
        highScoreText.text = _scores.Aggregate(string.Empty, (current, score) => string.Format("{0}{1} {2} {3}\n", current, score.Value, score.Name, FormatDate(score.Date)));
    }

    private string FormatDate(DateTime date)
    {
        return date.ToString("dd/MM");
    }
}
