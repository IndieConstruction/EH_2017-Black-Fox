using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Interfaccia per tutti coloro che possono prendere punti (Solo player).
/// </summary>
public interface IKillable
{
    void CheckIfKillable(PlayerIndex _playerKiller);
}
