using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * speed;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
