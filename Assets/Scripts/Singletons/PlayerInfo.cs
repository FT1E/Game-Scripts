using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Scriptable Objects/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    // used for enemy behaviour
    // melee mobs - just follow player and attack
    private Vector3 _position;
    public Vector3 position{ get { return _position; } }

    private Player _player;
    public Player player { get { return _player; }}

    public void SetPosition(Vector3 val)
    {
        _position = val;
    }

    public void SetPlayer(Player val)
    {
        _player = val;
    }
}
