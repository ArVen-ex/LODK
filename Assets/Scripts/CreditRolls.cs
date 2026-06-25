using UnityEngine;

public class CreditRoller : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 50f; // How fast the text moves up

    private RectTransform rectTransform;

    void Start()
    {
        // Grab the RectTransform (UI version of a Transform)
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Continuously move the text upwards on the Y axis
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}