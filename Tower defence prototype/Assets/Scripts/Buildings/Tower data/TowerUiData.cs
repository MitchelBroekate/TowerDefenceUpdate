using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TowerUIData", menuName = "TowerUIData")]
public class TowerUiData : ScriptableObject
{
  public Sprite icon;
  public String buildingName;
  public String descriptionText;
  public String upgradeCostText;
}
