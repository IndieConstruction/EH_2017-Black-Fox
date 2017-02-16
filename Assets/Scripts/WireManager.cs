using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (HingeJoint))]
public class WireManager : MonoBehaviour {


    public bool IsMasterHead = false;
    public Rigidbody HeadAnchor;
    public Rigidbody TailAnchor;
    

    List<GameObject> wire = new List<GameObject>();
    float radius;
	
	void Start () {
        //Setup for the MasterHead of the Wire
        if (IsMasterHead)
        {
            wire.Add(gameObject);           
        }

        radius = GetComponent<CapsuleCollider>().radius;
        
	}

    private void Update()
    {
        if (IsMasterHead && Input.GetKeyDown(KeyCode.Space))
            Extend();
    }


    /// <summary>
    /// Create a new element of Wire and attaches it on the Head of the last one
    /// </summary>
    void Extend()
    {
        GameObject lastTail = wire[wire.Count - 1];
        GameObject newPiece = Instantiate(gameObject, lastTail.transform.position - new Vector3(0,radius*2,0)*2, lastTail.transform.rotation);

        newPiece.GetComponent<WireManager>().IsMasterHead = false;
        wire.Add(newPiece);
        newPiece.GetComponent<HingeJoint>().connectedBody = lastTail.GetComponent<Rigidbody>();
        newPiece.GetComponent<HingeJoint>().autoConfigureConnectedAnchor = false;

        foreach (Transform sons in lastTail.GetComponentsInChildren<Transform>())
        {
            if (sons.name == "ElementBottom")
            {
                newPiece.GetComponent<HingeJoint>().connectedAnchor = sons.localPosition;
                newPiece.GetComponent<HingeJoint>().anchor = sons.localPosition;

            }
        }
            
        
    }
}
