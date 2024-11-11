using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // Voor EventSystem

public class TutorialUiScript : MonoBehaviour
{
    public RectTransform panel;         // De RectTransform van het UI-paneel dat moet bewegen
    public float panelWidth = 800f;     // De breedte van het paneel (pas deze aan aan je paneel)
    public Button triggerButton;        // De knop die de animatie triggert
    public float flyInSpeed = 500f;     // Snelheid waarmee het paneel naar de doelpositie beweegt
    public bool isPanelVisible = false; // Houdt bij of het paneel zichtbaar is of niet
    public KeyCode activatePanel;       // Toets om het paneel te activeren
    private Vector3 offScreenPosition;  // De startpositie van het paneel (links buiten het scherm)
    private Vector3 targetPosition;     // De doelpositie waar het paneel heen moet
    private Coroutine movePanelCoroutine; // Referentie naar de lopende coroutine

    void Start()
    {
        // Zet de beginpositie van het paneel links buiten het scherm
        offScreenPosition = new Vector3(-panelWidth, 0f, 0f);
        panel.anchoredPosition = offScreenPosition;

        // Stel de doelpositie in om het paneel op het scherm te plaatsen
        targetPosition = Vector3.zero; // Of pas dit aan naar de gewenste positie op het scherm

        // Voeg een listener toe voor de triggerButton
        triggerButton.onClick.AddListener(TogglePanel);
    }

    void Update()
    {
        // Zorg ervoor dat de invoer niet verwerkt wordt als de knop ingedrukt wordt
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == triggerButton.gameObject)
        {
            return; // Als de knop is geselecteerd (klikken of focus), doe niets
        }

        // Luister naar de activatePanel toets als de knop niet is geselecteerd
        if (Input.GetKeyDown(activatePanel))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        // Stop de huidige animatie als die loopt
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        if (isPanelVisible)
        {
            // Beweeg het paneel terug naar buiten het scherm
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
        }
        else
        {
            // Beweeg het paneel naar de doelpositie
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition));
        }

        isPanelVisible = !isPanelVisible;  // Wissel de zichtbaarheid status
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        while (Vector3.Distance(panel.anchoredPosition, destination) > 0.1f)
        {
            // Beweeg de positie van het paneel richting de doelpositie met een bepaalde snelheid
            panel.anchoredPosition = Vector3.MoveTowards(panel.anchoredPosition, destination, flyInSpeed * Time.deltaTime);
            yield return null;  // Wacht een frame voordat je verdergaat
        }

        // Zorg ervoor dat de positie precies gelijk is aan de doelpositie
        panel.anchoredPosition = destination;
        movePanelCoroutine = null;  // Reset de coroutine referentie wanneer klaar
    }
}
