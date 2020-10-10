using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rewardbox : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI introDesc;
    [SerializeField] TextMeshProUGUI rewardDesc;
    private float fadeTime;
    private float currA;
    private float frameA;
    private int fadingMode = 0;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void AssignInfo(string introText, string rewardText, Sprite infoSprite)
    {
        introDesc.text = introText;
        rewardDesc.text = rewardText;
        icon.sprite = infoSprite;
        icon.SetNativeSize();
    }

    public void SetRewardTextColor(Color newColor)
    {
        rewardDesc.color = newColor;
    }

    IEnumerator DelayedDestroy()
    {
        fadingMode = 2;
        yield return new WaitForSeconds(4f);

        StartCoroutine(FadeOut());

        yield return null;
    }

    private IEnumerator FadeIn()
    {
        float fadeTime = 1f;
        float currA = 0f;
        float frameA;
        fadingMode = 1;

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
        fadingMode = 3;
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
 
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (fadingMode < 2)
        {
            StartCoroutine(FadeIn());
        }
        else if (fadingMode >= 2)
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
