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

    float CurrentLength;
    Vector3 offSet;
	
	float fragmentDistance;
	
	void Start()
    {
        lineRend = GetComponent<LineRenderer>();

        fragments.Add(gameObject);
        ExtendRope(gameObject);
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

    /// <summary>
    /// Extend the rope toward the AnchorPoint
    /// </summary>
    /// <param name="_lastPiece">Current end of the rope</param>
    void ExtendRope(GameObject _lastPiece)
    {
        offSet = GetOffSet(_lastPiece.transform);
        int _lastFragment = fragments.LastIndexOf(_lastPiece);

        for (int i = _lastFragment+1; i < MaxLength -1; i++)
        {
            //Add a new Fragment to the rope
            Vector3 position = fragments[i - 1].transform.position + offSet;
            GameObject newFragment = Instantiate(FragmentPrefab, position, Quaternion.identity);
            fragments.Add(newFragment);
            newFragment.transform.parent = transform;

            ConfigurableJoint joint = newFragment.GetComponent<ConfigurableJoint>();
            joint.connectedBody = fragments[i - 1].GetComponent<Rigidbody>();

            //Is the AnchroPoint closer than the offSet? If so stop building the rope
            if (Vector3.Distance(newFragment.transform.position, AnchorPoint.position) <= fragmentDistance)
            {
                AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = newFragment.GetComponent<Rigidbody>();
                break;
            }
                    
        }        
    }
    /// <summary>
    /// Get the offSet vector toward the AnchorPoint
    /// </summary>
    /// <param name="_origin">Starting position</param>
    /// <returns>OffSet vector</returns>
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
