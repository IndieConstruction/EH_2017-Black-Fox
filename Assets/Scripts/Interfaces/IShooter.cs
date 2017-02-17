using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter {

    List<IDamageable> GetDamageable();

    GameObject GetOwner();
}
