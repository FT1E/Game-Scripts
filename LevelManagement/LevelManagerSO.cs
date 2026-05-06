using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerSO", menuName = "LevelManagerSO", order = 0)]
public class LevelManagerSO : ScriptableObject
{
    [SerializeField]
    public int[] mobWaveSizes;

    private LevelManager _levelManager;
    public LevelManager monoBehaviour { get { return _levelManager; }}

    public float progress {
        get
        {
            if(monoBehaviour == null || mobWaveSizes.Length == 0) return 0f;
            return monoBehaviour.mobsKilled / (float)mobWaveSizes[mobWaveSizes.Length - 1];
        }
    }


    // * below for disabling some actions on some levels
    // * ex. I don't want player to cast shield on level 1
    [SerializeField]
    private PlayerInfo _playerInfo;
    public PlayerInfo playerInfo { get { return _playerInfo; } }

    [SerializeField]
    private bool _shieldEnabled;
    public bool shieldEnabled { get { return _shieldEnabled; } }

    [SerializeField]
    private bool _knockbackEnabled;
    public bool knockbackEnabled { get { return _knockbackEnabled; } }

    public void SetLevelManager(LevelManager levelManager)
    {
        this._levelManager = levelManager;
    }

    public void SetPlayerAbilities()
    {
        if(_playerInfo.player == null) return;
        if(_shieldEnabled) _playerInfo.player.EnableShield();
        if(_knockbackEnabled) _playerInfo.player.EnableKnockbackMode();
    }
}