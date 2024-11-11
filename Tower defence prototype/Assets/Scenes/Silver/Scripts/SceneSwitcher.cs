using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private float delayInSeconds = 1f; // Stel de vertraging in seconden in

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds); // Wacht de aangegeven tijd
        SceneManager.LoadScene(sceneName); // Laad de scène
    }
}