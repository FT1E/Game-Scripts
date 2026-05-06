using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : Entity
{

    [SerializeField]
    private float _maxSpeed;
    public float MaxSpeed { get { return _maxSpeed; } }

    
    [SerializeField]
    private PlayerInfo _playerInfo;
    public PlayerInfo playerInfo { get { return _playerInfo; } }

    [SerializeField]
    private StateSO initialState;
    private StateMachine stateMachine;

    [SerializeField]
    private InputReader _inputReader = default;
    

    [SerializeField]
    private Transform _cameraTransform = default;
    public Transform cameraTransform { get { return _cameraTransform; } }

    [SerializeField]
    private Rig _spineRig;
    public Rig spineRig { get { return _spineRig; } }

    [SerializeField]
    private Vector3 weaponScaleDuringAttack = Vector3.one;

    [SerializeField]
    private Shield shield;

    // * variables for knockback mode
    [SerializeField]
    private float weaponKnockbackMode_cooldown=10f;
    [SerializeField]
    private float weaponKnockbackMode_duration=5f;
    private float weaponKnockbackMode_timer=10f;    // default value so it can be cast right from the start, not on cooldown from start
    private bool knockbackMode = false; // whether knockback mode is currently on at run-time
    [SerializeField]
    private float weaponKnockbackMode_force=5f;

    [SerializeField]
    private Material weaponEmissionMaterial;
    [SerializeField]
    private ParticleSystem weaponParticles;

    [SerializeField]
    private UI_SO uiSO;

    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannel;
    [SerializeField]
    private SceneSO[] menuScenes;
    private bool calledMenuScenes = false;


    // Input variables

    // input move direction
    private Vector2 _moveDirection;
    // below get method for getting the relative to camera move direction
    public Vector2 MoveDirection {
        get
        {
            float angle = - Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y; 
            return new Vector2(
                _moveDirection.x * Mathf.Cos(angle) - _moveDirection.y * Mathf.Sin(angle),
                _moveDirection.x * Mathf.Sin(angle) + _moveDirection.y * Mathf.Cos(angle)
            );
        }
    }
    public Vector2 ForwardDirection {
        get
        {
            float angle = - Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y; 
            return new Vector2(
                -Mathf.Sin(angle),
                Mathf.Cos(angle)
            );
        }
    }    
    // end input move direction
    public bool isRunning;
    public bool jumpTrigger;
    public bool attackTrigger;
    public bool lightAttackTrigger;
    // end Input variables

    public bool attackTurn;
    public bool moving;

    private CharacterController characterController;

    // Input actions system stuff
     private void OnEnable()
    {
        _playerInfo.SetPlayer(this);

        _inputReader.moveEvent += OnMove;
        _inputReader.runStartEvent += RunStart;
        _inputReader.runStopEvent += RunStop;
        _inputReader.jumpStartEvent += JumpStart;
        _inputReader.jumpCancelEvent += JumpCancel;
        _inputReader.attackEvent += AttackTrigger;
        _inputReader.lightAttackEvent += LightAttackTrigger;
        
    }

    public void EnableShield() {
        _inputReader.shieldEvent += CastShield;
    }

    public void EnableKnockbackMode() {
        _inputReader.knockbackModeTriggerEvent += ActivateKnockbackMode;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.runStartEvent -= RunStart;
        _inputReader.runStopEvent -= RunStop;
        _inputReader.jumpStartEvent -= JumpStart;
        _inputReader.jumpCancelEvent -= JumpCancel;
        _inputReader.attackEvent -= AttackTrigger;
        _inputReader.lightAttackEvent -= LightAttackTrigger;
        _inputReader.shieldEvent -= CastShield;
        _inputReader.knockbackModeTriggerEvent -= ActivateKnockbackMode;

        // _inputReader.DisableInputSystem();

        
    }

    // below methods attached to unity input actions system
    private void OnMove(Vector2 input)
    {
        _moveDirection = input;
    }
    private void RunStart()
    {
        isRunning = true;
    }

    private void RunStop()
    {
        isRunning = false;
    }

    private void JumpStart()
    {
        jumpTrigger = true;
    }

    private void JumpCancel()
    {
        jumpTrigger = false;
    }

    private void AttackTrigger() {
        attackTrigger = true;
    }

    private void LightAttackTrigger()
    {
        lightAttackTrigger = true;
    }

    private void CastShield()
    {
        if(!shield.Activate())
        {
            Debug.Log($"Shield on cooldown. Time left: {shield.cooldown:0.00}s");
            uiSO.playerUI.SetCooldownText($"Shield on cooldown. Time left: {shield.cooldown:0.00}s");
        }
    }
    private void ActivateKnockbackMode()
    {
        if(knockbackMode) return;

        if(weaponKnockbackMode_timer >= weaponKnockbackMode_cooldown)
        {
            StartCoroutine(KnockbackModeCoroutine());
        }
        else
        {
            Debug.Log($"Knockback Mode on cooldown. Time left: {weaponKnockbackMode_cooldown - weaponKnockbackMode_timer:0.00}s");
            uiSO.playerUI.SetCooldownText($"Knockback Mode on cooldown. Time left: {weaponKnockbackMode_cooldown - weaponKnockbackMode_timer:0.00}s");
        }
    }
    // end Input actions system stuff

    // weapon knockback mode
    private IEnumerator KnockbackModeCoroutine()
    {
        knockbackMode = true;
        // * enable emmision on material - so weapon shines a bit
        weaponEmissionMaterial.EnableKeyword("_EMISSION");
        weaponEmissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        weaponParticles.Play();

        weapon.SetKnockback(weaponKnockbackMode_force);
        
        yield return new WaitForSeconds(weaponKnockbackMode_duration);
        
        weaponParticles.Stop();
        
        weapon.SetKnockback(0f);
        
        weaponEmissionMaterial.DisableKeyword("_EMISSION");
        weaponEmissionMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        
        
        knockbackMode = false;
        weaponKnockbackMode_timer = 0f; // start cooldown after duration ends
    }

    // managing weapon attack collisions

    public override void EnableWeaponCollision()
    {
        base.EnableWeaponCollision();
        weapon.transform.localScale = weaponScaleDuringAttack;
    }
    public override void DisableWeaponCollision(string animatorParam)
    {
        weapon.transform.localScale = Vector3.one;
        lightAttackTrigger = false;
        DisableTorsoLayer();
        attackTrigger = false;      // consume input
        base.DisableWeaponCollision(animatorParam);
    }
    public void SetAnimBoolFalse()
    {
        animator.SetBool("Attack1", false);
    }

    public void EnableTorsoLayer()
    {
        animator.SetLayerWeight(1, 1f);
    }
    public void DisableTorsoLayer()
    {
        // 0.001 instead of 0 so the animation is played and the event which sets the layer weight to 1 is called
        // and small value so it doesn't override the base layer
        animator.SetLayerWeight(1, 0.001f);
    }

    // end weapon attack collisions


    void Awake()
    {
        if(_cameraTransform == null) _cameraTransform = Camera.main.transform;
        
        characterController = GetComponent<CharacterController>();
        stateMachine = new StateMachine(initialState);
        weapon.SetHitLayer(7);
        weapon.cancelAttacks = true;
    }

    void Start()
    {
        _inputReader.EnableGameplay();
    }
    void Update()
    {
        if(Health <= 0f && !calledMenuScenes)
        {
            sceneLoaderChannel.RaiseEvent(menuScenes);
            calledMenuScenes = true;
            return;
        }

        if(weaponKnockbackMode_timer < weaponKnockbackMode_cooldown) weaponKnockbackMode_timer += Time.deltaTime;
        else weaponKnockbackMode_timer = weaponKnockbackMode_cooldown;
        
        isGrounded = characterController.isGrounded;
        animator.SetFloat("speed", MyPhysics.GetCurrentSpeed(this) / MaxSpeed);

        MyPhysics.ApplyGravity(this);
        if (MyPhysics.GetCurrentSpeed(this) > MaxSpeed || !moving) MyPhysics.ApplyDragOnVelocityVector(this);
        
        stateMachine.Update(this);
        characterController.Move(velocityVector * Time.deltaTime);

        _playerInfo.SetPosition(transform.position);
        uiSO.playerUI.UpdateHPBar(Health, maxHealth);
    }

}