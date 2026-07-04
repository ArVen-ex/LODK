using System;
using UnityEngine;

public class Heart : MonoBehaviour, IItem
{

    public static event Action<int> OnHeartCollect;
    public int worth = 1;

    private bool isCollected = false; //to be used as a checker so the trigger doesnt happen twice before the object is destroyed
    public void Collect()
    {
        if (isCollected)
        {
            return;
        }
        isCollected = true;
        OnHeartCollect.Invoke(worth);
        AudioFXManager.Play("Cards");
        Destroy(gameObject);
    }
}
