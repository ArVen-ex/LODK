using System;
using UnityEngine;

public class Heart : MonoBehaviour, IItem
{

    public static event Action<int> OnHeartCollect;
    public int worth = 1;
    public void Collect()
    {
        OnHeartCollect.Invoke(worth);
        AudioFXManager.Play("Cards");
        Destroy(gameObject);
    }
}
