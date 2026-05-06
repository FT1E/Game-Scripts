using UnityEngine;

[CreateAssetMenu(fileName = "UI_SO", menuName = "ScriptableObjects/UI_SO", order = 1)]
public class UI_SO : ScriptableObject
{
    
    private PlayerUI _playerUI;
    public PlayerUI playerUI { get {return _playerUI;} }
    
    public void setPlayerUI(PlayerUI playerUI) {
        _playerUI = playerUI;
    }
}