using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject GameOverScreen;


    void Start()
    {
        // Ensure the screen is hidden when the level starts
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(false);
        }

        // Start listening for the death event
        DeathZone.OnPlayerDeath += GameOver;
    }
    public void restartGame()
    {
        Time.timeScale = 1f;
        MusicManager.PlayBackgroundMusic(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        
        if (GameOverScreen != null)
        {
            
            GameOverScreen.SetActive(true);
            MusicManager.PauseBackgroundMusic();
            Time.timeScale = 0f;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    void OnDestroy()
    {
        DeathZone.OnPlayerDeath -= GameOver;
    }
}
