using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour {

    public GameObject Objects;

//    public GameObject FishScoreTextObject;
//	public GameObject OctoScoreTextObject;
	public TextMesh fishScore;
	public TextMesh octopusScore;

    private GameObject collidedObject;

    void Start() {

    }

    void Update() {

	}

    void OnTriggerEnter(Collider other) {
        Debug.Log("trigger entered");
        collidedObject = other.gameObject;
        Transform collidedParent = collidedObject.transform.parent;
        if (collidedParent.gameObject == Objects)
        {
            ObjectsController objectsController = (ObjectsController) Objects.GetComponent(typeof(ObjectsController));
            objectsController.HandleCollision(collidedObject);
        }
		if (collidedObject.name.Substring (0, 4) == "Fish") {
			GlobalVariables.numFishCollected++;
			Debug.Log("Number of Fish Collected Now : " + GlobalVariables.numFishCollected);
		}
		
		if (collidedObject.name.Substring (0, 4) == "Octo") {
			GlobalVariables.numOctoCollected++;
		}
//        Text fishScoreText = FishScoreTextObject.GetComponent<Text>();
		fishScore.text = GlobalVariables.numFishCollected.ToString("D2");
  //      Text octoScoreText = OctoScoreTextObject.GetComponent<Text>();
        octopusScore.text = GlobalVariables.numOctoCollected.ToString("D2");
    }
}
