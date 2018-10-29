using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDisplayScipt : MonoBehaviour {

    public Transform cam;

	void Update () {

        // Force label to "always" face the camera
        transform.LookAt(cam);

        // Flip the label since it was backwards
        // the "forward" direction for 3D TextMeshPro must be in the wrong direction???
        transform.Rotate(0, 180, 0);
	}
}
