using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider mySlider;

    void Start()
    {
        mySlider = GetComponent<Slider>();

        // Whenever this specific slider moves, it calls the static method on your MusicManager
        mySlider.onValueChanged.AddListener(delegate { MusicManager.SetVolume(mySlider.value); });
    }
}