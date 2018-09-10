using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialClickThrough : MonoBehaviour
{

    public int SlideIndexToStart = 0;

    public GameObject[] Slides;

    public AudioClip SonarSound;

    private int currentSlideIndex;

    private bool indexButtonWentDown = false;
    private bool touchpadButtonWentDown = false;

    // Use this for initialization
    void Start()
    {
        currentSlideIndex = SlideIndexToStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (indexButtonWentDown)
        {
            if (!(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton("Swim")))
            {
                indexButtonWentDown = false;
                Slides[currentSlideIndex].SetActive(false);
                currentSlideIndex++;
                if (currentSlideIndex < Slides.Length)
                {
                    Slides[currentSlideIndex].SetActive(true);
                }
                else
                {
                    GlobalVariables.numFishCollected = 0;
                    GlobalVariables.numOctoCollected = 0;
                    SceneManager.LoadScene("Main");
                }
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton("Swim"))
            {
                indexButtonWentDown = true;
            }
        }
    }
}
