using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGold : MonoBehaviour
{
   private Currency _currency;

   private void Start()
   {
      _currency = Currency.Instance;
   }

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Y))
      {
         _currency.AddCurrency(1000);
      }
   }
}
