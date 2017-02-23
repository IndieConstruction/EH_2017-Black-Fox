using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
[RequireComponent (typeof(Rigidbody))]
public class WireController : MonoBehaviour {

    public Transform Target;                                        //Transform of the point to connect to this GameObject
    private Transform origin;                                       //Transform of the point of origin of the Rope (also set as last element of the Rope)

    public float RopeMaxLength = 100000f;                           //Max length of the rope (in Unity's unity)
    public float FiniteElementDensity = 0.01f;                      //Density of Joints per unity (of Unity)
    public float RopeWidth = 100f;                                  //LineRenderer Width
    public Vector3 SwingAxis = new Vector3(1, 0, 1);                //Sets which axis the Joint will swing
    
    private List<GameObject> joints = new List<GameObject>();
    private LineRenderer lineRend;
    private float sphereColliderRadius
    {
        get
        {
            //SphereCollider's radius can't be larger than the rope
            var offSet = Vector3.Distance(transform.position, Target.position) * FiniteElementDensity;
            if (offSet > RopeWidth)
                return RopeWidth / 2;
            else
                return offSet;
        }
    }                           //Radius of the SphereCollider in Joints

    private void Start()
    {
        lineRend = GetComponent<LineRenderer>();

        origin = transform;
        joints.Add(gameObject);

        Extend();
    }

    private void FixedUpdate()
    {
        RenderOnJointsPositions();
    }

    void Extend()
    {
        int jointsToAdd = CountJointsRequired(origin.position, Target.position);        

        for (int i = 0; i < jointsToAdd; i++)
        {
            GameObject newPiece = Instantiate(gameObject);
            newPiece.GetComponent<WireController>().enabled = false;
            joints.Add(newPiece);
            AttachTogether(newPiece, joints[joints.Count - 1]);
        }

        //Attach to Target in the end
        AttachTogether(joints[joints.Count - 1], Target.gameObject);
        lineRend.numPositions = joints.Count +1;
    }
    
    //Physically connect Joints
    void AttachTogether(GameObject _itemToAttach, GameObject _targetOfItem)
    {
        CharacterJoint itemJoint;
        if (_itemToAttach.GetComponent<CharacterJoint>() != null)
            itemJoint = _itemToAttach.GetComponent<CharacterJoint>();
        else
            itemJoint = _itemToAttach.AddComponent<CharacterJoint>();

        Vector3 offSet = GetJointsOffSet();
        _itemToAttach.transform.position = _targetOfItem.transform.position + offSet.normalized;
        itemJoint.connectedBody = _targetOfItem.GetComponent<Rigidbody>();
    }
    
    //Relative position between Joints
    Vector3 GetJointsOffSet()
    {
        Vector3 direction = Target.position - origin.position;
        direction = direction.normalized * Vector3.Distance(origin.position, Target.position) * FiniteElementDensity;
        return direction;
    }

    //Calculate the Joints needed
    int CountJointsRequired(Vector3 _originalPos, Vector3 _targetPos)
    {
        int jointsRequired = (int)(Vector3.Distance(origin.position, Target.position) * FiniteElementDensity);
        return jointsRequired;
    }

    //Manage the Rendering of the Rope
    void RenderOnJointsPositions()
    {
        for (int i = 0; i < joints.Count -1; i++)
        {
            lineRend.SetPosition(i, joints[i].transform.position);
        }
        lineRend.SetPosition(joints.Count-1, Target.position);
    }
}
