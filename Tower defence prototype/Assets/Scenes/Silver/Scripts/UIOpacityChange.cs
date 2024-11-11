using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpacityChanger : MonoBehaviour
{
    [SerializeField] private Image imageToFade; // Het UI Image component
    [SerializeField] private float startOpacity = 0f; // Beginwaarde van de opacity
    [SerializeField] private float endOpacity = 1f; // Eindwaarde van de opacity
    [SerializeField] private float duration = 1f; // De tijd die de fade moet duren

    private bool isFading = false; // Controleert of de animatie al bezig is

    private void Start()
    {
        // Zet de begin-opacity
        SetImageOpacity(startOpacity);
    }

    // Deze functie wordt aangeroepen wanneer de knop wordt geklikt
    public void FadeOpacity()
    {
        if (!isFading)
        {
            StartCoroutine(FadeImage());
        }
    }

    public IEnumerator FadeImage()
    {
        isFading = true;
        float elapsedTime = 0f;

        Color imageColor = imageToFade.color;
        float currentOpacity = imageColor.a; // Huidige alpha waarde

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newOpacity = Mathf.Lerp(currentOpacity, endOpacity, elapsedTime / duration);
            SetImageOpacity(newOpacity);
            yield return null;
        }

        // Zorg ervoor dat de eindwaarde exact wordt ingesteld
        SetImageOpacity(endOpacity);
        isFading = false;
    }

    // Helpt de alpha van de image aan te passen
    private void SetImageOpacity(float opacity)
    {
        Color imageColor = imageToFade.color;
        imageColor.a = Mathf.Clamp01(opacity); // Zorgt ervoor dat alpha tussen 0 en 1 blijft
        imageToFade.color = imageColor;
    }
}