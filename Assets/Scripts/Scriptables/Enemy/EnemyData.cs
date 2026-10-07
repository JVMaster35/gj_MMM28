using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Health & Shield")]
    public float maxHealth = 100f;
    public bool isShielded, isBroken, isHeavy;
    public float maxShieldStr, shieldRegenRate, shieldReducution;
    public float stunTime;
    public WaitForSeconds shieldRegenTime;
    public GenderType shieldType;
    public float healthRegen, regenTime;
    public Vector3 offsetUI;

    [Header("Movement")]
    public float moveSpeed;
    public float chargeTime, chargeSpeed;
    public int facingDirection = 1;
    public bool isAffected;

    [Header("Detection")]
    public bool playerDetected;
    public float ledgeDistance, obstacleDistance, playerDetectDistance;
    public float detectionPauseTime, playerDetectedWaitTime;

    [Header("Melee")]
    public int attackDamage;
    public float attackRate;
    public float nextAttackTime;
    public float attackRange;
    public float knockBackForce;
    public Vector2 knockBackAngle;
    public GenderType damageType;

    [Header("Shooter")]
    public bool isShooter;
    public bool canShoot;
    public int shotDamage;
    public float shootRange;
    public float rateOfFire;
    public GenderType shotType;

    [Header("Dodge, Guard & Teleport")]
    public float dodgeForce, endDodgeForce, endDodgeTime;
    public float guardStr, endGuardTime, endTeleportTime;
    public float teleportTime;
    public float distanceDGT;
    public float cooldownDGT;
    public Vector2 teleportDis, dodgeAngle;
    public bool canDodge, canGuard, canTeleport;
    public bool startDodge, startGuard, startTeleport;
    public bool endDodge, endGuard, endTeleport;

    [Header("Layers")]
    public LayerMask GroundLayer, HazardLayer, ObstacleLayer, PlayerLayer, DamagableLayers;
}
