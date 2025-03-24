using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;  // Reference to the AudioSource
    public FizzGame fizzGame;  // Reference to the FizzGame script

    public float maxPitch = 2f;  // Maximum pitch when fully shaken
    public float normalPitch = 1f;  // Default pitch

    void Start()
    {
        music.Play();
    }

    void Update()
    {
        // Ensure music pitch increases as fizz level rises
        float pitch = Mathf.Lerp(normalPitch, maxPitch, fizzGame.fizzLevel / fizzGame.maxFizz);
        music.pitch = pitch;
    }
}
