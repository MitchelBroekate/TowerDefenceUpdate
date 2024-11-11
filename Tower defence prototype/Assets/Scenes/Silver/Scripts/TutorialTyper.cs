using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTyper : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] tutorialTexts;
    public GameObject[] tutorialImages; // Array of images corresponding to tutorial text indexes
    public int disableClickIndex = 5;
    public float typingSpeed = 0.05f;
    public Button continueButton;
    public RectTransform alles;
    public int continuePlacedTurretIndex = 7;
    public bool continuePlacedTurret = false;
    private int index = 0;
    private bool isTyping = false;
    private bool canClickToSkip = true;
    private Coroutine typingCoroutine;

    void Start()
    {
        continueButton.onClick.AddListener(AllowNextText);
        UpdateImage(); // Ensure the correct image is displayed at the start
        StartTyping();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClickToSkip)
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (CanProceed())
            {
                NextText();
            }
        }

        if (index == disableClickIndex && Input.GetKeyDown(KeyCode.Tab))
        {
            AllowNextText();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EndTutorial();
        }
    }

    void StartTyping()
    {
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        textDisplay.text = "";
        foreach (char letter in tutorialTexts[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;

        if (index == disableClickIndex)
        {
            DisableClickToSkip();
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            canClickToSkip = true;
        }
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textDisplay.text = tutorialTexts[index];
        isTyping = false;
    }

    public void NextText()
    {
        if (index < tutorialTexts.Length - 1)
        {
            index++;
            Debug.Log("NextText: Moving to index " + index);

            UpdateImage(); // Immediately update image before starting to type the new text
            StartTyping();
        }
        else
        {
            Debug.Log("Tutorial Finished");
        }
    }

    public void AllowNextText()
    {
        if (index == disableClickIndex)
        {
            NextText();
        }
        else if (CanProceed())
        {
            NextText();
        }
    }

    public void DisableClickToSkip()
    {
        canClickToSkip = false;
    }

    private bool CanProceed()
    {
        if (index == continuePlacedTurretIndex && !continuePlacedTurret)
        {
            return false;
        }

        if (index == disableClickIndex)
        {
            return false;
        }

        return true;
    }

    private void UpdateImage()
    {
        // Deactivate all images first
        foreach (GameObject image in tutorialImages)
        {
            if (image != null)
            {
                image.SetActive(false);
            }
        }

        // Activate the image corresponding to the current text index, if it exists
        if (index < tutorialImages.Length && tutorialImages[index] != null)
        {
            Debug.Log("UpdateImage: Activating image for index " + index);
            tutorialImages[index].SetActive(true);
        }
        else
        {
            Debug.Log("UpdateImage: No image for index " + index);
        }
    }

    public void EndTutorial()
    {
        textDisplay.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        alles.gameObject.SetActive(false);

        // Deactivate all images when the tutorial ends
        foreach (GameObject image in tutorialImages)
        {
            if (image != null)
            {
                image.SetActive(false);
            }
        }
    }
}
