using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour {

    public string Name;

    private Transform heldObject = null;

    public void pickUp(Transform myThing) {

        if (heldObject == null) {
            heldObject = myThing;

            myThing.position = transform.position + new Vector3(0, 1, 0);
            myThing.parent = transform;
        }
    }

    public void drop() {
        if (IsCarryingSomething()) {
            heldObject.position = transform.Find("DropSpot").position;
            heldObject.parent = null;
            heldObject = null;
        }
    }

    public void use() {

        if (heldObject != null) {
            heldObject.Rotate(Vector3.up, 5);
        }
        if(heldObject.GetComponent<ItemScript>().itemType == "Book") {
            heldObject.Rotate(Vector3.forward, 5);
        }
    }

	void Start () {
        gameObject.GetComponentInChildren<TextMeshPro>().text = Name;
	}
	
	void Update () {
		
	}

    public bool IsCarryingSomething(){
        return heldObject != null;
    }
}
