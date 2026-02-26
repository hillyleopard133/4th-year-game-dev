using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [FormerlySerializedAs("speed")]
    [Header("Config")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    
    public float WalkSpeed  => walkSpeed;
    public float SprintSpeed => sprintSpeed;

    public Vector2 MoveDirection => moveDirection;

    private PlayerAnimations playerAnimations;
    private PlayerActions actions;
    private Player player;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;

    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    void Update()
    {
        ReadMovement();
    }    
    
    private void FixedUpdate()
    {
        Move();
    }
    

    private void Move()
    {
        rb2D.MovePosition(rb2D.position + moveDirection * (walkSpeed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
        if(moveDirection == Vector2.zero)
        {
            playerAnimations.SetMoveBoolTransition(false);
            return;
        }
        
        playerAnimations.SetMoveBoolTransition(true);
        playerAnimations.SetMoveAnimation(moveDirection);
    }

    private void EnableMovement()
    {
        actions.Movement.Enable();
    }

    private void DisableMovement()
    {
        actions.Movement.Disable();
    }

    private void OnEnable()
    {
        EnableMovement();
    }

    private void OnDisable()
    {
        DisableMovement();
    }
}
