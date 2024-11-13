using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurrency : MonoBehaviour
{
    [SerializeField] Currency currency;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currency.AddCurrency(1000);
        }
    }
}
