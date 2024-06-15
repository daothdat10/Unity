using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartGame;
    public bool isGameOver;
    public GameObject titleScreen;                                                                                                                                                                                                                                                                                                                                                                                  
    public List<GameObject> targets;
    
    
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnTarget()
    {
        while (isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]); // Spawn at specified position

            
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        restartGame.gameObject.SetActive(true);
        isGameOver = false;
        gameOverText.gameObject.SetActive(true);

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void StartGame(int difficulty)
    {
        isGameOver = true;
        score = 0;
        StartCoroutine(SpawnTarget());

        spawnRate /= difficulty;


        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
}
