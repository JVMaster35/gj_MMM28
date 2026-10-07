using UnityEngine;

public class ChargeState : EnemyBaseState
{
    public ChargeState(Enemy enemy, string animName) : base(enemy, animName)
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

        if(Time.time >= _enemy.GetStateTime() + _enemy.GetEnemyData().chargeTime)
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
        else
        {
            if (_enemy.CheckForMeleeRange())
            {
                _enemy.SwitchState(_enemy.GetMeleeState());
            }
            else if (_enemy.CheckIfDodgeGuardTeleport())
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
                Charge();
        }
    }

    private void Charge()
    {
        _enemy.GetEnemyRGBody().linearVelocity = new Vector2(_enemy.GetEnemyData().chargeSpeed * _enemy.GetEnemyData().facingDirection, 
            _enemy.GetEnemyRGBody().linearVelocity.y);
    }
}
