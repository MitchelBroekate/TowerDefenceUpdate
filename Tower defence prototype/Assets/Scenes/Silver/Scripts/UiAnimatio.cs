using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIScaleAnimator : MonoBehaviour
{
    public RectTransform uiElement;  // Het UI-element (bijv. Button, Panel, etc.)
    public float animationDuration = 0.5f;  // Hoe lang de animatie duurt
    private Vector3 originalScale;  // De originele schaal van het element

    void Start()
    {
        if (uiElement == null)
        {
            uiElement = GetComponent<RectTransform>();  // Zoek automatisch als het niet is ingesteld
        }
        originalScale = uiElement.localScale;  // Bewaar de originele schaal van het element
        uiElement.localScale = Vector3.zero;  // Begin de schaal op 0

        StartScaling();  // Start de animatie direct
    }

    public void StartScaling()
    {
        StartCoroutine(ScaleOverTime(animationDuration));  // Start de schaal-animatie
    }

    IEnumerator ScaleOverTime(float duration)
    {
        float elapsedTime = 0f;  // Houd de verstreken tijd bij
        Vector3 startingScale = uiElement.localScale;  // Start vanaf de huidige schaal (0)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;  // Verhoog de verstreken tijd met elke frame
            float progress = Mathf.Clamp01(elapsedTime / duration);  // Zorg dat de progress tussen 0 en 1 blijft
            uiElement.localScale = Vector3.Lerp(startingScale, originalScale, progress);  // Interpoleer de schaal
            yield return null;  // Wacht op het volgende frame
        }

        uiElement.localScale = originalScale;  // Zorg ervoor dat de schaal precies naar de originele grootte gaat
    }
}