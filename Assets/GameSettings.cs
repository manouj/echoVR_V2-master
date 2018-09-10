using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public GameObject FishGenerator;
    public GameObject Light;
    public GameObject TimerText;

    public bool isTimed = true;

    private string gameModeInitialText;
    private string volumeInitialText;
    private string amountOfLightInitialText;
    private string numberOfFishInitialText;
    private string timeLimitInitialText;

    private Text[] childrenComponents;

    // Use this for initialization
    void Start ()
    {
        childrenComponents = GetComponentsInChildren<Text>();
        FillInitialTextStrings();
    }

    private void FillInitialTextStrings()
    {
        foreach (Text child in childrenComponents)
        {
            switch (child.name)
            {
                case "GameModeText":
                    gameModeInitialText = child.text;
                    break;
                case "VolumeText":
                    volumeInitialText = child.text;
                    break;
                case "AmountOfLightText":
                    amountOfLightInitialText = child.text;
                    break;
                case "NumberOfFishText":
                    numberOfFishInitialText = child.text;
                    break;
                case "TimeLimitText":
                    timeLimitInitialText = child.text;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		foreach(Text child in childrenComponents)
        {
            switch(child.name)
            {
                case "GameModeText":
                    child.text = gameModeInitialText;
                    break;
                case "VolumeText":
                    child.text = volumeInitialText;
                    break;
                case "AmountOfLightText":
                    child.text = amountOfLightInitialText;
                    break;
                case "NumberOfFishText":
                    child.text = numberOfFishInitialText + GetMaxFish().ToString();
                    break;
                case "TimeLimitText":
                    child.text = timeLimitInitialText + GetTimerText().ToString();
                    break;
            }
        }
	}

    private int GetMaxFish()
    {
        ObjectsController fishGeneratorScript = FishGenerator.GetComponent<ObjectsController>();
        return fishGeneratorScript.maxFish;
    }
    private int GetTimerText()
    {
        TimerCount timerTextScript = TimerText.GetComponent<TimerCount>();
        return timerTextScript.secondsPerGame;
    }
}
