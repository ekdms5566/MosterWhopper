using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInManager : MonoBehaviour
{
    Image image;
    public float speed = 0.007f;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        if (gameObject.activeSelf && image.name == "Title") 
            Invoke("StartFadeIn", 2f);
        if (gameObject.activeSelf &&
            (image.name == "StartButton" || image.name == "ResumeButton" ||image.name=="OptionButton"|| image.name == "ExitButton" || image.name == "Option"))
            Invoke("StartFadeIn", 3f);
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Debug.Log("코루틴 함수 호출");
        float fadeCount = 0;
        while (fadeCount<1.0f)
        {
            fadeCount += 0.007f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, fadeCount);
        }
    }
}
