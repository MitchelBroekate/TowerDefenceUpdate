using UnityEngine;
using UnityEngine.UI;

public class UILights : MonoBehaviour
{
    public Light targetLight; 
    public Slider intensitySlider; 

    void Start()
    {
        if (targetLight != null && intensitySlider != null)
        {
            intensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
        }
    }

    public void UpdateLightIntensity(float value)
    {
        targetLight.intensity = value;
    }
}