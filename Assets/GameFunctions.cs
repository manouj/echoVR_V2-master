using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctions : MonoBehaviour {
	public PlayerMove playerMovement;
	public PlayerSonar sonar;
	public TimerCount timer;
	public ObjectsController objects;
	public GameObject selectableItemsHolder;
	public GameObject playerGui;
	public GameObject tutorial;

	// Use this for initialization
	void Start () {
		//loop ocean sounds
		playerMovement.playerCanSwim = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play() {
		Debug.Log ("Running Play");
		deactivateSelectableItems ();
		objects.CreateFish (500);
		playerGui.SetActive (true);
		GlobalVariables.numFishCollected = 0;
		playerMovement.playerCanSwim = true;
		playerMovement.resetSwimming ();
		sonar.canUseSonar = true;
		timer.Play ();
	}

	public void Tutorial() {
		Debug.Log ("Running Tutorial");
//		activateSelectableItems ();
		deactivateSelectableItems ();
		playerMovement.playerCanSwim = false;
		playerMovement.resetSwimming ();
		sonar.canUseSonar = false;
		tutorial.SetActive(true);
	}

	private void deactivateSelectableItems() {
		VRSelectable[] selectables = selectableItemsHolder.GetComponentsInChildren<VRSelectable> ();
		foreach(VRSelectable selectable in selectables) {
			selectable.transform.parent.gameObject.SetActive (false);
		}
	}

	private void activateSelectableItems() {
		VRSelectable[] selectables = selectableItemsHolder.GetComponentsInChildren<VRSelectable> ();
		foreach(VRSelectable selectable in selectables) {
			selectable.transform.parent.gameObject.SetActive (true);
		}

	}

}
