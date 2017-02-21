

public class AvatarParameters {

    public PlayerID playerID;
    public string PlayerName;
    public float Life;
    public float PowerPoint;

    public AvatarParameters(PlayerID _IDPlayer, string _name, float _life, float _powerPoint)
    {
        playerID = _IDPlayer;
        PlayerName = _name;
        Life = _life;
        PowerPoint = _powerPoint;
    }

}
