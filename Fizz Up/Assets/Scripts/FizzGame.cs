using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FizzGame : MonoBehaviour
{
    public Text fizzText;
    public Text timerText;
    public Text historyText;
    public Button restartButton;

    public float fizzLevel = 0;
    public float maxFizz = 100;
    private float timer = 0f;
    private bool gameRunning = true;

    private List<float> previousTimes = new List<float>();

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        LoadHistory(); // Load history on start
    }

    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2") + "s";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                fizzLevel++;
                fizzText.text = "Shakes: " + fizzLevel;

                if (fizzLevel >= maxFizz)
                {
                    EndGame();
                }
            }
        }
    }

    void EndGame()
    {
        gameRunning = false;
        previousTimes.Add(timer);
        SaveHistory();  // Save history when game ends
        UpdateHistoryUI();
        timerText.text += " - DONE!";
        restartButton.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }

    void UpdateHistoryUI()
    {
        historyText.text = "HISTORY\n";
        foreach (float time in previousTimes)
        {
            historyText.text += time.ToString("F2") + "s\n";
        }
    }

    void SaveHistory()
    {
        for (int i = 0; i < previousTimes.Count; i++)
        {
            PlayerPrefs.SetFloat("History" + i, previousTimes[i]);
        }
        PlayerPrefs.SetInt("HistoryCount", previousTimes.Count);
        PlayerPrefs.Save();
    }

    void LoadHistory()
    {
        previousTimes.Clear();
        int count = PlayerPrefs.GetInt("HistoryCount", 0);
        for (int i = 0; i < count; i++)
        {
            float time = PlayerPrefs.GetFloat("History" + i, 0);
            previousTimes.Add(time);
        }
        UpdateHistoryUI();
    }
}
