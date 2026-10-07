using UnityEngine;

public class PlayerDetectionState : EnemyBaseState
{
    public PlayerDetectionState(Enemy enemy, string animName) : base (enemy, animName)
    {
        _enemy = enemy;
        _animName = animName;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.GetEnemyRGBody().linearVelocity = Vector2.zero;
        _enemy.GetAlertIcon().gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.GetAlertIcon().gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_enemy.CheckIfDodgeGuardTeleport())
        {
            if (_enemy.GetEnemyData().canDodge)
            {
                _enemy.SwitchState(_enemy.GetDodgeState());
            }

            if (_enemy.GetEnemyData().canGuard)
            {
                _enemy.SwitchState(_enemy.GetGuardState());
            }

            if (_enemy.GetEnemyData().canTeleport)
            {
                _enemy.SwitchState(_enemy.GetTeleportState());
            }
        }
        else if (!_enemy.CheckForPlayer())
        {
            _enemy.SwitchState(_enemy.GetPatrolState());
        }
        else
        {
            if (Time.time > _enemy.GetStateTime() + _enemy.GetEnemyData().playerDetectedWaitTime)
            {
                if (_enemy.GetEnemyData().isShooter && _enemy.CheckForShootRange())
                {
                    _enemy.SwitchState(_enemy.GetShootState());
                }
                else
                {
                    _enemy.SwitchState(_enemy.GetChargeState());
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
