using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool isFacingRight;
    private Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Horizontal") > 0)
        {

        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {

        }
    }
}
