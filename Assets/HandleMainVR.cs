using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMainVR : MonoBehaviour {

    public GameObject DotInactive;
    public GameObject DotActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void onOver()
    {
        DotInactive.SetActive(true);
    }

    void onOut()
    {
        DotInactive.SetActive(false);
        DotActive.SetActive(false);
    }
}
