using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform focus;

	void Update () {

        transform.LookAt(focus);

        Vector3 scrolling = Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 10f;
        transform.Translate(scrolling);
	}
}
