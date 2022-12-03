using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickUpRange=5;
    public float moveForce = 250;
    public Transform holdParent;

    private GameObject heldObj;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("check1");
            // if the object has not been picked up yet: 
            if (heldObj == null) {
                Debug.Log("check2");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange)) {
                    Debug.Log("check3", hit.transform.gameObject);
                    PickupObject(hit.transform.gameObject);
                }

            } else {
                DropObject();

            }
        }

        if (heldObj != null) {
            MoveObject();
        }
    }

    void MoveObject () {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f) {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    // when picking up the object
    void PickupObject(GameObject pickObj) {
        Debug.Log("check4");
        if(pickObj.GetComponent<Rigidbody>()) {
            Debug.Log("check5");
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }

    void DropObject() {
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;

        heldObj.transform.parent = null;
        heldObj = null;

    }
}
