using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressamt;
    public Text ScoreText;

    [Header("CompleteGame")]
    public LevelDoor exitDoor;


    void Start()
    {
        progressamt = 0;
        Heart.OnHeartCollect += IncreaseProgressAmount;
        ScoreText.text = progressamt.ToString() + " /4";
    }

    void IncreaseProgressAmount(int amount)
    {
        progressamt += amount;

        ScoreText.text = progressamt.ToString() + " /4";

        if (progressamt >= 4)
        {
            Debug.Log("Game Complete!"); //edit this with UI

            if (exitDoor != null)
            {
                exitDoor.isUnlocked = true;

            }
        }
    }

    void OnDestroy()
    {
        Heart.OnHeartCollect -= IncreaseProgressAmount;
    }
}
