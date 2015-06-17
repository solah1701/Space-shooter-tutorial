using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(HighScore))]
public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public GameObject enemyRed;
    public GameObject enemyPurple;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    public bool gameOver;
    private bool restart;
    private int score;
    private HighScore highScore;


    void UpdateScore()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                var spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                var spawnRotation = Quaternion.identity;
                Instantiate(enemyRed, spawnPosition, spawnRotation);
                //Instantiate(enemyRed, new Vector3(i, spawnValue.y, spawnValue.z), Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (!gameOver) continue;
            restartText.text = "Press 'R' to restart";
            restart = true;
            break;
        }
    }

    IEnumerator SpawnRedEnemy()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = -(int)spawnValue.x; i < spawnValue.x; i++)
            {
                var spawnPosition = new Vector3(i, spawnValue.y, spawnValue.z);
                var spawnRotation = Quaternion.identity;
                Instantiate(enemyRed, spawnPosition, spawnRotation);
                i++;
            }
            yield return new WaitForSeconds(waveWait);
            if (!gameOver) continue;
            restartText.text = "Press 'R' to restart";
            restart = true;
            break;
        }
    }

    // Use this for initialization
    void Start()
    {
        score = 0;
        if (highScore == null) highScore = GetComponent<HighScore>();
        if (highScore == null) throw new MissingComponentException("HighScore component is missing");
        highScore.InitScores();
        gameOverText.text = string.Empty;
        restartText.text = string.Empty;
        UpdateScore();
        //StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnRedEnemy());
    }

    void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(Application.loadedLevel);
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        StartCoroutine(EndGame());
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        gameOverText.text = "Game Over";
        gameOver = true;
        if (highScore.IsNewTopScore(score))
            highScore.AddTopScore(score);
        highScore.DisplayText();
    }
}
