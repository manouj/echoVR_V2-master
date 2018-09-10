using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
	public AudioClip[] prompts;
	public float repeatDelay = 10;
	public ObjectsController objects;
	public PlayerSonar sonar;
	public PlayerMove movement;
	public GameObject headset;
//	public Text debug;
	private int currentPrompt = 0;
	private float timer;
	private Quaternion playerMovement;
	private bool playerExplored = false;
	// Use this for initialization
	void Start () {
		//"You are a baby beluga learning to navigate using echo location and find food"
		GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
	}
	
	// Update is called once per frame
	void Update () {
//		debug.text = UnityEngine.XR.InputTracking.GetLocalRotation (UnityEngine.XR.XRNode.Head).ToString ();
		if (!GetComponent<AudioSource> ().isPlaying && currentPrompt == 0) {
			//			"Press the touch pad to make a call
			sonar.canUseSonar = true;
			currentPrompt++;
			GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
			timer = Time.time + repeatDelay;
		}
		if (currentPrompt == 1) {
			if (Input.GetButton ("SonarPing") || OVRInput.GetDown (OVRInput.Button.PrimaryTouchpad) || OVRInput.GetDown (OVRInput.Button.One)) {
				currentPrompt++;
				//"If you don't hear an echo, there is no food ahead. Look in a different direction and press the touchpad again...
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
				timer = Time.time + repeatDelay;

				playerMovement = headset.transform.rotation;
			}
		}

		if (currentPrompt == 2) {
			//check for movement
			if (Quaternion.Angle (playerMovement, headset.transform.rotation) > 40) {
				Invoke ("CreateFish", 2);	
				currentPrompt+=2;
			} else {
				if (timer < Time.time) {
					GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt+1]);
					timer = Time.time + repeatDelay;
				}
			
			}

		}


		if (!GetComponent<AudioSource> ().isPlaying && currentPrompt == 1) {
			if (timer < Time.time) {
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
				timer = Time.time + repeatDelay;
			}
		}

		if (currentPrompt == 4 && !GetComponent<AudioSource> ().isPlaying) {
			movement.playerCanSwim = true;
			if (sonar.hasPingedFish) {
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
				currentPrompt++;
			}
		}

		if (!GetComponent<AudioSource> ().isPlaying && currentPrompt == 5) {
			if (timer < Time.time) {
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
				timer = Time.time + repeatDelay;
				}

			if (Input.GetButton ("SonarPing") || OVRInput.GetDown (OVRInput.Button.PrimaryTouchpad) || OVRInput.GetDown (OVRInput.Button.One)) {
				//check for still in line with fish/ping
				currentPrompt++;
				playerMovement = headset.transform.rotation;
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);

			}

		}
			
		if (currentPrompt == 6 && !GetComponent<AudioSource> ().isPlaying) {
			//check to see if they looked around
			if (Quaternion.Angle (playerMovement, headset.transform.rotation) > 10 && !playerExplored) {
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt+1]);
				playerExplored = true;
			}
			//did user press trigger
			if(movement.playerHasSwam && !GetComponent<AudioSource> ().isPlaying) {
				//did user eat fish

				if (!movement.MovementParticles.activeSelf &&  GlobalVariables.numFishCollected == 0) {
					//stopped before found fish
					currentPrompt = 8;
					GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);

				}

			}
		}

		if (GlobalVariables.numFishCollected > 0 && !GetComponent<AudioSource> ().isPlaying && currentPrompt < 10) {
			//ate fish
			currentPrompt = 9;
			GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
			currentPrompt++;
			Debug.Log ("Prompt was 9, now it's 10");

		}

		if (currentPrompt == 8 && !GetComponent<AudioSource> ().isPlaying) {
			if (GlobalVariables.numFishCollected > 0) {
				//ate fish
				currentPrompt = 9;
				GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
				currentPrompt++;
				Debug.Log ("Prompt was 8, now it's 10");
			}

		}

		if (currentPrompt == 10 && !GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().PlayOneShot (prompts [currentPrompt]);
			currentPrompt++;
			movement.playerCanSwim = false;
			sonar.canUseSonar = false;
		}


		if (currentPrompt == 11) {
			if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton ("Swim")) {
				//trigger, start playing
				movement.gameObject.GetComponent<GameFunctions>().Play();
				gameObject.SetActive (false);
			}
		
			if (Input.GetButton ("SonarPing") || OVRInput.GetDown (OVRInput.Button.PrimaryTouchpad) || OVRInput.GetDown (OVRInput.Button.One)) {
				//touchpad, return to loading screen
				SceneManager.LoadScene("Main");
				gameObject.SetActive (false);

			}
		}


	}

	void CreateFish() {
		objects.CreateNewFish (headset.transform, 1);
	}
}
