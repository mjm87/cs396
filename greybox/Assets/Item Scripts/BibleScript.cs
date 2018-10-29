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
		readRandomVerseFromTheBible();
		// Increase character "Bible" skills
		// Consume some time
    }


	private void readRandomVerseFromTheBible(){
		string[] verses = new string[]{
			"Psalms 8",
			"Genesis 1",
			"John 3:16",
			"Revelations 14",
			"Philippians 2",
			"Leviticus 9"
		};

		string randomVerse = verses[Random.Range(0,verses.Length-1)];
		Debug.Log("Read " + randomVerse);
	}
}
