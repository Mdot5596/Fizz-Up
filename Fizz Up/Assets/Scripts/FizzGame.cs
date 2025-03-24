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
    public Transform canFR1Image;
    public GameObject explodedCanImage; 
 

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

                 canFR1Image.localScale = Vector3.one * (1 + fizzLevel / maxFizz * 0.1f); //VIsual shakes can

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

    // Check if the new time qualifies for the top 3
    UpdateBestTimes(timer);
    SaveHistory();  // Save top 3 times
    UpdateHistoryUI();  // Refresh display

    explodedCanImage.SetActive(true); 

    
    StartCoroutine(ShowRestartButtonWithDelay(1.5f));
   }

   private System.Collections.IEnumerator ShowRestartButtonWithDelay(float delay)
   {
    yield return new WaitForSeconds(delay); 
    restartButton.gameObject.SetActive(true); 
   }    



    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }

    // Updates the top 3 best times
    void UpdateBestTimes(float newTime)
    {
    previousTimes.Add(newTime);

    // Sort the list in ascending order (best time first)
    previousTimes.Sort();

    // Keep only the top 3 times
    if (previousTimes.Count > 3)
     {
        previousTimes.RemoveAt(3);
     }
    }

    void UpdateHistoryUI()
    {
    historyText.text = "BEST TIMES\n";
    foreach (float time in previousTimes)
    {
        historyText.text += time.ToString("F2") + "s\n";
    }
    }

void SaveHistory()
{
    for (int i = 0; i < previousTimes.Count; i++)
    {
        PlayerPrefs.SetFloat("BestTime" + i, previousTimes[i]);
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
        float time = PlayerPrefs.GetFloat("BestTime" + i, float.MaxValue);
        previousTimes.Add(time);
    }

    // Ensure the list is sorted and trimmed
    previousTimes.Sort();
    while (previousTimes.Count > 3) previousTimes.RemoveAt(3);

    UpdateHistoryUI();
}
}