using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    public float minAlpha = 0.3f;
    public float maxAlpha = 0.7f;
    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child != transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material[] materials = renderer.materials;
                    foreach (Material material in materials)
                    {
                        float randomAlpha = Random.Range(minAlpha, maxAlpha);
                        Color color = material.color;
                        color.a = randomAlpha;
                        material.color = color;
                    }
                }
            }
        }
    }
}
