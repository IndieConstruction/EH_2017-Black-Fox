using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackFox;

public class ShooterBase : MonoBehaviour {

    protected ShooterBaseConfig shooterBaseConfig;

    private Vector3 _direction;
    protected Vector3 direction
    {
        get
        {
            if (_direction == null || _direction == Vector3.zero)
                return Vector3.forward;
            else
                return _direction;
        }
        set { _direction = value; }
    }

    #region API
    /// <summary>
    /// Spara un proiettile
    /// </summary>
    public virtual void ShootBullet()
    {
        Debug.Log(direction);
        GameObject instantiatedProjectile = Instantiate(shooterBaseConfig.ProjectilePrefab, transform.position + direction, Quaternion.LookRotation(direction));
        instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(direction.normalized * shooterBaseConfig.BulletSpeed, ForceMode.Impulse);
        instantiatedProjectile.GetComponent<Projectile>().SetOwner(GetComponentInParent<IShooter>());
        Destroy(instantiatedProjectile, shooterBaseConfig.LifeTime);
    }
    /// <summary>
    /// Determina la direzione di fuco. Spara verso Vector3.Forward se non settato manualmente
    /// </summary>
    /// <param name="_direction"></param>
    public virtual void SetFireDirection(Vector3 _direction) { }
    #endregion
}

[Serializable]
public class ShooterBaseConfig
{
    public GameObject ProjectilePrefab;
    public float LifeTime;
    public float BulletSpeed;
}
