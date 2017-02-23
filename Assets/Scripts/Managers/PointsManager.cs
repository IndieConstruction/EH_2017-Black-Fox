using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


/// <summary>
/// Gestore dei punti del player
/// </summary>
public class PointsManager {

    int AddPoints;
    int SubPoints;
    int pointsToWin;
    List<PlayerPoints> pointsManager = new List<PlayerPoints>()
        { new PlayerPoints(PlayerIndex.One), new PlayerPoints(PlayerIndex.Two), new PlayerPoints(PlayerIndex.Three), new PlayerPoints(PlayerIndex.Four) };

    public PointsManager(int _killPoints, int _deathPoints, int _pointsToWin)
    {
        AddPoints = _killPoints;
        SubPoints = _deathPoints;
        pointsToWin = _pointsToWin;
    }

    public void UpdateKillPoints(PlayerIndex _killer, PlayerIndex _victim)
    {
        foreach (var item in pointsManager)
        {
            if (item.PlayerIndex == _killer)
            {
                item.KillPoints += AddPoints;
                if(item.KillPoints == pointsToWin)
                {
                    
                }
                break;
            }
        }

        foreach (var item in pointsManager)
        {
            if (item.PlayerIndex == _victim && item.KillPoints > 0)
            {
                item.KillPoints -= SubPoints;
                break;
            }
        }
        
    }


    void WhoWin()
    {
        GameManager.Instance.uiManager.WindDisplay.enabled = true;
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
