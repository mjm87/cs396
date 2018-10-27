using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDisplayScipt : MonoBehaviour {

    public Transform cam;

	// Update is called once per frame
	void Update () {

        transform.LookAt(cam);

        transform.Rotate(0, 180, 0);

	}
}
