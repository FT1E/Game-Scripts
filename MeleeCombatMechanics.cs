using UnityEngine;

public class MeleeCombatMechanics : MonoBehaviour
{

    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private Animator animator = default;
    private System.Random random = new System.Random();

    [SerializeField] private int numberOfSlashAttacks = 3;

    [SerializeField] private Weapon weapon = default;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.attackEvent += RandomSlashAttack;

    }

    private void OnDisable()
    {
        _inputReader.attackEvent -= RandomSlashAttack;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomSlashAttack() {
        //animator.SetTrigger($"Attack{random.Next(numberOfSlashAttacks)}");
        weapon.Attack(random.Next(numberOfSlashAttacks), animator);
    }
}
