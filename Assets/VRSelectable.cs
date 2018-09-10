using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSelectable : MonoBehaviour {
	private Material startingMaterial;
	public string functionToCall;
	// Use this for initialization
	void Start () {
		if (GetComponent<MeshRenderer> ()) {
			startingMaterial = gameObject.GetComponent<MeshRenderer> ().material;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Select(Material newMaterial) {
			gameObject.GetComponent<MeshRenderer> ().material = newMaterial;
	}

	public void Deselect() {
			gameObject.GetComponent<MeshRenderer> ().material = startingMaterial;
	}
}
