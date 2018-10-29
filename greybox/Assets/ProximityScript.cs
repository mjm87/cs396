using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityScript : MonoBehaviour {

	private Dictionary<string, List<Enterable>> subscribers = new Dictionary<string, List<Enterable>>();
	private Dictionary<string, List<GameObject>> nearbyObjects = new Dictionary<string, List<GameObject>>();

	// Record gameObjects entereing the proximity zone
	void OnTriggerEnter(Collider other) {
		if(subscribers.ContainsKey(other.tag)){

			// update list of nearby objects with that tag
			nearbyObjects[other.tag].Add(other.gameObject);

			// update any listening scripts
			foreach(Enterable script in subscribers[other.tag]){
				script.OnEntered(other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(subscribers.ContainsKey(other.tag)){
			foreach(Enterable script in subscribers[other.tag]){
				script.OnExited(other.gameObject);
			}
		}
	}

	// Returns a list of all GameObjects that have entered the
	// proximity trigger area / zone
	public GameObject[] GetObjectsWithTag(string tag){
		if(nearbyObjects.ContainsKey(tag)) {
			return nearbyObjects[tag].ToArray();
		}
		return null;
	}

	// subscribe the listener to a given tag
	public void Subscribe(string tag, Enterable script){

		// creating a new list of subscribers if necessary
		if(!subscribers.ContainsKey(tag)) {
			subscribers[tag] = new List<Enterable>();
			nearbyObjects[tag] = new List<GameObject>();
		}

		// add the subscriber
		subscribers[tag].Add(script);
	}

	// subscribe listener to a list of tags
	public void Subscribe(string[] tags, Enterable script){
		foreach(string tag in tags) {
			Subscribe(tag, script);
		}
	}

	// unsubscribe listener from the tag
	public void Unsubscribe(string tag, Enterable script){
		if(subscribers.ContainsKey(tag)){

			// remove subscriber from the list of subscribers
			subscribers[tag].Remove(script);

			// remove dictionary entry if no other subscribers are interested in this tag...
			if(subscribers[tag].Count == 0) {
				subscribers.Remove(tag);
			}
		}
	}

	// unsubscribe from multiple tags
	public void Unsubscribe(string[] tags, Enterable script){
		foreach(string tag in tags) {
			Unsubscribe(tag, script);
		}
	}
}
