using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBackToStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton("Swim") || OVRInput.GetDown(OVRInput.Button.Any) || OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad)) {
			SceneManager.LoadScene("Main");
		}
        if (Time.timeSinceLevelLoad > 20)
            SceneManager.LoadScene("Main");
	}
}
