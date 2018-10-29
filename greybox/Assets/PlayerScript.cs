using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, Enterable {

    public float speed = 7f;
    public float rotationSpeed = 50f;

    private float rotationAngle = 0;

    void Start () {
        GetComponentInChildren<ProximityScript>().Subscribe("Item", this);
    }

    void OnDestroy () {
        GetComponentInChildren<ProximityScript>().Unsubscribe("Item", this);
    }

	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward*Time.deltaTime*Input.GetAxis("Vertical")*speed);
        // transform.Rotate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")*speed);


        rotationAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(0, rotationAngle, 0); 
     
        // Interact
        if(Input.GetKeyUp(KeyCode.E)) {
            GameObject item = getNearestItem();
            if(item != null) {
                interactWith(item);
            }
        }

        // Drop / Pick-up
        if(Input.GetKeyUp(KeyCode.Space)){
            if(GetComponent<CharacterScript>().IsCarryingSomething()){
                // drop carried item
                GetComponent<CharacterScript>().drop();
            } else {
                // pickup nearest item
                GameObject item = getNearestItem();
                if(item != null && isWithinPickupRange(item)){
                    pickup(item);
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

    private void interactWith(GameObject item){
        item.GetComponent<Interactable>().Interact(GetComponent<CharacterScript>());
    }

    private void pickup(GameObject item){
        GetComponent<CharacterScript>().pickUp(item.transform);
    }

    private float distanceTo(GameObject item){
        return (item.transform.position - transform.position).magnitude;
    }

    private bool isWithinPickupRange(GameObject item){
        float minimumDistanceRange = 2f;
        return distanceTo(item) <= minimumDistanceRange;
    }

    public void OnEntered(GameObject other)
    {
        // do nothing for now
    }

    public void OnExited(GameObject other)
    {
        // do nothing for now
    }
}