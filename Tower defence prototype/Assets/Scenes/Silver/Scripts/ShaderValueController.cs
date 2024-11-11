using System.Collections;
using UnityEngine;

public class ShaderValueController : MonoBehaviour
{
    public Material dissolveMaterial; // Het materiaal met de dissolve shader
    public Material newMaterial; // Het nieuwe materiaal dat je wilt toepassen
    public string dissolveProperty = "_DissolveAmount"; // De dissolve eigenschap in de shader
    public float duration = 3f; // De tijdsduur van de dissolve animatie
    public float timeSpeed = 3; // De snelheid waarmee _FloatTime omhoog gaat
    public MeshRenderer targetRenderer; // De renderer van het object

    private bool hasDissolved = false; // Vlag om bij te houden of de dissolve al gestart is

    private void Start()
    {
        // Start het dissolve proces wanneer het script begint, maar alleen als het nog niet gestart is
        if (!hasDissolved)
        {
            StartCoroutine(DissolveEffect());
            hasDissolved = true; // Markeer als gestart zodat het niet opnieuw gebeurt
        }
    }

    IEnumerator DissolveEffect()
    {
        float elapsedTime = 0f;
        float startValue = 1f; // Begin met een dissolve waarde van 1
        float endValue = 0f;   // Fade naar een dissolve waarde van 0
        float timeValue = 0f;  // Start de tijdswaarde op 0

        // Stap 1: Dissolve van 1 naar 0
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            dissolveMaterial.SetFloat(dissolveProperty, Mathf.Lerp(startValue, endValue, t));

            // Verhoog de tijdswaarde tijdens het dissolve proces
            timeValue += Time.deltaTime * timeSpeed;

            yield return null;
        }

        // Zorg ervoor dat de dissolve-waarde exact op 0 wordt gezet
        dissolveMaterial.SetFloat(dissolveProperty, endValue);


        // Vervang het materiaal van de renderer door het nieuwe materiaal
        targetRenderer.material = newMaterial;
    }
}