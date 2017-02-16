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
            wire.Add(this.gameObject);
            GetComponent<HingeJoint>().connectedBody = HeadAnchor;            
        }

        radius = GetComponent<SphereCollider>().radius;
        Fill();
        
	}

    private void Update()
    {
        if (IsMasterHead && Input.GetKeyDown(KeyCode.Space))
            Extend();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.Equals(TailAnchor.gameObject))
            return;

        if (TailAnchor.GetComponent<HingeJoint>() == null)
            TailAnchor.gameObject.AddComponent<HingeJoint>();

        GameObject lastTail = wire[wire.Count - 1].GetComponentInChildren<CapsuleCollider>().gameObject;
        wire[wire.Count - 1].transform.Translate(TailAnchor.transform.position);
        TailAnchor.GetComponent<HingeJoint>().connectedBody = lastTail.GetComponent<Rigidbody>();
        TailAnchor.GetComponent<HingeJoint>().connectedAnchor = lastTail.GetComponent<CapsuleCollider>().center;

    }


    /// <summary>
    /// Create a Wire between the Head and the Tail Anchor
    /// </summary>
    void Fill()
    {
        if(HeadAnchor.Equals(null) || TailAnchor.Equals(null))
            return;

        Vector3 spaceToFill = HeadAnchor.transform.position - TailAnchor.transform.position;
        spaceToFill /= gameObject.transform.localScale.magnitude;
        float piecesNeeded = spaceToFill.magnitude / radius;

        for (int i = 0; i < piecesNeeded; i++)
        {
           Extend();
        }

        Vector3 tailPosition = wire[wire.Count - 1].transform.position;
        tailPosition = Vector3.MoveTowards(tailPosition, TailAnchor.transform.position, Mathf.Infinity);
    }


    /// <summary>
    /// Create a new element of Wire and attaches it on the Head of the last one
    /// </summary>
    void Extend()
    {
        GameObject lastTail = wire[wire.Count - 1].GetComponentInChildren<CapsuleCollider>().gameObject;
        GameObject newPiece = Instantiate<GameObject>(this.gameObject, lastTail.transform.position, lastTail.transform.rotation);

        newPiece.GetComponent<WireManager>().IsMasterHead = false;
        wire.Add(newPiece);
        newPiece.GetComponent<HingeJoint>().connectedBody = lastTail.GetComponent<Rigidbody>();
        newPiece.GetComponent<HingeJoint>().connectedAnchor = lastTail.GetComponent<CapsuleCollider>().center;
    }
}
