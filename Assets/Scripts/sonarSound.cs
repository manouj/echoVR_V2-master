using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonarSound : MonoBehaviour {


    public AudioSource sonar;
    public bool isTriggered;
	// Use this for initialization
	void Start () {
        sonar = GetComponent<AudioSource>();
        isTriggered = false;

    }
	
	// Update is called once per frame


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isTriggered = true;
            if (isTriggered)
            {
                sonar.Play();
            }
        }

    }
}
