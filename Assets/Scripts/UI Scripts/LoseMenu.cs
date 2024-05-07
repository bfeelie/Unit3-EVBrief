using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    EnergyBar EnergyBar;

    //On Awake, set Lose Menu to hidden, get ElectricityBar script
    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        //GetComponent<ElectricityBar>().LoseEvent();
    }

    public void ToggleLoseScreen(int energyMin)
    {
        //if()
        //{

        //}
    }
}