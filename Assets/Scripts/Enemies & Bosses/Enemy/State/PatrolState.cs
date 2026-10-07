using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public PatrolState(Enemy enemy, string animName) : base (enemy, animName)
    {
        _enemy = enemy;
        _animName = animName;
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
        else if (_enemy.CheckForPlayer())
        {
            _enemy.SwitchState(_enemy.GetPlayerDetectionState());
        }
        else if (_enemy.CheckForObstacles())
        {
            Rotate();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        PatrolArea();
    }

    private void PatrolArea()
    {
        if (!_enemy.GetEnemyData().playerDetected)
        {
            if (_enemy.GetEnemyData().facingDirection == 1)
            {
                if (_enemy.GetEnemyData().isAffected)
                {
                    _enemy.GetEnemyRGBody().linearVelocity = new Vector2(_enemy.GetEnemyData().moveSpeed, _enemy.GetEnemyRGBody().linearVelocity.y)
                        * Mathf.Max(_enemy.GetEnemyData().moveSpeed - _enemy.GetGround().Friction, 0f);
                }
                else
                {
                    _enemy.GetEnemyRGBody().linearVelocity = new Vector2(_enemy.GetEnemyData().moveSpeed, _enemy.GetEnemyRGBody().linearVelocity.y);
                }
            }
            else
            {
                if (_enemy.GetEnemyData().isAffected)
                {
                    _enemy.GetEnemyRGBody().linearVelocity = new Vector2(-_enemy.GetEnemyData().moveSpeed, _enemy.GetEnemyRGBody().linearVelocity.y)
                        * Mathf.Max(_enemy.GetEnemyData().moveSpeed - _enemy.GetGround().Friction, 0f);
                }
                else
                {
                    _enemy.GetEnemyRGBody().linearVelocity = new Vector2(-_enemy.GetEnemyData().moveSpeed, _enemy.GetEnemyRGBody().linearVelocity.y);
                }
            }
        }
    }

    private void Rotate()
    {
        if (_enemy.GetEnemyData().facingDirection == 1)
        {
            _enemy.transform.localScale = new Vector3(-_enemy.transform.localScale.x, _enemy.transform.localScale.y, _enemy.transform.localScale.z);
            _enemy.GetEnemyData().facingDirection = -1;
        }
        else
        {
            _enemy.transform.localScale = new Vector3(-_enemy.transform.localScale.x, _enemy.transform.localScale.y, _enemy.transform.localScale.z);
            _enemy.GetEnemyData().facingDirection = 1;
        }
    }
}
