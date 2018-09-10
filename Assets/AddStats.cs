using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddStats : MonoBehaviour {

    public GameObject FishNumTextObj;
    public GameObject OctoNumTextObj;
    public GameObject StarOne;
    public GameObject StarTwo;
    public GameObject StarThree;

	// Use this for initialization
	void Start () {
        Text FishNumText = FishNumTextObj.GetComponent<Text>();
        FishNumText.text = GlobalVariables.numFishCollected.ToString("D2");

        Text OctoNumText = OctoNumTextObj.GetComponent<Text>();
        OctoNumText.text = GlobalVariables.numOctoCollected.ToString("D2");

        int totalFish = GlobalVariables.numFishCollected + GlobalVariables.numOctoCollected;

        if (totalFish >= 1)
            StarOne.SetActive(true);
        if (totalFish >= 3)
            StarTwo.SetActive(true);
        if (totalFish >= 5)
            StarThree.SetActive(true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
