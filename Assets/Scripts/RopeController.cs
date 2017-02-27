using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeController : MonoBehaviour
{
	public GameObject FragmentPrefab;
    public Transform AnchorPoint;	
	public int MaxLength = 80;
    public float Resolution = 0.01f;

	List<GameObject> fragments = new List<GameObject>();
    LineRenderer lineRend;

    float CurrentLength = 80;
    Vector3 offSet;
	
	float fragmentDistance;
	
	void Start()
	{
        ExtendRope(gameObject);
        lineRend = GetComponent<LineRenderer>();
        lineRend.numPositions = (fragments.Count) + 1;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < fragments.Count; i++)
        {
            lineRend.SetPosition(i, fragments[i].transform.position);
        }
        lineRend.SetPosition(fragments.Count, AnchorPoint.position);
    }


    void ExtendRope(GameObject _lastPiece)
    {
        offSet = GetOffSet(_lastPiece.transform);
        int _lastFragment = fragments.LastIndexOf(_lastPiece);

        for (int i = _lastFragment; i < MaxLength -1; i++)
        {
            //Add a new Fragment to the rope
            Vector3 position = fragments[i - 1].transform.position + offSet;
            GameObject newFragment = Instantiate(FragmentPrefab, position, Quaternion.identity);
            fragments.Add(newFragment);
            newFragment.transform.parent = transform;

            //Is the AnchroPoint closer than the offSet? If so stop building the rope
            if (fragmentDistance <= offSet.magnitude)
            {
                AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = newFragment.GetComponent<Rigidbody>();
                break;
            }
            //Connect the piece to the previus one 
            if (i > 0)
            {
                ConfigurableJoint joint = newFragment.GetComponent<ConfigurableJoint>();
                joint.connectedBody = fragments[i - 1].GetComponent<Rigidbody>();
            }          
        }        
    }

    Vector3 GetOffSet(Transform _origin)
    {
        //GetDirection
        Vector3 dir = AnchorPoint.position - _origin.position;
        dir = dir.normalized;

        //Measure OffSet
        float desiredOffSet = Vector3.Distance(AnchorPoint.position, _origin.position)*Resolution;

        float ropeWidth = GetComponent<LineRenderer>().widthMultiplier;
        //Return the minimum OffSet
        if (desiredOffSet >= ropeWidth)
        {
            fragmentDistance = ropeWidth;
            return dir * ropeWidth;
        }
        else
        {
            fragmentDistance = desiredOffSet;
            return dir * desiredOffSet;
        }
    }
}
