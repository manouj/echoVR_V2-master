using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour {

    public GameObject[] GoalObjects;
    public float WorldSizeX = 20f;
    public float WorldSizeY = 20f;
    public float WorldSizeZ = 20f;
    public int maxFish = 3;

    private AudioSource capturedSource;

    private GameObject player;
   
    private float lastTime;
    private float startTime;
    private Queue<GameObject> objectsToPlay;
    private Queue<GameObject> waitingObjectsToPlay;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        capturedSource = transform.GetComponent<AudioSource>();
        objectsToPlay = new Queue<GameObject>();
        waitingObjectsToPlay = new Queue<GameObject>();
        lastTime = Time.time;
        startTime = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
    }

	public void CreateFish(int numFish) {
		for (int i = 0; i < numFish; i++) {
			CreateNewFish ();
		}

	}

    public void HandleCollision(GameObject collidedObject)
    {
        capturedSource.Play();
        Debug.Log("Captured");
        CreateNewFish();
        Destroy(collidedObject);
    }

    public GameObject CreateNewFish()
    {
        int fishChoiceIndex = Random.Range(0, GoalObjects.Length);
        GameObject newGoalObject = Instantiate(GoalObjects[fishChoiceIndex], transform);
        float newX = Random.Range(-WorldSizeX / 2f, WorldSizeX / 2f);
        float newY = Random.Range(0, WorldSizeY);
        float newZ = Random.Range(-WorldSizeZ / 2f, WorldSizeZ / 2f);
        newGoalObject.transform.localPosition = new Vector3(newX, newY, newZ);
        newGoalObject.transform.Rotate(new Vector3(0f, 1f, 0f), Random.Range(0, 360));
        newGoalObject.SetActive(true);
        if (objectsToPlay.Count > 0)
            objectsToPlay.Enqueue(newGoalObject);
        else
            waitingObjectsToPlay.Enqueue(newGoalObject);
        return newGoalObject;
    }

	public GameObject CreateNewFish(Transform position, int fishChoice)
	{
		int fishChoiceIndex = Random.Range(0, GoalObjects.Length);
		GameObject newGoalObject = Instantiate(GoalObjects[fishChoice], position);
		newGoalObject.transform.parent = transform;
//		newGoalObject.transform.localPosition = new Vector3(newX, newY, newZ);
//		newGoalObject.transform.Rotate(new Vector3(0f, 1f, 0f), Random.Range(0, 360));
		newGoalObject.SetActive(true);
		return newGoalObject;
	}



}
