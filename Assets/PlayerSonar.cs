using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TransformAndDistance : IComparable
{
    public float distance;
    public Transform transform;
    public int CompareTo(object obj)
    {
        if(obj is TransformAndDistance)
        {
            return this.distance.CompareTo((obj as TransformAndDistance).distance);
        }
        throw new ArgumentException("Object is not a TransformAndDistance");
    }
}

public class PlayerSonar : MonoBehaviour
{

    public float sonarAngle = 60f;
    public GameObject AllFish;
    public float speedOfSoundMultiplier = 5f;
    public AudioClip sonarClip;
    public int numFishToPing = 2;
    public float volumeMultiplier = 20;
	public bool hasPingedFish = false;
	public bool canUseSonar = false;

    [NonSerialized]
    public bool sonarPinging = false;

    [NonSerialized]
    public int NumPings = 0;

    
    private List<AudioSource> audioSources;
    private DateTime lastPlayedTime;
    private AudioSource newSource;

    // Use this for initialization
    void Start()
    {
        audioSources = new List<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorForward = Camera.main.transform.forward;
		if (canUseSonar) {
			if (!sonarPinging) {
				if (Input.GetButton ("SonarPing") || OVRInput.Get (OVRInput.Button.PrimaryTouchpad) || OVRInput.Get (OVRInput.Button.One)) {
					StartPingingSonar (vectorForward);
					NumPings++;
				}
			}
			TimeSpan sinceLastPlayed = DateTime.Now - lastPlayedTime;
			if (sinceLastPlayed.TotalSeconds > 1) {
				sonarPinging = false;
			}
		}
        OVRInput.Update();
    }

    private void StartPingingSonar(Vector3 vectorForward)
    {
        sonarPinging = true;
        lastPlayedTime = DateTime.Now;
        CreateNewAudioSource(0, 1);

        Transform[] allFish = GetFishArray();
        Transform[] fishInFront = CalculateFishInFront(allFish, vectorForward);
        Vector3 playerPosition = Camera.main.transform.position;
        int numFishPinged = 0;
        List<TransformAndDistance> fishInFrontWithDistance = new List<TransformAndDistance>();
        CalculateFishArrayDistance(fishInFront, playerPosition, fishInFrontWithDistance);
        fishInFrontWithDistance.Sort();
        foreach (TransformAndDistance fish in fishInFrontWithDistance)
        {
            if (numFishPinged < numFishToPing)
            {
				hasPingedFish = true;
				CreateNewAudioSource(fish.distance, 1.5f);
                numFishPinged++;
            }
        }
    }

    private static void CalculateFishArrayDistance(Transform[] fishInFront, Vector3 playerPosition, List<TransformAndDistance> fishInFrontWithDistance)
    {
        foreach (Transform fish in fishInFront)
        {
            TransformAndDistance newFishWithDistance = new TransformAndDistance()
            {
                transform = fish,
                distance = Vector3.Distance(fish.position, playerPosition)
            };
            fishInFrontWithDistance.Add(newFishWithDistance);
        }
    }

    private Transform[] GetFishArray()
    {
        int numChildren = AllFish.transform.childCount;
        Transform[] children = new Transform[numChildren];
        for (int i = 0; i < numChildren; i++)
        {
            Transform child = AllFish.transform.GetChild(i);
            children[i] = child;
        }
        return children;
    }

    private Transform[] CalculateFishInFront(Transform[] fishArr, Vector3 vectorForward)
    {
        List<Transform> fishInFront = new List<Transform>();
        foreach(Transform fishTransform in fishArr)
        {
            Vector3 vectorToFish = GetUnitVectorToFish(fishTransform);
            if (Vector3.Angle(vectorToFish, vectorForward) <= sonarAngle)
            {
                fishInFront.Add(fishTransform);
            }
        }
        return fishInFront.ToArray();
    }
    private Vector3 GetUnitVectorToFish(Transform fishTransform)
    {
        Vector3 fishPosition = fishTransform.position;
        Vector3 playerPosition = transform.position;
        Vector3 vectorToFish = fishPosition - playerPosition;
        return vectorToFish.normalized;
    }

    private void CreateNewAudioSource(float distanceToFish, float pitch)
    {
        if(newSource != null)
            if(!newSource.isPlaying)
                newSource.mute = true;
        newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = sonarClip;
        newSource.volume = volumeMultiplier / (distanceToFish);
        newSource.pitch = pitch;
        newSource.PlayDelayed(distanceToFish * (1 / speedOfSoundMultiplier));
    }

}