using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Scriptable Objects/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    // used for enemy behaviour
    // melee mobs - just follow player and attack
    private Vector3 _position;
    public Vector3 position{ get { return _position; } }

    public void SetPosition(Vector3 val)
    {
        _position = val;
    }
}
