using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour {

    public string name;

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
