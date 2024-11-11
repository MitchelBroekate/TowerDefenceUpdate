using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlyIn : MonoBehaviour
{
    public RectTransform panel;
    public GameObject targetObject;  // Het GameObject waar het paneel naartoe moet bewegen
    public GameObject offScreenObject; // Het GameObject dat buiten het scherm staat
    public Button triggerButton;
    public float flyInSpeed = 500f;
    public bool isPanelVisible = false;
    public KeyCode activatePanel;
    private Coroutine movePanelCoroutine;

    void Start()
    {
        panel.anchoredPosition = offScreenObject.GetComponent<RectTransform>().anchoredPosition;
        triggerButton.onClick.AddListener(TogglePanel);
    }

    void Update()
    {
        if (Input.GetKeyDown(activatePanel))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        if (isPanelVisible)
        {
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenObject.GetComponent<RectTransform>().anchoredPosition));
        }
        else
        {
            movePanelCoroutine = StartCoroutine(MovePanel(targetObject.GetComponent<RectTransform>().anchoredPosition));
        }

        isPanelVisible = !isPanelVisible;
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        while (Vector3.Distance(panel.anchoredPosition, destination) > 0.1f)
        {
            panel.anchoredPosition = Vector3.MoveTowards(panel.anchoredPosition, destination, flyInSpeed * Time.deltaTime);
            yield return null;
        }

        panel.anchoredPosition = destination;
        movePanelCoroutine = null;
    }
}
