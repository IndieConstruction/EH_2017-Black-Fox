using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
[RequireComponent (typeof(HingeJoint))]
/// <summary>
/// Used to manage the rope between the component and the Target position
/// </summary>
public class RopeManager : MonoBehaviour {

    //Transform of the point to connect to this GameObject
    public Transform Target;

    public float FiniteElementDensity = 1f;                      //Density of Joints per unity (of Unity)
    public float RopeDrag = .1f;                                    //Each Joint Drag
    public float RopeMass = .1f;                                    //Each Joint Mass
    public float RopeColliderRadius = 1f;                           //Radius of the SphereCollider in Joints

    private List<GameObject> joints = new List<GameObject>();       //Collection of Joints that describe the rope
    private LineRenderer lineRend;                                  //Reference to the LineRenderer
    private int totalJoints = 0;                                    //Total amount of Joints
    private bool rope = false;

    public Vector3 SwingAxis = Vector3.one;                         //Sets which axis the Joint will swing on
    public float LowTwistLimt = -100f;                              //The lower limit of the Joint axes
    public float HighTwistLimit = 100f;                             //The upper limit of the joint axes
    public float Swing1Limit = 20f;                                 //The initial position of the joint axes;


    private void Awake()
    {
        ExtendRope();
    }

    private void Update()
    {

    }

    /// <summary>
    /// Update the Joints number and position to fit the required FiniteElementDensity
    /// This method can only increase the number of Joints, extending the Rope
    /// </summary>
    void UpdateJoints()
    {
        //Check if it's really neede the Update of Joints
        int totalJointsNeeded = (int)(Vector3.Distance(transform.position, Target.position) * FiniteElementDensity);
        if (totalJointsNeeded < totalJoints)
            return;

        //Update of the Joints to fit the needs
        totalJoints = totalJointsNeeded;
        //Update the position of the points for the Line Renderer
        lineRend.numPositions = totalJoints;
        //Update of the list Object and Position of the Joints
        while (joints.Count < totalJoints)
        {
            joints.Add(new GameObject());
        }
        //Set the first and last of the list as this and the Target
        joints[0] = gameObject;
        joints[joints.Count - 1] = Target.gameObject;
    }

    /// <summary>
    /// Extend the Rope toward the Target position
    /// </summary>
    void ExtendRope()
    {
        UpdateJoints();

        var separation = ((Target.position)-transform.position)/(totalJoints-1);

        for (int i = 0; i < totalJoints-1; i++)
        {
            if (joints[i].GetComponent<Transform>() == null)
            {
                joints[i].AddComponent<Transform>();
                Vector3 pos = (separation * i) + transform.position;
                joints[i].transform.position = pos;
            }

            if (joints[i].GetComponent<SphereCollider>() == null)
                joints[i].AddComponent<SphereCollider>();

            if (joints[i].GetComponent<HingeJoint>() == null)
                joints[i].AddComponent<HingeJoint>();

            if (joints[i].GetComponent<Rigidbody>() == null)
                joints[i].AddComponent<Rigidbody>();

            AdjustJoint(i);
            AdjustJointPhysics(i);            
        }

        //Setup of the Target's HingeJoint
        HingeJoint lastHJ;
        if (Target.gameObject.GetComponent<HingeJoint>() == null)
            Target.gameObject.AddComponent<HingeJoint>();
        lastHJ = Target.gameObject.GetComponent<HingeJoint>();
        lastHJ.connectedBody = joints[totalJoints - 1].GetComponent<Rigidbody>();
        lastHJ.axis = SwingAxis;
        JointLimits lastHJlimits = lastHJ.limits;
        lastHJlimits.min = LowTwistLimt;
        lastHJlimits.max = HighTwistLimit;

        Target.parent = transform;

        //set the Rope as existing
        rope = true;
    }

    /// <summary>
    /// Set generic parameters of the joints
    /// </summary>
    /// <param name="n">The index of the list of GameObjects</param>
    void AdjustJoint(int n)
    {
        //Set the name of the joint
        if(n != 0 && n!= totalJoints-1)
            joints[n].name = "Joint " + n;

        //Set the parent of the joint
        joints[n].transform.parent = transform;

    }

    /// <summary>
    /// Set the parameter of SphereCollider, Rigidbody and HingeJoint
    /// </summary>
    /// <param name="n">The index of the list of GameObjects</param>
    void AdjustJointPhysics(int n)
    {
        SphereCollider coll = joints[n].GetComponent<SphereCollider>();
        Rigidbody rigid = joints[n].GetComponent<Rigidbody>();
        HingeJoint hj = joints[n].GetComponent<HingeJoint>();
        JointLimits hjLimits = hj.limits;

        hj.axis = SwingAxis;
        hj.connectedBody = (n == 1) ? GetComponent<Rigidbody>() : joints[n - 1].GetComponent<Rigidbody>();
        hjLimits.min = LowTwistLimt;
        hjLimits.max = HighTwistLimit;

        rigid.drag = RopeDrag;
        rigid.mass = RopeMass;

        coll.radius = RopeColliderRadius;
    }
}
