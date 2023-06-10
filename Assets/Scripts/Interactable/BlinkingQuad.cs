using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingQuad : MonoBehaviour

    
{
    public float BlinkInterval = 0.5f;
    private Renderer quadRenderer;
    private bool isVisible = true;

    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            isVisible = !isVisible;
            quadRenderer.enabled = isVisible;
            yield return new WaitForSeconds(BlinkInterval);
        }
    }
}
