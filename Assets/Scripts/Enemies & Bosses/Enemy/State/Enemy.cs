using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    #region Variables

    private Controller _controller;

    [SerializeField] private EnemyData _data;

    [SerializeField]
    private EnemyBaseState _currentState;
    [SerializeField]
    private PatrolState _patrolState;
    [SerializeField]
    private PlayerDetectionState _playerDetectionState;
    [SerializeField]
    private IdleState _idleState;
    [SerializeField]
    private ChargeState _chargeState;
    [SerializeField]
    private MeleeAttackState _meleeAttackState;
    [SerializeField]
    private ShootAttackState _shootAttackState;
    [SerializeField]
    private DodgeState _dodgeState;
    [SerializeField]
    private GuardState _guardState;
    [SerializeField]
    private TeleportState _teleportState;
    [SerializeField]
    private StunState _stunState;
    [SerializeField]
    private DamageState _damageState;
    [SerializeField]
    private DeathState _deathState;

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Rigidbody2D _body;
    [SerializeField]
    private Transform _ledgeDetect;
    [SerializeField]
    private GameObject _alertIcon;
    [SerializeField]
    private BoxCollider2D _colllider;

    private float stateTime = 1f;

    private EnemyGround _ground;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _idleState = new IdleState(this, "idle");
        _patrolState = new PatrolState(this, "patrol");
        _playerDetectionState = new PlayerDetectionState(this,"playerDetected");
        _chargeState = new ChargeState(this, "charge");
        _meleeAttackState = new MeleeAttackState(this, "melee");
        _shootAttackState = new ShootAttackState(this, "shoot");
        _dodgeState = new DodgeState(this, "dodge");
        _guardState = new GuardState(this, "guard");
        _damageState = new DamageState(this, "hurt");
        _stunState = new StunState(this, "stun");
        _deathState = new DeathState(this, "dead");

        _currentState = _patrolState;
        _currentState.Enter();

        _data.facingDirection = 1;

        _controller = GetComponent<Controller>();

        _ground = GetComponent<EnemyGround>();
    }

    void Update()
    {
        _currentState.LogicUpdate();

        CheckForObstacles();
        CheckForPlayer();
    }

    private void FixedUpdate()
    {
        _currentState.PhysicsUpdate();
    }
    #endregion

    #region Checks
    public bool CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(_ledgeDetect.position, Vector2.down, _data.ledgeDistance, _data.GroundLayer);
        RaycastHit2D obstacleHit = Physics2D.Raycast(_ledgeDetect.position, Vector2.right, _data.obstacleDistance, _data.ObstacleLayer);

        if(hit.collider == null || obstacleHit.collider == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForPlayer()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(_ledgeDetect.position, _data.facingDirection==1 ? 
            Vector2.right : Vector2.left, _data.playerDetectDistance, _data.PlayerLayer);
        if(playerHit.collider == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForMeleeRange()
    {
        RaycastHit2D meleeTargetHit = Physics2D.Raycast(_ledgeDetect.position, _data.facingDirection == 1 ? 
            Vector2.right : Vector2.left, _data.attackRange, _data.PlayerLayer);
        if (meleeTargetHit.collider == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForShootRange()
    {
        RaycastHit2D shootTargetHit = Physics2D.Raycast(_ledgeDetect.position, _data.facingDirection == 1 ? 
            Vector2.right : Vector2.left, _data.shootRange, _data.PlayerLayer);
        if (shootTargetHit.collider == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfDodgeGuardTeleport()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(_ledgeDetect.position, _data.facingDirection == 1 ?
            Vector2.right : Vector2.left, _data.distanceDGT, _data.PlayerLayer);
        bool aggressivePlayer = _data.facingDirection > 0 && _controller.input.RetrieveMovementInput() < 0
            || _data.facingDirection < 0 && _controller.input.RetrieveMovementInput() > 0;

        Debug.Log("Player Controller Input: " + _controller.input.RetrieveMovementInput());

        if (playerHit && aggressivePlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Other Functions
    public void SwitchState(EnemyBaseState _newState)
    {
        _currentState.Exit();
        _currentState = _newState;
        _currentState.Enter();
        stateTime = Time.time;
    }

    public void AnimationFinishTrigger()
    {
        _currentState.AnimationFinishTrigger();
    }

    public void AnimationAttackTrigger()
    {
        _currentState.AnimationAttackTrigger();
    }
    #endregion

    #region Get Variable Functions
    public EnemyData GetEnemyData()
    {
        return _data;
    }

    public Rigidbody2D GetEnemyRGBody()
    {
        return _body;
    }

    public IdleState GetIdleState()
    {
        return _idleState;
    }

    public PatrolState GetPatrolState()
    {
        return _patrolState;
    }

    public PlayerDetectionState GetPlayerDetectionState()
    {
        return _playerDetectionState;
    }

    public ChargeState GetChargeState()
    {
        return _chargeState;
    }

    public MeleeAttackState GetMeleeState()
    {
        return _meleeAttackState;
    }

    public ShootAttackState GetShootState()
    {
        return _shootAttackState;
    }

    public DodgeState GetDodgeState()
    {
        return _dodgeState;
    }

    public GuardState GetGuardState()
    {
        return _guardState;
    }

    public TeleportState GetTeleportState()
    {
        return _teleportState;
    }

    public StunState GetStunState()
    {
        return _stunState;
    }

    public DamageState GetDamageState()
    {
        return _damageState;
    }

    public DeathState GetDeathState()
    {
        return _deathState;
    }

    public EnemyGround GetGround()
    {
        return _ground;
    }

    public Transform GetEnemyLD()
    {
        return _ledgeDetect.transform;
    }

    public GameObject GetAlertIcon()
    {
        return _alertIcon;
    }

    public Animator GetEnemyAnim()
    {
        return _anim;
    }

    public BoxCollider2D GetEnemyCollider()
    {
        return _colllider;
    }


    public float GetStateTime()
    {
        return stateTime;
    }
    #endregion

    public void DestoryGameObject()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_ledgeDetect.position, (_data.facingDirection == 1 ? Vector2.right : Vector2.left) * _data.playerDetectDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_ledgeDetect.position, _data.attackRange);
    }
    
}
