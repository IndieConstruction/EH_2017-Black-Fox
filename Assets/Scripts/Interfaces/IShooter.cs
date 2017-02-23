using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Interfaccia per tutti coloro che hanno la capacità di sparare.
/// </summary>
public interface IShooter {

    List<IDamageable> GetDamageable();

    PlayerIndex GetOwner();
}
