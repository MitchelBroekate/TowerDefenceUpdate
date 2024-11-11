using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    public Toggle fullScreenToggle;

    void Start()
    {

        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}