using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour, Conversable {

    public string Name;
    public string greeting = "Hi.";

    private Transform heldObject = null;

    private TextMeshPro speechBubble;

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
        speechBubble = transform.Find("Speech Bubble").GetComponent<TextMeshPro>();
	}
	
	void Update () {
		
	}

    public bool IsCarryingSomething(){
        return heldObject != null;
    }

    public void ListenTo(string message, Conversable speaker, Conversation conversation)
    {
        // if we haven't said something yet in this conversation
        if(!conversation.HasSpokenIn(this)){
           speak(greeting, conversation); 
        } else {
            // otherwise we've said all we've come here to say and must therefore bid our adieus
            conversation.Leave(this);
        }
    }

    public string GetName() {
        return transform.name;
    }

    public void StartConversationWith(string text, Conversable otherPerson){
        Conversation conversation = new Conversation(this);
        conversation.Join(otherPerson);
        speak(greeting, conversation);
    }

    private void speak(string text, Conversation conversation){
        conversation.Speak(text, this);
        speechBubble.text = text;
        StartCoroutine(fadeSpeechBubble());
    }

    // clear the speech bubble text box
    // after an appropriate amount of time
    // TODO: consider an actual "fade" with alpha modification
    private IEnumerator fadeSpeechBubble() {
        float fadeTime = 2f;
        yield return new WaitForSeconds(fadeTime);
        speechBubble.text = "";
    }
}
