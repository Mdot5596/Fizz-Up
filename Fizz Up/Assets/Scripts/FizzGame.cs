using UnityEngine;
using UnityEngine.UI;

public class FizzGame : MonoBehaviour
{
    public Text fizzText;      // Displays the current fizz level
    public Text timerText;     // Displays the time
    public float fizzLevel = 0;
    public float maxFizz = 100;
    private float timer = 0f;
    private bool gameRunning = true;

    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime; // Keep track of time
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
        timerText.text += " - DONE!";
        Debug.Log("Game Over! Time: " + timer.ToString("F2"));
    }
}
