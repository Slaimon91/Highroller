using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaTitlebox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;

    private void Awake()
    {
        int AreaTitleCount = FindObjectsOfType<AreaTitlebox>().Length;
        if (AreaTitleCount > 1)
        {
            foreach(AreaTitlebox titleBox in FindObjectsOfType<AreaTitlebox>())
            {
                if(titleBox != gameObject.GetComponent<AreaTitlebox>())
                {
                    titleBox.Remove();
                }
            }
        }

        StartCoroutine(FadeIn());
    }
    public void AssignInfo(string nameText)
    {
        title.text = nameText;
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(2.5f);

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
        foreach (TextMeshProUGUI texts in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
        {
            texts.color = new Color(texts.color.r, texts.color.g, texts.color.b, 0f);
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
                }
            }
            foreach (TextMeshProUGUI texts in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                texts.color = new Color(texts.color.r, texts.color.g, texts.color.b, texts.color.a + frameA);
                if (currA == 0)
                    currA = texts.color.a;
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
            }
            foreach (TextMeshProUGUI texts in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                texts.color = new Color(texts.color.r, texts.color.g, texts.color.b, texts.color.a - frameA);
                if (currA == 0)
                    currA = texts.color.a;
            }

            yield return null;
        }

        Remove();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
