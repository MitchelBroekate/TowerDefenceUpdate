using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LvlSelect : MonoBehaviour
{
    public bool placedFirstTurret = false;
    public Button placeTurretButton;

    void Start()
    {
        placeTurretButton.onClick.AddListener(PlaceFirstTurret);
    }

    // This method sets placedFirstTurret to true when the button is clicked
    void PlaceFirstTurret()
    {
        placedFirstTurret = false;
    }
}