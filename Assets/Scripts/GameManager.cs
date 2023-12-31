using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] 
    private List<Target> targetPrefabs;
    [SerializeField] 
    private List<Target> spawnedTargets;
    [SerializeField] private float spawnRate = 1.0f;
    public int difficulty;
    
    private bool _isRunning;

    [Header("Scoring")]
    [SerializeField] private int score;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        titleScreen = transform.Find("Mainscreen").gameObject;
        gameplayScreen = transform.Find("Gameplay").gameObject;
        scoreText = gameplayScreen.transform.Find("Canvas").Find("Text_Score").GetComponent<TextMeshProUGUI>();
        gameOverText = gameplayScreen.transform.Find("Canvas").Find("Text_GameOver").GetComponent<TextMeshProUGUI>();
        restartButton = gameOverText.transform.GetChild(0).Find("Button_Restart").GetComponent<Button>();
        quitButton = gameOverText.transform.GetChild(0).Find("Button_Quit").GetComponent<Button>();
    }

    public void SetDifficulty(int value)
    {
        titleScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        difficulty = value;
        spawnRate /= difficulty;
        spawnedTargets = new List<Target>();
        _isRunning = true;
        StartGame();
    }
    private void StartGame()
    {
        UpdateScore(0);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(Quit);
        StartCoroutine(SpawnTargets());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < spawnedTargets.Count; i++)
        {
            var target = spawnedTargets[i];
            if (!_isRunning || target.transform.position.y < -7)
            {
                target.OnDeath -= OnTargetDestroyed;
                Destroy(target.gameObject);
                spawnedTargets.Remove(target);
                i--;
            }
        }
    }


    private IEnumerator SpawnTargets()
    {
        while(_isRunning)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            var target = Instantiate(targetPrefabs[index]);
            target.OnDeath += OnTargetDestroyed;
            spawnedTargets.Add(target);
        }
    }
    
    private void OnTargetDestroyed(Target target, int value)
    {
        Instantiate(target.explosionParticle, target.transform.position, target.explosionParticle.transform.rotation);
        
        Destroy(target.gameObject);
        spawnedTargets.Remove(target);
        target.OnDeath -= OnTargetDestroyed;

        if (value == -1)
        {
            UpdateScore(-score);
            GameOver();
        }
        else UpdateScore(value);

    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        _isRunning = false;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
