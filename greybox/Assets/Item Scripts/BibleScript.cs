using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BibleScript : MonoBehaviour, Interactable {

	public Color color;
	public string label = "Bible";

    // Use this for initialization
    void Start () {
		GetComponent<MeshRenderer>().material.color = color;
		transform.Find("Label").GetComponent<TextMeshPro>().text = label;
		//TODO: figure out what's wrong with the label-text in-game
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Interact(CharacterScript character)
    {
		Debug.Log("Read page from the bible");
		// TODO: Add some randomization (Read Psalms 8, Ecclesiastes 4)
		// Increase character "Bible" skills
		// Consume some time
    }
}
