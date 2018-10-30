using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, Enterable {

    public float speed = 7f;
    public float rotationSpeed = 50f;
    private float rotationAngle = 0;

    private CharacterScript character;

    void Start () {
        GetComponentInChildren<ProximityScript>().Subscribe("Item", this);
        character = GetComponent<CharacterScript>();
    }

    void OnDestroy () {
        GetComponentInChildren<ProximityScript>().Unsubscribe("Item", this);
    }

	void Update () {

        transform.Translate(Vector3.forward*Time.deltaTime*Input.GetAxis("Vertical")*speed);

        rotationAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(0, rotationAngle, 0); 

        // Use item
        if (Input.GetKey(KeyCode.M)){
            character.use();
        }
     
        // Interact with item
        if(Input.GetKeyUp(KeyCode.E)) {
            GameObject item = getNearestItem();
            if(item != null) {
                interactWith(item);
            }
        }

        // Drop / Pick-up
        if(Input.GetKeyUp(KeyCode.Space)){
            // if we're carrying something, drop it
            if(character.IsCarryingSomething()){
                character.drop();
            } else {
                // otherwise pickup nearest available item
                GameObject item = getNearestItem();
                if(item != null && isWithinPickupRange(item)){
                    character.pickUp(item.transform);
                }
            }
        }
    }

    private GameObject getNearestItem(){

        // Find all "item" tagged objects within proximity of the object
        GameObject[] nearbyItems = GetComponentInChildren<ProximityScript>().GetObjectsWithTag("Item");
        GameObject nearestItem = null; 

        if(nearbyItems.Length > 1) {

            // Find the closest "item"
            nearestItem = nearbyItems[0];
            foreach(GameObject item in nearbyItems){
                if(distanceTo(nearestItem) > distanceTo(item)){
                    nearestItem = item;
                }
            }
        }

        return nearestItem;
    }

    // Call the Interact method on the item
    private void interactWith(GameObject item){
        item.GetComponent<Interactable>().Interact(character);
    }

    // Compute distance from the given item
    // to the this gameObject's position
    private float distanceTo(GameObject item){
        return (item.transform.position - transform.position).magnitude;
    }

    // checks if the item is within 2 Unity meters
    private bool isWithinPickupRange(GameObject item){
        float minimumDistanceRange = 2f;    // parameterize?
        return distanceTo(item) <= minimumDistanceRange;
    }

    // define these to satisfy the observer pattern
    // TODO: could be completely removed, but might end up being useful.
    // Check with Michel before deleting
    public void OnEntered(GameObject other) { /* noop */ }
    public void OnExited(GameObject other) { /* noop */ }
}