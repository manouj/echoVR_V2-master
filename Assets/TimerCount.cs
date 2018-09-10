using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour {

    public int secondsPerGame = 90;

    private int timeLeft;
//    private Text text;
    private DateTime lastTick;
	public TextMesh timer;
	bool playing = false;
	void Start () {
    }
	
	void Update () {
		
	}

	public void Play() {
		timeLeft = secondsPerGame;
//		text = GetComponent<Text>();
		lastTick = DateTime.Now;
//		text.text = timeLeft.ToString("D2");
		timer.text = timeLeft.ToString ("D2");
		playing = true;

	}

    private void FixedUpdate()
    {
		if (playing) {
			TimeSpan timeSinceLastTick = DateTime.Now - lastTick;
			if (timeSinceLastTick > TimeSpan.FromSeconds (1)) {
				lastTick = DateTime.Now;
				timeLeft--;
				timer.text = timeLeft.ToString ("D2");
//				text.text = timeLeft.ToString ("D2");

				if (timeLeft <= 0) {
					playing = false;
					SceneManager.LoadSceneAsync ("GameOverScreen");
				
				}
			}
		}
	}
}
