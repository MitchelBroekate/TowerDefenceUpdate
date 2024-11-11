using System.Collections;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text tmpText;                // De TextMeshPro tekst die je wilt verplaatsen
    public Vector3 targetPosition1;         // Het doelpunt van de eerste knop
    public Vector3 targetPosition2;         // Het doelpunt van de tweede knop
    public float moveSpeed = 5f;            // De snelheid waarmee de tekst beweegt
    public RectTransform rectTransform;     // De RectTransform van de tekst
    private Vector3 originalPosition;       // De originele positie van de tekst
    private Vector3 currentTarget;          // De huidige doelpositie waar de tekst heen moet
    private bool isAtOriginalPosition = true; // Controleert of de tekst zich in de originele positie bevindt

    void Start()
    {
        // Verwijzing naar de RectTransform van het TMP tekst object
        rectTransform = tmpText.GetComponent<RectTransform>();

        // De originele positie van de tekst opslaan
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }

        // Beginstand: de tekst staat op de originele positie
        currentTarget = originalPosition;
    }

    // Verplaatst de tekst op basis van de gekozen knop
    public void MoveToPosition(int buttonNumber)
    {
        if (isAtOriginalPosition)
        {
            // Beweeg naar het doelpunt afhankelijk van welke knop is ingedrukt
            if (buttonNumber == 1)
            {
                currentTarget = targetPosition1;
            }
            else if (buttonNumber == 2)
            {
                currentTarget = targetPosition2;
            }
        }
        else
        {
            // Als de tekst niet op de originele positie is, ga terug naar de originele positie
            currentTarget = originalPosition;
        }

        // Start de verplaatsing
        StartCoroutine(Move(rectTransform.anchoredPosition, currentTarget));

        // Wissel de status
        isAtOriginalPosition = !isAtOriginalPosition;
    }

    private IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * moveSpeed;
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }
    }
}