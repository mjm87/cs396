using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform focus;

	void Update () {

        transform.LookAt(focus);
	}
}
