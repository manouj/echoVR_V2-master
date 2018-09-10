using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGazeSelect : MonoBehaviour {
	public Material selectedMaterial;
	public float raycastDistance;
	private VRSelectable selectedObject;
	public GameFunctions functions;
	public GameObject anchor;
	public AudioClip hitSound;
	public AudioClip clickSound;
	public TextMesh debugMessage;
	private string previousObjectSelected;

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && selectedObject != null || selectedObject != null && OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) || Input.GetMouseButtonDown(0) && selectedObject != null) {
			functions.BroadcastMessage (selectedObject.functionToCall);
			GetComponent<AudioSource> ().PlayOneShot (clickSound);

		}
//		debugMessage.text = anchor.transform.rotation.ToString ();
		Debug.DrawRay(anchor.transform.position,anchor.transform.TransformDirection(Vector3.forward) * raycastDistance);
		RaycastHit hit;
		if(Physics.Raycast(anchor.transform.position,  anchor.transform.TransformDirection(Vector3.forward) * raycastDistance, out hit)) {
			
			//raycast hit something new
			if (hit.transform.gameObject.name != previousObjectSelected || !hit.transform.gameObject.GetComponent<VRSelectable>()) {
				if (selectedObject != null) {
					selectedObject.Deselect ();
					selectedObject = null;
				}
			}


			if (hit.transform.GetComponent<VRSelectable> ()) {
				selectedObject = hit.transform.gameObject.GetComponent<VRSelectable>();
				if (previousObjectSelected != hit.transform.name) {
					selectedObject.Select (selectedMaterial);
					GetComponent<AudioSource> ().PlayOneShot (hitSound);
					previousObjectSelected = hit.transform.name;
				}

			}
		}
	}
}
