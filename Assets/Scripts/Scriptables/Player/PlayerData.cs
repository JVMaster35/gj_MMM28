using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Health & Defence")]
    public float maxHealth = 100f;
    public float healthRegenRate;
    public float maxHealthRegen;
    public float defencePower;
    public bool isDead;

    [Header("Movement")]
    public float moveSpeed;
    public float _maxSpeedChange, _acceleration;
    public float _maxAcceleration;
    public float _maxAirAcceleration;
    public Vector2 _direction, _desiredVelocity, _velocity;

    [Header("Jump")]
    public int _jumpPhase;
    public int _maxAirJumps;
    public float jumpPower;
    public float _downwardMovementMultiplier;
    public float _upwardMovementMultiplier;
    public float _defaultGravityScale, _jumpSpeed;
    public bool _desiredJump;

    [Header("Attack")]
    public int attackDamage;
    public float attackRate;
    public float nextAttackTime;
    public float attackRange;

    [Header("Interact")]
    public bool interaction;

    [Header("Gender")]
    public GenderType gender;
    public float changeRecharge;
    public float nearDeathHealthRegain;
    public GameObject genderSwitchPuff;

    [Header("Layers")]
    public LayerMask GroundLayer, HazardLayer, EnemyLayer, BossLayer, DamageableLayers, CollectableLayers;

    [Header("Ability Checker")]
    public bool canDoubleJump;
    public bool canDash;

    [Header("DoubleJump")]
    public float doubleJumpPower;

    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float afterImageLifeTime, timeBetweenAfterImages;
    public float waitAfterDashing;
    public float dashCounter;
    public float afterImageCounter;
    public float dashRechargeCounter;


}
