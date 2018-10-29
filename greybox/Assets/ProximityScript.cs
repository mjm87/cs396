using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOTE: a good majority of this script is currently not being used.
 * The goal of the script was to create a collidable trigger zone
 * that allows a character to know what objects are nearby them.
 *
 * Initially, I was approaching the problem with a generic implementation
 * of the Observer pattern. While this would work, I realized that at 
 * present I have no real interest in the actually entrance/exit of 
 * any given item: but rather am only interested in the aggregate.
 *
 * That being said, I can foresee it becoming useful in the future
 * and have thus left it in for now. We can review that decision later.
 */

public class ProximityScript : MonoBehaviour {

	private Dictionary<string, List<Enterable>> subscribers = new Dictionary<string, List<Enterable>>();
	private Dictionary<string, List<GameObject>> nearbyObjects = new Dictionary<string, List<GameObject>>();

	// Record gameObjects entering the proximity zone
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

	// Record gameObjects leaving the zone
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
