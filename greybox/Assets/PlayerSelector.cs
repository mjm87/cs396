using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour {

    public List<Transform> playables;
    private Dictionary<string, Transform> playableDict;
    private Dropdown dropdown;
    private Transform previous;

	// Use this for initialization
	void Start () {
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();

        playableDict = new Dictionary<string, Transform>();
        foreach(Transform playable in playables)
        {
            CharacterScript character = playable.GetComponent<CharacterScript>();
            playableDict.Add(character.name, character.transform);
            dropdown.options.Add(new Dropdown.OptionData(character.name, null));
        }

        previous = playables[0];
        previous.gameObject.AddComponent<PlayerScript>();
        
	}
	
    public void Select() {

        Destroy(previous.gameObject.GetComponent<PlayerScript>());

        //Debug.Log(dropdown.options[dropdown.value].text);

        string selected = dropdown.options[dropdown.value].text;

        playableDict[selected].gameObject.AddComponent<PlayerScript>();

        previous = playableDict[selected];
    }

	// Update is called once per frame
	void Update () {

	}
}
