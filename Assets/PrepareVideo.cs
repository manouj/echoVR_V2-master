using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PrepareVideo : MonoBehaviour {

    public GameObject VideoObject;

    private VideoPlayer video;

    // Use this for initialization
    void Start()
    {
        video = VideoObject.GetComponent<VideoPlayer>();
        video.Prepare();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        
    }
}
