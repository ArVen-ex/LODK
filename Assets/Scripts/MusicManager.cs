using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }

       
        instance = this;
        audioSource = GetComponent<AudioSource>();

        
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }
    }

    public static void SetVolume(float volume)
    {
        instance.audioSource.volume = volume;
    }

    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            instance.audioSource.clip = audioClip;
        }
        if (instance.audioSource.clip != null)
        {
            if (resetSong)
            {
                instance.audioSource.Stop();
            }
            instance.audioSource.Play();
        }
    }

    public static void PauseBackgroundMusic()
    {
        instance.audioSource.Pause();
    }

}
