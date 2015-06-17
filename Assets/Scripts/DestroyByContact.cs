using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameController))]
public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int ScoreValue;
    GameController gameController;

    void Start()
    {
        var gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Cannot find 'GameController' script");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") return;
        if(explosion != null) Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(ScoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
