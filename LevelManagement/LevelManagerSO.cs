using Borodar.FarlandSkies.CloudyCrownPro;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerSO", menuName = "LevelManagerSO", order = 0)]
public class LevelManagerSO : ScriptableObject
{
    [SerializeField]
    public int[] mobWaveSizes;

    private LevelManager _levelManager;
    public LevelManager monoBehaviour { get { return _levelManager; }}

    [Header("Skybox settings")]
    [SerializeField]
    private bool _dayNightCycle = false;
    public bool dayNightCycle { get { return _dayNightCycle; } }
    [SerializeField]
    private float _dayNightCycleDuration = 60f;
    public float dayNightCycleDuration { get { return _dayNightCycleDuration; } }

    [Tooltip("0 to 100, 0 is midnight, 50 is 12pm and 100 is midnight again")]
    [SerializeField]
    private float _startingDayNightCycleProgress = 50f;
    public float startingDayNightCycleProgress { get { return _startingDayNightCycleProgress; } }
    

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