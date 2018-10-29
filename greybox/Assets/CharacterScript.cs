using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour {

    public string name;

    private Transform heldObject;


    public void pickUp(Transform myThing) {

        if(!IsCarryingSomething()) {

            // place object over head
            myThing.position = transform.position + new Vector3(0, 1, 0);
            myThing.parent = transform;

            // keep track of what we are holding
            heldObject = myThing;
        }
    }

    public void drop() {
        if (IsCarryingSomething()) {
            heldObject.position = transform.Find("DropSpot").position;
            heldObject.parent = null;
            heldObject = null;
        }
    }

	// Use this for initialization
	void Start () {
        gameObject.GetComponentInChildren<TextMeshPro>().text = name;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsCarryingSomething(){
        return heldObject != null;
    }
}
