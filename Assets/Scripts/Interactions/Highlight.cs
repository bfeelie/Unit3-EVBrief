using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    // Assign renderers - pick any colour
    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color color = Color.red;

    // Helper list to cache all object materials
    private List<Material> materials;

    // Get all materials from each renderer
    private void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            // A single child object could have multiple materials - so needs to be materialS <
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in materials)
            {
                // Enable emission to set the colour
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
        }    
        else
        {
            foreach (var material in materials)
            {
                // Disable if not used anywhere else
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
