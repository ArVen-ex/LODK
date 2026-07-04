using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void QuickRestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
