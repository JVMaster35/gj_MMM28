using UnityEngine;

public class IdleState : EnemyBaseState
{
    public IdleState(Enemy enemy, string animName) : base(enemy, animName)
    {
        _enemy = enemy;
        _animName = animName;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_enemy.CheckForPlayer())
        {
            _enemy.SwitchState(_enemy.GetPlayerDetectionState());
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
