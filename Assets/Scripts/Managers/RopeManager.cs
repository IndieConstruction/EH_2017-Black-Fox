using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(ConfigurableJoint))]
/// <summary>
/// Used to manage the rope between the component and the Target position
/// </summary>
public class RopeManager : MonoBehaviour
{

    public Transform Target;                                        //Transform of the point to connect to this GameObject
    private Transform origin;                                       //Transform of the point of origin of the Rope (also set as last element of the Rope)
    private Rigidbody anchoredBody;                                 //The Rigidbody connected to the rope
    public float RopeMaxLength = 100000f;                           //Max length of the rope (in Unity's unity)
    public float FiniteElementDensity = 0.01f;                      //Density of Joints per unity (of Unity)
    public float RopeWidth = 100f;                                  //LineRenderer Width
    public int CollisionLayer = 9;
    private float mass;                                             //Rigidbodies variables
    private float drag;
    private float angularDrag;
    private RigidbodyConstraints constraints = new RigidbodyConstraints();
    private Vector3 cjAnchor;                                       //Configurable Joint variables
    private Vector3 cjAxis;
    private Vector3 cjSecondaryAxis;
    private ConfigurableJointMotion[] cjMotion = new ConfigurableJointMotion[6];
    private JointDrive xDrive;
    private JointDrive yzDrive;
    private JointProjectionMode projectionMode;
    private float projectionDistance;
    private float projectionAngle;
    private List<GameObject> joints = new List<GameObject>();       //Collection of Joints that describe the rope
    private LineRenderer lineRend;                                  //Reference to the LineRenderer
    private int totalJoints = 0;                                    //Total amount of Joints
    private float ropeCurrentLength=0;                              //Current length of the rope (in Unity's unity)
    
    private float sphereColliderRadius
    {
        get
        {
            //SphereCollider's radius can't be larger than the rope
            var offSet = Vector3.Distance(transform.position, Target.position) * FiniteElementDensity;
            if (offSet > RopeWidth)
                return RopeWidth/2;
            else
                return offSet;
        }
    }                           //Radius of the SphereCollider in Joints

    private void Awake()
    {
        //Get reference to LineRederer
        lineRend = GetComponent<LineRenderer>();
        //Get reference to the connected Rigidbody
        anchoredBody = GetComponent<ConfigurableJoint>().connectedBody;
        //Setup of the joints based onthe the current ConfigurableJoint and Rigidbody
        Rigidbody rigidOnMe = GetComponent<Rigidbody>();
        mass = rigidOnMe.mass;
        drag = rigidOnMe.drag;
        angularDrag = rigidOnMe.angularDrag;
        constraints = rigidOnMe.constraints;

        ConfigurableJoint cjOnMe = GetComponent<ConfigurableJoint>();
        cjAnchor = cjOnMe.anchor;
        cjAxis = cjOnMe.axis;
        cjSecondaryAxis = cjOnMe.secondaryAxis;
        cjMotion[0] = cjOnMe.xMotion;
        cjMotion[1] = cjOnMe.yMotion;
        cjMotion[2] = cjOnMe.zMotion;
        cjMotion[3] = cjOnMe.angularXMotion;
        cjMotion[4] = cjOnMe.angularYMotion;
        cjMotion[5] = cjOnMe.angularZMotion;
        xDrive = cjOnMe.xDrive;
        yzDrive = cjOnMe.angularYZDrive;
        projectionMode = cjOnMe.projectionMode;
        projectionDistance = cjOnMe.projectionDistance;
        projectionAngle = cjOnMe.projectionAngle;

        //RopeManager setup
        lineRend.widthMultiplier = RopeWidth;        

        //Set this gameObject as origin of the rope
        origin = transform;
        joints.Add(gameObject);
        totalJoints++;

        //Build the Rope
        ExtendRope();
    }

    private void Update()
    {
        if(anchoredBody.velocity.sqrMagnitude > GetComponent<Rigidbody>().velocity.sqrMagnitude)
        {
            anchoredBody.AddRelativeForce(anchoredBody.velocity,ForceMode.VelocityChange);
        }
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
        ropeCurrentLength += Vector3.Distance(origin.position, Target.position);
        totalJoints = (int)(ropeCurrentLength * FiniteElementDensity);

        //Update the position of the points for the Line Renderer
        lineRend.numPositions = totalJoints;

        //Increase the length of the rope
        for (int i = oldJointsAmount; i < totalJoints - 2; i++)
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
        var separation = ((Target.position) - origin.position) / (totalJoints-1);

        for (int i = joints.LastIndexOf(origin.gameObject) + 1; i < totalJoints - 2; i++)
        {
            //Create a new joint
            joints[i].name = ("Joint " + i);

            pos = (separation * i) + origin.position;
            joints[i].transform.position = pos;

            //Set the parent of the joint
            joints[i].transform.parent = transform;

            //Set SphereColliders, Rigidbodies and HingeJoints
            AdjustJointPhysics(joints[i], joints[i - 1].GetComponent<Rigidbody>());
        }

        //Setup of the current GameObject
        AdjustJointPhysics(joints[0]);
        //Setup of the Target's CharacterJoint
        AdjustJointPhysics(Target.gameObject);
    }

    /// <summary>
    /// Set the parameter of SphereCollider, Rigidbody and HingeJoint
    /// </summary>
    /// <param name="_jointToSetup">The Joint to setup</param>
    void AdjustJointPhysics(GameObject _jointToSetup)
    {
        ConfigurableJoint currentCJ;
        Rigidbody currentRigid;

        //Reference to HingeJoint
        if (_jointToSetup.GetComponent<ConfigurableJoint>() == null)
            currentCJ = _jointToSetup.AddComponent<ConfigurableJoint>();
        else
            currentCJ = _jointToSetup.GetComponent<ConfigurableJoint>();

        //Reference to Rigidbody (existence assured by the presence of the HingeJoint)
        currentRigid = _jointToSetup.GetComponent<Rigidbody>();

        //Setup of the Rigidbody
        currentRigid.mass = mass;
        currentRigid.drag = drag;
        currentRigid.angularDrag = angularDrag;

        if (_jointToSetup == Target.gameObject)
        {
            //Configurable Joint Setup
            currentCJ.connectedBody = joints[joints.LastIndexOf(Target.gameObject) - 1].GetComponent<Rigidbody>();
            
            currentCJ.anchor = cjAnchor;
            currentCJ.axis = cjAxis;
            currentCJ.secondaryAxis = cjSecondaryAxis;
            currentCJ.xMotion = cjMotion[0];
            currentCJ.yMotion = cjMotion[1];
            currentCJ.zMotion = cjMotion[2];
            currentCJ.angularXMotion = cjMotion[3];
            currentCJ.angularYMotion = cjMotion[4];
            currentCJ.angularZMotion = cjMotion[5];
            currentCJ.xDrive = xDrive;
            currentCJ.yDrive = yzDrive;
            currentCJ.projectionMode = projectionMode;
            currentCJ.projectionDistance = projectionDistance;
            currentCJ.projectionDistance = projectionAngle;
        }
            

        //Set the proper layer ("Rope")
        _jointToSetup.layer = CollisionLayer;        
    }

    /// <summary>
    /// Set the parameter of SphereCollider, Rigidbody and HingeJoint
    /// </summary>
    /// <param name="_jointToSetup">The Joint to setup</param>
    /// <param name="_rbToConnect">The Rigidbody to which connet the Joint</param>
    void AdjustJointPhysics(GameObject _jointToSetup, Rigidbody _rbToConnect)
    {
        SphereCollider coll;
        Rigidbody currentRigid;
        ConfigurableJoint currentCJ;

        //Reference to SphereCollider
        if (_jointToSetup.GetComponent<SphereCollider>() == null)
            coll = _jointToSetup.AddComponent<SphereCollider>();
        else
            coll = _jointToSetup.GetComponent<SphereCollider>();

        //Reference to ConfigurableJoint
        if (_jointToSetup.GetComponent<ConfigurableJoint>() == null)
            currentCJ = _jointToSetup.AddComponent<ConfigurableJoint>();
        else
            currentCJ = _jointToSetup.GetComponent<ConfigurableJoint>();

        //Reference to Rigidbody
        currentRigid = _jointToSetup.GetComponent<Rigidbody>();

        //Configurable Joint Setup        
        currentCJ.connectedBody = _rbToConnect;

        currentCJ.anchor = cjAnchor;
        currentCJ.axis = cjAxis;
        currentCJ.secondaryAxis = cjSecondaryAxis;
        currentCJ.xMotion = cjMotion[0];
        currentCJ.yMotion = cjMotion[1];
        currentCJ.zMotion = cjMotion[2];
        currentCJ.angularXMotion = cjMotion[3];
        currentCJ.angularYMotion = cjMotion[4];
        currentCJ.angularZMotion = cjMotion[5];
        currentCJ.xDrive = xDrive;
        currentCJ.yDrive = yzDrive;
        currentCJ.projectionMode = projectionMode;
        currentCJ.projectionDistance = projectionDistance;
        currentCJ.projectionDistance = projectionAngle;

        //Setup of the Rigidbody
        currentRigid.mass = mass;
        currentRigid.drag = drag;
        currentRigid.angularDrag = angularDrag;

        //Setup of the SphereCollider
        coll.radius = sphereColliderRadius;        
        coll.isTrigger = false;

        //Set the proper layer ("Rope")
        _jointToSetup.layer = CollisionLayer;
    }


    #region To Move Into a Library
    /// <summary>
    /// Copy a Component into a GameObject
    /// </summary>
    /// <typeparam name="T">Type of Component to Copy</typeparam>
    /// <param name="_original">Original from where copy to</param>
    /// <param name="_destination">The GameObject that will recive the copy</param>
    /// <returns></returns>
    T CopyComponent<T>(T _original, GameObject _destination) where T : Component
    {
        System.Type type = _original.GetType();
        //Check if the Component is already there or have to be instantiated
        var dst = _destination.GetComponent(type) as T;
        if (!dst) dst = _destination.AddComponent(type) as T;
        //Actual copy of fields
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(_original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(_original, null), null);
        }
        return dst as T;
    }

    #endregion
}

