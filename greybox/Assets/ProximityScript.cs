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

		// look at each component that's been subscribed to
		foreach(string component in subscribers.Keys){
			if(other.GetComponent(component)){
				
				// keep a list of all the objects with the given component
				nearbyObjects[component].Add(other.gameObject);

				// update any listening scripts
				foreach(Enterable script in subscribers[component]){
					script.OnEntered(other.gameObject);
				}

			}
		}
	}

	// Record gameObjects leaving the zone
	void OnTriggerExit(Collider other) {

		// check each type of component we're tracking
		foreach(string component in subscribers.Keys) {
			if(other.GetComponent(component)) {
				// notify each interested script
				foreach(Enterable script in subscribers[component]) {
					script.OnExited(other.gameObject);
				}
			}
		}	
	}

	// Returns a list of all GameObjects that have entered the
	// proximity trigger area / zone
	public GameObject[] GetObjectsWithComponent(string component){
		if(nearbyObjects.ContainsKey(component)) {
			return nearbyObjects[component].ToArray();
		}
		return null;
	}

	// subscribe listener to messages when any object with the 
	// specified component enters or exits the zone
	public void Subscribe(string component, Enterable script){

		// creating a new list of subscribers if necessary
		if(!subscribers.ContainsKey(component)) {
			subscribers[component] = new List<Enterable>();
			nearbyObjects[component] = new List<GameObject>();
		}

		// add the subscriber
		subscribers[component].Add(script);
	}

	// subscribe listener to a list of components
	public void Subscribe(string[] components, Enterable script){
		foreach(string component in components) {
			Subscribe(component, script);
		}
	}

	// unsubscribe listener from following nearby objects
	// with the given component (i.e. c# script, interface, Transform)
	public void Unsubscribe(string component, Enterable script){
		if(subscribers.ContainsKey(component)){

			// remove subscriber from the list of subscribers
			subscribers[component].Remove(script);

			// remove dictionary entry if no other subscribers are interested in this component...
			if(subscribers[component].Count == 0) {
				subscribers.Remove(component);
			}
		}
	}

	// unsubscribe from multiple components
	public void Unsubscribe(string[] components, Enterable script){
		foreach(string component in components) {
			Unsubscribe(component, script);
		}
	}

	private Vector3 scale;
	private float scaleTime;

	public void ResetSphere(){
		// shrink scale to nothingness
		scale = transform.localScale;
		transform.localScale = Vector3.zero;
		scaleTime = Time.time;
	}

	void Start () {
		ResetSphere();
	}

	void Update() {
		// gradually grow back to full size
		if(Time.time - scaleTime <= 1) {
			transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.time - scaleTime);
		}
	}
}
