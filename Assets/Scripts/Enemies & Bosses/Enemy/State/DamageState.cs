using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class DamageState : EnemyBaseState
{
    public float kBForce;
    public Vector2 kBAngle;
    public int kBDir;

    public DamageState(Enemy enemy, string animName) : base(enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ApplyKnockback(kBForce, kBAngle, kBDir);
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
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public void ApplyKnockback(float knockBackForce, Vector2 knockBackAngle, int faceDirection)
    {
        _enemy.GetEnemyRGBody().linearVelocity = new Vector2(knockBackAngle.x * faceDirection, knockBackAngle.y)
                    * knockBackForce;
    }
}
