﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward*Time.deltaTime*Input.GetAxis("Vertical")*speed);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")*speed);

     
    }
}
