using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCollider : MonoBehaviour {
	public GameFunctions functions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<VRSelectable> ()) {
			functions.BroadcastMessage (other.gameObject.GetComponent<VRSelectable>().functionToCall);

		}
	}

}
