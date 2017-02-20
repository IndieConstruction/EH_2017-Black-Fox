using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interfaccia per tutti coloro che possono prendere punti (Solo player).
/// </summary>
public interface ICollectablePoints
{
    void CollectPoints(Avatar _killer, Avatar _victim);
}
