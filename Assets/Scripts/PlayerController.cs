using UnityEngine;
using AdvancedStateHandling;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float xInput;
    public Rigidbody2D rb;
    public bool isFacingRight;

    public Enemy currentCollidedEnemy;

    [Header("Conditions")]
    public bool canCheck;
    public bool isInInteraction;
    public bool jump;
    public bool isJumped;
    public bool isDamaged;

    public AdvancedStateMachine playerStateMachine;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
        GameManager.instance.blackBoard.SetValue("PlayerController", this);
        playerStateMachine = new AdvancedStateMachine();
        var moveState = new MoveState(this);
        var inInteractionState = new InInteractionState(this);
        var jumpState = new JumpState(this);
        var damageState = new DamageState(this);

        Add(moveState, jumpState, new FuncPredicate(() => jump));
        Add(jumpState, moveState, new FuncPredicate(() => !isJumped && rb.linearVelocity.y == 0f));
        Add(moveState, inInteractionState, new FuncPredicate(() => isInInteraction));
        Add(inInteractionState, moveState, new FuncPredicate(() => !isInInteraction));
        Add(jumpState, inInteractionState, new FuncPredicate(() => isInInteraction));
        Add(inInteractionState, jumpState, new FuncPredicate(() => !isInInteraction));

        Any(damageState, new FuncPredicate(() => isDamaged));

        Add(damageState, moveState, new FuncPredicate(() => !isDamaged));

        playerStateMachine.currentState = moveState;
        
    }

    public void Add(IState from, IState to, IPredicate condition)
    {
        playerStateMachine.AddTransition(from, to, condition);
    }

    public void Any(IState to, IPredicate condition)
    {
        playerStateMachine.AddTransitionFromAnytate(to, condition);
    }

    void Update()
    {
        SetXAxis();
        if (!isInInteraction)
        {
            SetRotation();
        }
        
        playerStateMachine.Update();
    }

    public void SetXAxis()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        
    }

    public void SetRotation()
    {
        if (isFacingRight && xInput<0)
        {
            transform.localScale = new Vector2 (-1, 1);
            isFacingRight = false;
        }
        if (!isFacingRight && xInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
            isFacingRight = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTrigger");
        if(collision.TryGetComponent<ICheckable>(out ICheckable checkable))
        {
            checkable.OnEnterCheck();
        }

        if(collision.TryGetComponent<InteractCandle>(out InteractCandle candle))
        {
            Debug.Log("Candle Interaction Started");
            candle.OnStartInteract();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICheckable>(out ICheckable checkable))
        {
            checkable.OnLeaveCheck();
        }
        if (collision.TryGetComponent<InteractCandle>(out InteractCandle candle))
        {
            candle.OnLeaveInteract();
        }
    }

    public void OnCollideWithEnemy(Enemy enemy)
    {
        currentCollidedEnemy = enemy;
        isDamaged = true;
    }



}
