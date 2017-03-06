using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour {

    private Transform target;

    public Transform spawner;
    public Transform player1, player2, player3, player4;

    public Rigidbody Bullet;
    public float maxDistance = 100.000f;
    public float Force;






    // Use this for initialization
    void Start () {

        target = null;



    }
	
	
    
    
    
    
    
    // Update is called once per frame
	void Update () {

        if (target == null)
        {
            ChooseTarget(player1);
            ChooseTarget(player2);
            ChooseTarget(player3);
            ChooseTarget(player4);
        }

        else
        {
            FollowTarget();
        }

    }

    void ChooseTarget(Transform _player)
    {
        if (Vector3.Distance(transform.position, _player.position) < maxDistance && target == null)
            target = _player;

    }




    void FollowTarget()
    {
        if (Vector3.Distance(target.position,transform.position)< maxDistance)
        {
            Vector3 _target = new Vector3(target.position.x, 0f, target.position.z);
            transform.LookAt(_target);
            Shoot();

        }
        else
        {
            target = null;

        }


    }

    void Shoot()
    {
        Rigidbody go = Instantiate(Bullet, spawner.position, Quaternion.identity);
        go.AddRelativeForce(Vector3.forward * Force, ForceMode.Impulse);
        Destroy(go, 3f);

    }

    void AutoKillbullet()
    {
        
    }
}
