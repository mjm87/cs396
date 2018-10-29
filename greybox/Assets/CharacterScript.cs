using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour {

    public string name;

    //added null
    private Transform heldObject = null;


    public void pickUp(Transform myThing) {

        //added if statement
        if (heldObject == null) {
            heldObject = myThing;

            myThing.position = transform.position + new Vector3(0, 1, 0);
            myThing.parent = transform;
        }
    }

    public void drop() {
        if (heldObject != null)
        {
            heldObject.position = transform.Find("DropSpot").position;
            heldObject.parent = null;
            heldObject = null;
        }
    }

    //added use method
    public void use() {

        if (heldObject != null) {

            heldObject.Rotate(Vector3.up, 5);
        }
        if(heldObject.GetComponent<ItemScript>().itemType == "Book") {
            heldObject.Rotate(Vector3.forward, 5);
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
