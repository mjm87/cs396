using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour {

    public List<Transform> playables;
    private Dictionary<string, Transform> playableDict;
    private Dropdown dropdown;
    private Transform previous;

	void Start () {

        // Clearing the PlayerSelector dropdown
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();

        // Linking dropdown options to the selected playable characters
        playableDict = new Dictionary<string, Transform>();
        foreach(Transform playable in playables)
        {
            CharacterScript character = playable.GetComponent<CharacterScript>();
            playableDict.Add(character.name, character.transform);
            dropdown.options.Add(new Dropdown.OptionData(character.name, null));
        }

        // defaulting the dropdown to the first playable character in the list
        dropdown.value = 0;
        dropdown.RefreshShownValue();

        // defaulting the player to the first playable character in the list
        previous = playables[0];
        previous.gameObject.AddComponent<PlayerScript>();
	}
	
    // Essentially, we're handing off the PlayerScript component
    // like the Conch shell from Lord of Flies to indicate which
    // playable character is being controlled by the player.
    public void Select() {

        // Remove the PlayerScript component from the previous playable character 
        Destroy(previous.gameObject.GetComponent<PlayerScript>());

        // Adding the PlayerScript component to the newly selected playable character
        string selected = dropdown.options[dropdown.value].text;
        playableDict[selected].gameObject.AddComponent<PlayerScript>();

        // Keeping track of which object now has the PlayerScript
        previous = playableDict[selected];
    }
}
