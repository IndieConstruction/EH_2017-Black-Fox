using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(HingeJoint))]
/// <summary>
/// Used to manage the rope between the component and the Target position
/// </summary>
public class RopeManager : MonoBehaviour
{

    public Transform Target;                                        //Transform of the point to connect to this GameObject
    private Transform origin;                                       //Transform of the point of origin of the Rope (also set as last element of the Rope)

    public float RopeMaxLength = 100000f;                           //Max length of the rope (in Unity's unity)
    public float FiniteElementDensity = 0.01f;                      //Density of Joints per unity (of Unity)
    public float RopeWidth = 100f;                                  //LineRenderer Width
    public bool AutoSet = true;                                     //Setup for HingeJoints and Rigidbodies
    public Vector3 SwingAxis = new Vector3(1, 0, 1);                //Sets which axis the Joint will swing 
    public float RopeDrag = .1f;                                    //Each Joint Drag
    public float RopeMass = .1f;                                    //Each Joint Mass
    public float SwingMinAngle = -180f;                             //Minimum amount of swing
    public float SwingMaxAngle = 180f;                              //Maximum amount of swing
    public float BounceMinVelocity = 1;                             //

    private List<GameObject> joints = new List<GameObject>();       //Collection of Joints that describe the rope
    private LineRenderer lineRend;                                  //Reference to the LineRenderer
    private JointLimits hjLimit = new JointLimits();
    private int totalJoints = 0;                                    //Total amount of Joints
    private float ropeCurrentLength;                                //Current length of the rope (in Unity's unity)
    private float ropeStretching;                                   //Distance -----

    private float sphereColliderRadius
    {
        get
        {
            //SphereCollider's radius can't be larger than the rope
            var offSet = Vector3.Distance(transform.position, Target.position) / (totalJoints - 1);
            if (offSet > RopeWidth)
                return RopeWidth;
            else
                return offSet;
        }
    }                             //Radius of the SphereCollider in Joints

    private void Awake()
    {
        //Get reference to LineRederer
        lineRend = GetComponent<LineRenderer>();
        //Setup of the joints based onthe the current HingeJoint and Rigidbody
        if (AutoSet == true)
        {
            HingeJoint hj = GetComponent<HingeJoint>();
            Rigidbody rb = GetComponent<Rigidbody>();
            hjLimit = hj.limits;
            SwingAxis = hj.axis;
            RopeDrag = rb.drag;
            RopeMass = rb.mass;
            SwingMinAngle = hjLimit.min;
            SwingMaxAngle = hjLimit.max;
            BounceMinVelocity = hjLimit.bounceMinVelocity;
        }
        //RopeManager setup
        lineRend.widthMultiplier = RopeWidth;
        hjLimit.min = SwingMinAngle;
        hjLimit.max = SwingMaxAngle;

        //Set this gameObject as origin of the rope
        origin = transform;
        joints.Add(gameObject);
        totalJoints++;

        //Build the Rope
        ExtendRope();
    }

    private void Update()
    {

    }

    void LateUpdate()
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
        for (int i = oldJointsAmount; i < totalJoints - 1; i++)
            joints.Add(new GameObject());

        //Set the last of the list as the Target        
        joints.Add(Target.gameObject);
    }

    /// <summary>
    /// Extend the Rope from the last Joint toward the Target position
    /// </summary>
    void ExtendRope()
    {
        UpdateJoints();

        Vector3 pos;

        //Measure the reqired offset between the joints
        var separation = ((Target.position) - origin.position) / (totalJoints - 1);

        for (int i = joints.LastIndexOf(origin.gameObject) + 1; i < totalJoints - 1; i++)
        {
            //Create a new joint
            joints[i].name = ("Joint " + i);

            pos = (separation * i) + origin.position;
            joints[i].transform.position = pos;

            //Set the parent of the joint
            joints[i].transform.parent = transform;

            //Set SphereColliders, Rigidbodies and HingeJoints
            if (i == 0)
                AdjustJointPhysics(joints[0]);
            else
                AdjustJointPhysics(joints[i], joints[i - 1].GetComponent<Rigidbody>());
        }

        //Setup of the Target's HingeJoint
        AdjustJointPhysics(Target.gameObject);
    }

    /// <summary>
    /// Set the parameter of SphereCollider, Rigidbody and HingeJoint
    /// </summary>
    /// <param name="_jointToSetup">The Joint to setup</param>
    void AdjustJointPhysics(GameObject _jointToSetup)
    {
        HingeJoint hj;
        Rigidbody rigid;

        //Reference to HingeJoint
        if (_jointToSetup.GetComponent<HingeJoint>() == null)
            hj = _jointToSetup.AddComponent<HingeJoint>();
        else
            hj = _jointToSetup.GetComponent<HingeJoint>();

        //Reference to Rigidbody (existence assured by the presence of the HingeJoint)
        rigid = _jointToSetup.GetComponent<Rigidbody>();

        //Setup of the HingeJoint
        hj.axis = SwingAxis;
        hj.useLimits = true;
        if (AutoSet)
        {
            hj.limits = hjLimit;
        }
        else
        {
            hjLimit = hj.limits;
            SwingMinAngle = hjLimit.min;
            SwingMaxAngle = hjLimit.max;
            BounceMinVelocity = hjLimit.bounceMinVelocity;
            hj.limits = hjLimit;
        }
        hj.enablePreprocessing = true;
        if (_jointToSetup == Target.gameObject)
            hj.connectedBody = joints[joints.LastIndexOf(Target.gameObject) - 1].GetComponent<Rigidbody>();
        //Setup of the Rigidbody
        rigid.drag = RopeDrag;
        rigid.mass = RopeMass;
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

        //Reference to HingeJoint
        if (_jointToSetup.GetComponent<HingeJoint>() == null)
            hj = _jointToSetup.AddComponent<HingeJoint>();
        else
            hj = _jointToSetup.GetComponent<HingeJoint>();

        //Reference to Rigidbody
        rigid = _jointToSetup.GetComponent<Rigidbody>();

        //Set the proper layer ("Rope")
        _jointToSetup.layer = 9;

        //Setup of the HingeJoint
        hj.axis = SwingAxis;
        hj.useLimits = true;
        if (AutoSet)
        {
            hj.limits = hjLimit;
        }
        else
        {
            hjLimit = hj.limits;
            SwingMinAngle = hjLimit.min;
            SwingMaxAngle = hjLimit.max;
            BounceMinVelocity = hjLimit.bounceMinVelocity;
            hj.limits = hjLimit;
        }
        hj.enablePreprocessing = true;
        hj.connectedBody = _rbToConnect;
        //Setup of the Rigidbody
        rigid.drag = RopeDrag;
        rigid.mass = RopeMass;
        //Setup of the SphereCollider
        coll.radius = sphereColliderRadius;
    }
}

