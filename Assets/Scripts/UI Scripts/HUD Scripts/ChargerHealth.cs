using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Charger Stats")]
    public ChargerBar chargerBar;
    public int currentEnergy = 50;
    public int maxEnergy = 50;
    [HideInInspector] public bool isEmpty;

    [Header("Particles")]
    public ParticleSystem _zapParticles;
    public GameObject[] zapParticles;
    public int zapIndex = 0;

    public void Start()
    {
        currentEnergy = maxEnergy;
        chargerBar.SetMaxEnergy(maxEnergy);
    }

    public void TakeEnergy(int energy)
    {
        currentEnergy -= energy;
        chargerBar.SetEnergy(currentEnergy);

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            isEmpty = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if it's a petrol station
        if (other.gameObject.GetComponent<Player_Energy>())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _zapParticles.Play();
                Debug.Log("Turned on particles");
            }
        }

        else
        {
            Debug.Log("Didn't start particles. D'oh");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if it's a petrol station
        if (other.gameObject.GetComponent<Player_Energy>())
        {
            _zapParticles.Stop();
        }

        else
        {
            Debug.Log("Didn't stop particles. D'oh");
        }
    }
}
