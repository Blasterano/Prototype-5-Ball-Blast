using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.5f;
    private int score;
    private int life;
    private bool paused;

    public List<GameObject> targets;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI gameOverText;
    public Button restart;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Pause();
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void StartGame(int difficulty)
    {
        score = 0;
        life = 3;
        paused = false;
        isGameActive = true;
        spawnRate /= difficulty;

        UpdateScore(0);
        StartCoroutine("SpawnTarget");

        titleScreen.gameObject.SetActive(false);
    }


    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void Life()
    {
        life--;
        lifeText.text = "Life: " + life;
        if (life <= 0)
        {
            life = 0;
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseScreen.gameObject.SetActive(paused);
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            pauseScreen.gameObject.SetActive(paused);
        }
    }

}
