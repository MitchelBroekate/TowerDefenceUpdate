using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    public static Currency Instance;
    
    public float currency;
    private int currencyToShow;
    public TMP_Text currencyText;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currencyText.text = currency.ToString() + "$";
    }
    public void SubtractCurrency(float amount)
    {
        currency -= amount;
        currencyToShow = (int)Math.Round(currency);
        currencyText.text = currencyToShow.ToString() + "$";
    }
    public void AddCurrency(float amount)
    {
        currency += amount;
        currencyToShow = (int)Math.Round(currency);
        currencyText.text = currencyToShow.ToString() + "$"; 
    }
}
