using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceHeight = 0.25f; 
    public float bounceSpeed = 2f;     

    private Vector3 startPos;

    void Start()
    {
        
        startPos = transform.position;
    }

    void Update()
    {
        
        float newY = startPos.y + (Mathf.Sin(Time.time * bounceSpeed) * bounceHeight);

        
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}