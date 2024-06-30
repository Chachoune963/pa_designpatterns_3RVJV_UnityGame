using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;
    
    public PlayerState(PlayerController player)
    {
        this.player = player;
    }
    
    public abstract void OnUpdate();
    public abstract void OnHit(AttackInfo attackInfo);
}

public class PlayerController : MonoBehaviour, Subscriber<int>, IDamageable
{
    public int MaxHealth
        => maxHealth;
    public ObservableValue<int> CurrentHealth
        => health;

    public float MovementSpeed => movementSpeed;

    [HideInInspector] [DoNotSerialize] public Vector2 CurrentVelocity;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;

    [SerializeField] private int maxHealth;
    [SerializeField] private float movementSpeed;

    private ObservableValue<int> health = new ObservableValue<int>();

    private PlayerState state;


    void Start()
    {
        health.Value = maxHealth;
        health.Subscribe(this);
        state = new NeutralPlayerState(this);
    }

    void Update()
    {
        state.OnUpdate();
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + CurrentVelocity * Time.fixedDeltaTime);
    }

    public void ChangeState(PlayerState newState)
    {
        state = newState;
    }
    public void Damage(AttackInfo attackInfo)
        => state.OnHit(attackInfo);

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Notify(int value)
    {
        if (value <= 0)
            Die();
    }
}
