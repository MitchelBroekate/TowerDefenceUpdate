using System.Collections;
using TMPro;
using UnityEngine;

public class DeathScreenUiText : MonoBehaviour
{
    public TMP_Text textComponent;  // Sleep hier je TextMeshPro Text in
    public float typingSpeed = 0.05f;  // De snelheid waarmee de tekst wordt getypt
    public float startDelay = 1.0f;  // De vertraging voordat het typen begint

    private string fullText;  // Volledige tekst die moet worden weergegeven
    private string currentText = "";  // Huidige weergave van de tekst

    void Start()
    {
        fullText = textComponent.text;  // Haal de volledige tekst op die moet worden getypt
        textComponent.text = "";  // Maak het Text veld leeg om het typ-effect te starten
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(startDelay);  // Wacht de ingestelde tijd voor de delay

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);  // Wacht tussen elke letter
        }
    }
}