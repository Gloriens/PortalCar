using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public AudioClip backgroundMusic;  
    private AudioSource musicSource;

    // Start is called before the first frame update
    private void Start()
    {
        // Müzik kaynağını başlatıyoruz
        musicSource = gameObject.AddComponent<AudioSource>(); 
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;  
        musicSource.Play();  
    }

    public void playGame()
    {
        
        ResetGame();
        
        // "Game" sahnesini yükle
        SceneManager.LoadScene("Game");
    }

    private void ResetGame()
    {
        
        Time.timeScale = 1; // Zamanı tekrar başlat
        Debug.Log("Oyun sıfırlandı.");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void goMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}