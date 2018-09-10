using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAdminControls : MonoBehaviour {


    public GameObject adminPanel;
    public Text adminTextAlert;
    private int adminButtonClicks = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        adminTextAlert.text = "";
        for (int i = 0; i < adminButtonClicks; i++)
        {
            adminTextAlert.text += "+";
        }
        if(adminButtonClicks == 10)
        {
            adminPanel.SetActive(!adminPanel.activeSelf);
            adminButtonClicks = 0;
        }

        if(Input.GetButton("AdminButton") && (adminButtonClicks % 2 == 0))
        {
            if (adminButtonClicks != 6)
            {
                adminButtonClicks++;
            }
            else
            {
                adminButtonClicks = 0;
            }
        }
        if (!Input.GetButton("AdminButton") && (adminButtonClicks % 2 == 1))
        {
            adminButtonClicks++;
        }
        if (Input.GetButton("Swim"))
        {
            if (adminButtonClicks == 6)
            {
                adminButtonClicks++;
            }
            if (adminButtonClicks != 7 && adminButtonClicks != 8)
            {
                adminButtonClicks = 0;
            }
        }
    }
}
