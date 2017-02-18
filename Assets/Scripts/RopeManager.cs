﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
[RequireComponent (typeof(HingeJoint))]
/// <summary>
/// Used to manage the rope between the component and the Target position
/// </summary>
public class RopeManager : MonoBehaviour {
        
    public Transform Target;                                        //Transform of the point to connect to this GameObject
    private Transform origin;                                       //Transform of the point of origin of the Rope (also set as last element of the Rope)

    public float RopeMaxLength = 1f;                                //Max length of the rope (in Unity's unity)
    public float FiniteElementDensity = 1f;                         //Density of Joints per unity (of Unity)
    public float RopeDrag = .1f;                                    //Each Joint Drag
    public float RopeMass = .1f;                                    //Each Joint Mass
    public float RopeColliderRadius = 1f;                           //Radius of the SphereCollider in Joints

    private List<GameObject> joints = new List<GameObject>();       //Collection of Joints that describe the rope
    private LineRenderer lineRend;                                  //Reference to the LineRenderer
    private int totalJoints = 0;                                    //Total amount of Joints
    private float ropeCurrentLength;                                //Current length of the rope (in Unity's unity)
    private bool rope = false;

    public Vector3 SwingAxis = new Vector3(1,0,1);                  //Sets which axis the Joint will swing on

    private void Awake()
    {
        //Get reference to LineRederer
        lineRend = GetComponent<LineRenderer>();

        //Set this gameObject as origin of the rope
        origin = transform;
        joints.Add(gameObject);
        totalJoints++;

        //Build the Rope
        UpdateJoints();
        ExtendRope();
    }

    void LateUpdate()
    {
        // Does rope exist? If so, update its position
        if (rope)
        {
            for (int i = 0; i < totalJoints; i++)
            {
                if (i == 0) 
                    lineRend.SetPosition(i, transform.position);

                else if (i == totalJoints - 1)
                    lineRend.SetPosition(i, Target.position);

                else
                    lineRend.SetPosition(i, joints[i].transform.position);
            }
            lineRend.enabled = true;
        }
        else
            lineRend.enabled = false;
    }

    /// <summary>
    /// Update the Joints number to fit the required FiniteElementDensity
    /// This method can only increase the number of Joints
    /// </summary>
    void UpdateJoints()
    {
        int oldJointsAmount = totalJoints;
        //Update of the Joints to fit the needs
        ropeCurrentLength = Vector3.Distance(origin.position, Target.position);
        totalJoints = (int)(ropeCurrentLength * FiniteElementDensity);
        
        //Update the position of the points for the Line Renderer
        lineRend.numPositions = totalJoints;
        //Increase the length of the rope
        for (int i = oldJointsAmount; i < totalJoints-2; i++)
            joints.Add(new GameObject());

        //Set the last of the list as the Target        
        joints.Add(Target.gameObject);//ELEMENTO NON ISTANZIATO! Correggi
    }

    /// <summary>
    /// Extend the Rope from the last Joint toward the Target position
    /// </summary>
    void ExtendRope()
    {
        Vector3 pos;

        //Measure the reqired offset between the joints
        var separation = ((Target.position)-origin.position)/(totalJoints-1);

        for (int i = joints.LastIndexOf(origin.gameObject); i < totalJoints-1; i++)
        {
            //Create a joint
            joints[i].name = ("Joint " + i);

            pos = (separation * i) + origin.position;
            joints[i].transform.position = pos;

            //Set the parent of the joint
            joints[i].transform.parent = transform;

            //Set SphereColliders, Rigidbodies and HingeJoints
            if (i == 0)
                AdjustJointPhysics(joints[0]);
            else
                AdjustJointPhysics(joints[i],joints[i-1].GetComponent<Rigidbody>());            
        }

        //Setup of the Target's HingeJoint
        AdjustJointPhysics(Target.gameObject);

        //set the Rope as existing
        rope = true;
    }

    /// <summary>
    /// Set the parameter of SphereCollider, Rigidbody and HingeJoint
    /// </summary>
    /// <param name="_jointToSetup">The Joint to setup</param>
    void AdjustJointPhysics(GameObject _jointToSetup)
    {
        SphereCollider coll = null;
        Rigidbody rigid;
        HingeJoint hj;

        if(_jointToSetup != Target.gameObject)
        {
            //Reference to SphereCollider
            if (_jointToSetup.GetComponent<SphereCollider>() == null)
                coll = _jointToSetup.AddComponent<SphereCollider>();
            else
                coll = _jointToSetup.GetComponent<SphereCollider>();
        }        

        //Reference to HingeJoint
        if (_jointToSetup.GetComponent<HingeJoint>() == null)
            hj = _jointToSetup.AddComponent<HingeJoint>();
        else
            hj = _jointToSetup.GetComponent<HingeJoint>();

        //Reference to Rigidbody
        rigid = _jointToSetup.GetComponent<Rigidbody>();

        //Setup of the HingeJoint
        hj.axis = SwingAxis;
        //Setup of the Rigidbody
        rigid.drag = RopeDrag;
        rigid.mass = RopeMass;
        if (_jointToSetup != Target.gameObject)
            //Setup of the SphereCollider
            coll.radius = RopeColliderRadius;
    }

    /// <summary>
    /// Set the parameter of SphereCollider, Rigidbody and HingeJoint
    /// </summary>
    /// <param name="_jointToSetup">The Joint to setup</param>
    /// <param name="_rbToConnect">The Rigidbody to which connet the Joint</param>
    void AdjustJointPhysics(GameObject _jointToSetup, Rigidbody _rbToConnect)
    {
        SphereCollider coll;
        Rigidbody rigid;
        HingeJoint hj;

        //Reference to SphereCollider
        if (_jointToSetup.GetComponent<SphereCollider>() == null)
            coll = _jointToSetup.AddComponent<SphereCollider>();
        else
            coll = _jointToSetup.GetComponent<SphereCollider>();

        //Reference to Rigidbody
        if (_jointToSetup.GetComponent<Rigidbody>() == null)
            rigid = _jointToSetup.AddComponent<Rigidbody>();
        else
            rigid = _jointToSetup.GetComponent<Rigidbody>();

        //Reference to HingeJoint
        if (_jointToSetup.GetComponent<HingeJoint>() == null)
            hj = _jointToSetup.AddComponent<HingeJoint>();
        else
            hj = _jointToSetup.GetComponent<HingeJoint>();

        //Setup of the HingeJoint
        hj.axis = SwingAxis;
        hj.connectedBody = _rbToConnect;
        //Setup of the Rigidbody
        rigid.drag = RopeDrag;
        rigid.mass = RopeMass;
        //Setup of the SphereCollider
        coll.radius = RopeColliderRadius;
    }
}
