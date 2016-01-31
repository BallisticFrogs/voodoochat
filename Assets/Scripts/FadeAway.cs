using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeAway : MonoBehaviour
{
    public float waitUntilFade;
    public float fadeDuration;

    private bool fadeStarted = false;
    private Text textToFadeOut;

    void Start()
    {
        textToFadeOut = GetComponent<Text>();
    }

	// Update is called once per frame
    private void Update()
    {
        if (fadeStarted)
        {
            Color color = textToFadeOut.color;
            color.a -= 1/(fadeDuration*60);
            textToFadeOut.color = color;
            if (textToFadeOut.color.a <= 0) ;

        } else if (Time.time > waitUntilFade)
        {
            fadeStarted = true;
        }
        
    }
}

