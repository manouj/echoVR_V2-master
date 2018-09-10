using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class PlayerMove : MonoBehaviour
{
	public bool playerCanSwim = false;
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float movementSpeed = 1f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool lockCursor = true;
    public bool isStartScreen = false;
    public bool isGameOverScreen = false;
    public GameObject MovementParticles;
	public bool playerHasSwam = false;
    
	private float timeLastPressed;
    private float timeDelayBetweenClicks = 1;
    private Quaternion cameraRot;
    private Quaternion mainRot;

    private bool cursorIsLocked = true;
    private bool swimButtonPressed = false;

    private float xRot = 0f;
    private AudioSource[] audioSources;
    private AudioSource movingSource;

    static private int movingSourceIndex = 0;

    private int positionInBackSequence = 0;

	public void resetSwimming() {
		swimButtonPressed = false;
		if (MovementParticles != null) {
			MovementParticles.SetActive (false);
		}
	}

    private void Start()
    {
        timeLastPressed = Time.time;

        mainRot = transform.localRotation;
        audioSources = transform.GetComponents<AudioSource>();
        if (audioSources.Length > 0)
        {
            movingSource = audioSources[movingSourceIndex];
            movingSource.Pause();
        }
    }

    private void Update()
    {
        Rigidbody rigidBody = this.transform.GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        cameraRot = Camera.main.transform.localRotation;
        xRot += CrossPlatformInputManager.GetAxis("Mouse Y") * XSensitivity;
        float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * YSensitivity;

        xRot = Mathf.Clamp(xRot, -89, 89);

        cameraRot.eulerAngles = new Vector3(-xRot, 0f, 0f);
        mainRot *= Quaternion.Euler(0f, yRot, 0f);


        transform.localRotation = mainRot;
        Camera.main.transform.localRotation = cameraRot;


        UpdateCursorLock();

        if (OVRInput.Get(OVRInput.Button.One))
            swimButtonPressed = true;
        else
            swimButtonPressed = false;

        OVRInput.Update();
    }

    private bool PositioninSequenceCheck(int position, bool swimPressed, bool sonarPressed)
    {
        switch (position)
        {
            case 0:
                if (swimPressed && sonarPressed)
                {
                    return true;
                }
                break;
            case 1:
                if (!swimPressed && sonarPressed)
                {
                    return true;
                }
                break;
            case 2:
                if (swimPressed && sonarPressed)
                {
                    return true;
                }
                break;
            default:
                return true;
                break;


        }
        return false;
    }

    private void FixedUpdate()
    {
        bool swimPressed = (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton("Swim"));
        bool sonarPressed = (Input.GetButton("SonarPing") || OVRInput.Get(OVRInput.Button.PrimaryTouchpad) || OVRInput.Get(OVRInput.Button.One));
        bool foundPositionInSequence = false;
        /*while (!foundPositionInSequence)
        {
            bool positionCheck = PositioninSequenceCheck(positionInBackSequence, swimPressed, sonarPressed);
            if (positionCheck)
                foundPositionInSequence = true;
            else

        }*/



//        if (!isStartScreen || !isGameOverScreen)
		if(playerCanSwim) {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton("Swim"))
            {
				playerHasSwam = true;
                transform.position += Camera.main.transform.forward * movementSpeed;
                if (MovementParticles != null)
                    MovementParticles.SetActive(true);

                if (movingSource != null)
                    if (!movingSource.isPlaying)
                        movingSource.UnPause();
            }
            else
            {
                if (MovementParticles != null)
                    MovementParticles.SetActive(false);
                if (movingSource != null)
                    movingSource.Pause();
            }
        }
        OVRInput.FixedUpdate();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorIsLocked = true;
        }

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}