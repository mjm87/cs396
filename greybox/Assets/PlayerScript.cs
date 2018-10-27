using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 7f;
    public float rotationSpeed = 50f;

    private float rotationAngle = 0;

	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Item") {
            GetComponent<CharacterScript>().pickUp(collider.transform);

        }
    }


	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward*Time.deltaTime*Input.GetAxis("Vertical")*speed);
        // transform.Rotate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")*speed);

        if (Input.GetKeyUp(KeyCode.Space)) {

            GetComponent<CharacterScript>().drop();
        }

        rotationAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(0, rotationAngle, 0); 
     
    }
}
