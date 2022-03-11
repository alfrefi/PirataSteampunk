using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingBehavior : MonoBehaviour
{
    [SerializeField, ColorUsage(showAlpha: true, hdr: true)]
    Color NormalColor;

    [SerializeField, ColorUsage(showAlpha: true, hdr: true)]
    Color GlowColor;

    private SpriteRenderer spriteRenderer;

    public bool DoGlow = false;
    public bool DoDeGlow = false;
    public bool DoGlowDeGlow = false;

    public float glowForSecondsTime = 2f;
    public float timeForFullGlow = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( DoGlow )
        {
            StartCoroutine(Glow());
        }

        if ( DoDeGlow )
        {
            StartCoroutine(DeGlow());
        }

        if ( DoGlowDeGlow )
        {
            StartCoroutine(GlowDeglow());
        }
    }

    IEnumerator Glow()
    {
        DoGlow = false;

        float elapsedTime = 0f;
        while ( elapsedTime < timeForFullGlow )
        {
            Color color = Color.Lerp(spriteRenderer.material.GetColor("_HDRColor"), GlowColor, elapsedTime/timeForFullGlow);
            spriteRenderer.material.SetColor("_HDRColor", color);

            elapsedTime += Time.deltaTime;
            yield return false;
        }

        yield return false;
    }

    IEnumerator DeGlow()
    {
        DoDeGlow = false;

        float elapsedTime = 0f;
        while ( elapsedTime < timeForFullGlow )
        {
            Color color = Color.Lerp(spriteRenderer.material.GetColor("_HDRColor"), NormalColor, elapsedTime/timeForFullGlow);
            spriteRenderer.material.SetColor("_HDRColor", color);

            elapsedTime += Time.deltaTime;
            yield return false;
        }

        yield return false;
    }

    IEnumerator GlowDeglow()
    {
        DoGlowDeGlow = false;

        float elapsedTime = 0f;
        while ( elapsedTime < glowForSecondsTime / 2 )
        {
            Color color = Color.Lerp(spriteRenderer.material.GetColor("_HDRColor"), GlowColor, elapsedTime/(glowForSecondsTime/2));
            spriteRenderer.material.SetColor("_HDRColor", color);

            elapsedTime += Time.deltaTime;
            yield return false;
        }

        elapsedTime = 0f;
        while ( elapsedTime < glowForSecondsTime / 2 )
        {
            Color color = Color.Lerp(spriteRenderer.material.GetColor("_HDRColor"), NormalColor, elapsedTime/(glowForSecondsTime/2));
            spriteRenderer.material.SetColor("_HDRColor", color);

            elapsedTime += Time.deltaTime;
            yield return false;
        }

        yield return false;
    }
}
