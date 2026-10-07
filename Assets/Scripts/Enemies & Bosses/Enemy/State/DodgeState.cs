using UnityEngine;

public class DodgeState : EnemyBaseState
{
    public DodgeState(Enemy enemy, string animName) : base(enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.GetEnemyRGBody().linearVelocity = Vector2.zero;
        _enemy.GetEnemyData().startDodge = false;
        _enemy.GetEnemyData().endDodge = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > _enemy.GetStateTime() * .05f && !_enemy.GetEnemyData().startDodge)
        {
            StartDodge();
        }

        if(Time.time > _enemy.GetStateTime() + _enemy.GetEnemyData().cooldownDGT)
        {
            if (_enemy.CheckForPlayer())
            {
                _enemy.SwitchState(_enemy.GetPlayerDetectionState());
            }
            else
            {
                _enemy.SwitchState(_enemy.GetPatrolState());
            }
        }
        else if (Time.time > _enemy.GetStateTime() + _enemy.GetEnemyData().endDodgeTime && !_enemy.GetEnemyData().endDodge)
        {
            EndDodge();
        }
    }

    private void StartDodge()
    {
        _enemy.GetEnemyRGBody().linearVelocity = new Vector2(_enemy.GetEnemyData().dodgeAngle.x * -_enemy.GetEnemyData().facingDirection,
    _enemy.GetEnemyData().dodgeAngle.y) * _enemy.GetEnemyData().dodgeForce;
        _enemy.GetEnemyData().startDodge = true;
    }

    private void EndDodge()
    {
        _enemy.GetEnemyRGBody().linearVelocity = new Vector2(.1f + _enemy.GetEnemyData().facingDirection,-1) * _enemy.GetEnemyData().endDodgeForce;
        _enemy.GetEnemyData().endDodge = true;
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
}
