using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackState : EnemyBaseState
{
    public MeleeAttackState(Enemy enemy, string animName) : base(enemy, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_enemy.GetEnemyLD().position, _enemy.GetEnemyData().attackRange, _enemy.GetEnemyData().DamagableLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            IKnockBack knockable = hitCollider.GetComponent<IKnockBack>();

            if (damageable != null)
            {
                knockable.Knockback(_enemy.GetEnemyData().knockBackForce, _enemy.GetEnemyData().knockBackAngle, _enemy.GetEnemyData().facingDirection);

                damageable.Damage(_enemy.GetEnemyData().attackDamage, _enemy.GetEnemyData().damageType);
            }
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _enemy.SwitchState(_enemy.GetPatrolState());
    }
}
