using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestore dei punti del player
/// </summary>
public class PointsManager {

    int killPoint;
    int deathPoint;
    int pointToWin;
    List<PlayerPoints> pointsManager = new List<PlayerPoints>()
        { new PlayerPoints(PlayerID.One), new PlayerPoints(PlayerID.Two), new PlayerPoints(PlayerID.Three), new PlayerPoints(PlayerID.Four) };

    public PointsManager(int _killPoint, int _deathPoint, int _pointToWin)
    {
        killPoint = _killPoint;
        deathPoint = _deathPoint;
        pointToWin = _pointToWin;
    }

    public void UpdateKillPoints(PlayerID _killer, PlayerID _victim)
    {
        foreach (var item in pointsManager)
        {
            if (item.PlayerID == _killer)
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
            if (item.PlayerID == _victim && item.KillPoints > 0)
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

    PlayerID playerID;
    int powerPoints;
    int killPoints;

    public PlayerID PlayerID
    {
        get { return playerID; }
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

    public PlayerPoints(PlayerID _IDPlayer)
    {
        playerID = _IDPlayer;
    }
}
