using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortraitChange : MonoBehaviour
{
    public float buildingCost;
    public Sprite enoughCurrency;
    public Sprite insufficientCurrency;
    public Image portraitImage;

    void Update()
    {
        if (Currency.Instance.currency >= buildingCost)
        {
            portraitImage.sprite = enoughCurrency;
        }
        else
        {
            portraitImage.sprite = insufficientCurrency;
        }
    }
}
