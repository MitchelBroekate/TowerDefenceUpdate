using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelPositionAndScaleToggle : MonoBehaviour
{
    public RectTransform panel;
    public Vector3 targetPosition;
    public Vector3 targetScale = Vector3.one;
    public KeyCode toggleKey = KeyCode.Tab;
    public float transitionSpeed = 5f;
    public Button toggleButton;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isAtTargetPosition = false;
    private Coroutine moveCoroutine;

    void Start()
    {
        originalPosition = panel.anchoredPosition;
        originalScale = panel.localScale;

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePanelPositionAndScale);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            TogglePanelPositionAndScale();
        }
    }

    void TogglePanelPositionAndScale()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        if (isAtTargetPosition)
        {
            moveCoroutine = StartCoroutine(MovePanel(originalPosition, originalScale));
        }
        else
        {
            moveCoroutine = StartCoroutine(MovePanel(targetPosition, targetScale));
        }

        isAtTargetPosition = !isAtTargetPosition;
    }

    IEnumerator MovePanel(Vector3 destinationPosition, Vector3 destinationScale)
    {
        while (Vector3.Distance(panel.anchoredPosition, destinationPosition) > 0.1f ||
               Vector3.Distance(panel.localScale, destinationScale) > 0.01f)
        {
            panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, destinationPosition, Time.deltaTime * transitionSpeed);
            panel.localScale = Vector3.Lerp(panel.localScale, destinationScale, Time.deltaTime * transitionSpeed);
            yield return null;
        }

        panel.anchoredPosition = destinationPosition;
        panel.localScale = destinationScale;
    }
}
