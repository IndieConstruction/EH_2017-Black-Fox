using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


/// <summary>
/// Gestore dei punti del player
/// </summary>
public class PointsManager {

    int killPoint;
    int deathPoint;
    int pointToWin;
    List<PlayerPoints> pointsManager = new List<PlayerPoints>()
        { new PlayerPoints(PlayerIndex.One), new PlayerPoints(PlayerIndex.Two), new PlayerPoints(PlayerIndex.Three), new PlayerPoints(PlayerIndex.Four) };

    public PointsManager(int _killPoint, int _deathPoint, int _pointToWin)
    {
        killPoint = _killPoint;
        deathPoint = _deathPoint;
        pointToWin = _pointToWin;
    }

    public void UpdateKillPoints(PlayerIndex _killer, PlayerIndex _victim)
    {
        foreach (var item in pointsManager)
        {
            if (item.PlayerIndex == _killer)
            {
                item.KillPoints += killPoint;
                if(item.KillPoints == pointToWin)
                {
                    //Di al gameManager che il player #n ha vinto
                }
                break;
            }
        }

        foreach (var item in pointsManager)
        {
            if (item.PlayerIndex == _victim && item.KillPoints > 0)
            {
                item.KillPoints -= deathPoint;
                break;
            }
        }
        
    }
}

/// <summary>
/// Contenitore dei punti del player
/// </summary>
public class PlayerPoints
{

    PlayerIndex playerIndex;
    int powerPoints;
    int killPoints;

    public PlayerIndex PlayerIndex
    {
        get { return playerIndex; }
    }

    public int KillPoints
    {
        get { return killPoints; }
        set { killPoints = value; }
    }

    public int PowerPoints
    {
        get { return powerPoints; }
        set { powerPoints = value; }
    }

    public PlayerPoints(PlayerIndex _playerIndex)
    {
        playerIndex = _playerIndex;
    }
}
