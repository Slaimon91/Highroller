using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    Image foreground;
    private void Start()
    {
        foreground = GetComponent<Image>();
        //StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        foreground.enabled = true;
        float speed = 0.75f;
        float alpha = foreground.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / speed)
        {
            Color newColor = new Color(44f/255f, 40f/255f, 27f/255f, Mathf.Lerp(alpha, 0f, t));
            foreground.color = newColor;
            yield return null;
        }
        foreground.enabled = false;
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        foreground.enabled = true;
        float speed = 0.75f;
        float alpha = foreground.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / speed)
        {
            Color newColor = new Color(44f / 255f, 40f / 255f, 27f / 255f, Mathf.Lerp(alpha, 1f, t));
            foreground.color = newColor;
            yield return null;
        }
        yield return null;
    }
}
