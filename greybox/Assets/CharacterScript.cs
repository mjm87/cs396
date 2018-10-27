using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour {

    public string name;

    private Transform heldObject;


    public void pickUp(Transform myThing) {

        heldObject = myThing;

        myThing.position = transform.position + new Vector3(0, 1, 0);
        myThing.parent = transform;

    }

    public void drop() {
        if (heldObject != null)
        {
            heldObject.position = transform.Find("DropSpot").position;
            heldObject.parent = null;
            heldObject = null;
        }
    }

	// Use this for initialization
	void Start () {
        //GameObject cubeLabel = GameObject.Find("CubeName");
        //cubeLabel.GetComponent<TextMeshProUGUI>().text = name;

        gameObject.GetComponentInChildren<TextMeshPro>().text = name;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
