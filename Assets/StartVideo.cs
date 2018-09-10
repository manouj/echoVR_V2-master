using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideo : MonoBehaviour {

    public GameObject VideoObject;

    private VideoPlayer video;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnEnable()
    {
        video = VideoObject.GetComponent<VideoPlayer>();
        video.Play();
    }
}
