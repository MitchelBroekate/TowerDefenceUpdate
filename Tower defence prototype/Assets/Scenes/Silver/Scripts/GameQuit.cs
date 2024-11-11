using UnityEngine;
using UnityEngine.UI;

public class GameQuit : MonoBehaviour
{
    public Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(CloseGame);
    }

    // Functie om de game te sluiten
    void CloseGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
