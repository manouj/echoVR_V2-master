using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimateStart : MonoBehaviour {

    private Animator animator;
    private bool randomizeStart = true;
    private float randomStart;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        randomStart = Random.Range(0, animator.GetCurrentAnimatorStateInfo(0).length);
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, randomStart);
	}
	
	// Update is called once per frame
	void Update () {
    }
}
