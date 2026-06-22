using UnityEngine;
using UnityEngine.UI;

public class AudioFXManager : MonoBehaviour
{

    private static AudioFXManager instance;

    private static AudioSource audioSource;
    private static AudioFXLibrary library;
    [SerializeField] private Slider SFXslider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            library = GetComponent<AudioFXLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else {
           Destroy(gameObject);
        }
    
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = library.GetRandomClip(soundName);
        if (audioClip != null) { 
        
        audioSource.PlayOneShot(audioClip);
        }   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFXslider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

   public static void SetVolume(float volume)
    {
        audioSource.volume = volume;

    }

    public  void OnValueChanged()
    {
        SetVolume(SFXslider.value);
    }
}
