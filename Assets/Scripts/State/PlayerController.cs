using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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

public class PlayerController : MonoBehaviour, IDamageable
{
    public int CurrentHealth
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
                Die();
        }
    }

    public float MovementSpeed => movementSpeed;

    [HideInInspector] [DoNotSerialize] public Vector2 CurrentVelocity;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;

    [SerializeField] private int health;
    [SerializeField] private float movementSpeed;

    private PlayerState state;


    void Start()
    {
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
}
