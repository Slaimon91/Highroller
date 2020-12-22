using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI introDesc;
    private float fadeWaitTime;
    private float fadeTime;
    private float currA;
    private float frameA;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void AssignInfo(string introText,  float waitTime)
    {
        if(introText != "")
            introDesc.text = introText;
        fadeWaitTime = waitTime;
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(fadeWaitTime);

        StartCoroutine(FadeOut());

        yield return null;
    }

    private IEnumerator FadeIn()
    {
        float fadeTime = 1f;
        float currA = 0f;
        float frameA;

        List<Image> imagesList = new List<Image>();

        foreach (Image images in gameObject.GetComponentsInChildren<Image>())
        {
            if (images.color.a != 0f)
            {
                images.color = new Color(images.color.r, images.color.g, images.color.b, 0f);
                imagesList.Add(images);
            }
        }
        if(gameObject.GetComponentsInChildren<TextMeshProUGUI>() != null)
        {
            foreach (TextMeshProUGUI texts in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                texts.color = new Color(texts.color.r, texts.color.g, texts.color.b, 0f);
            }
        }


        while (currA < 1.0f)
        {
            frameA = (Time.deltaTime / fadeTime);
            currA = 0f;
            foreach (Image images in imagesList)
            {
                if (images.gameObject.GetComponent<Rewardbox>() == null)
                {
                    images.color = new Color(images.color.r, images.color.g, images.color.b, images.color.a + frameA);
                    if (currA == 0)
                        currA = images.color.a;
                }
            }
            if (gameObject.GetComponentsInChildren<TextMeshProUGUI>() != null)
            {
                foreach (TextMeshProUGUI texts in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    texts.color = new Color(texts.color.r, texts.color.g, texts.color.b, texts.color.a + frameA);
                    if (currA == 0)
                        currA = texts.color.a;
                }
            }

            yield return null;
        }
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator FadeOut()
    {
        float fadeTime = 1f;
        float currA = 255f;
        float frameA;
        while (currA > 0.0f)
        {
            frameA = (Time.deltaTime / fadeTime);
            currA = 0f;
            foreach (Image images in gameObject.GetComponentsInChildren<Image>())
            {
                images.color = new Color(images.color.r, images.color.g, images.color.b, images.color.a - frameA);
                if (currA == 0)
                    currA = images.color.a;
            }
            foreach (TextMeshProUGUI texts in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                texts.color = new Color(texts.color.r, texts.color.g, texts.color.b, texts.color.a - frameA);
                if (currA == 0)
                    currA = texts.color.a;
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
