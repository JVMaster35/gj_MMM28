using UnityEngine;

public class EnemyBaseState
{
    protected Enemy _enemy;
    protected string _animName;

    public EnemyBaseState(Enemy enemy, string animName)
    {
        _enemy = enemy;
        _animName = animName;
    }

    public virtual void Enter()
    {
        _enemy.GetEnemyAnim().SetBool(_animName, true);
        Debug.Log("Entered: " + _animName);
    }

    public virtual void Exit()
    {
        _enemy.GetEnemyAnim().SetBool(_animName, false);
        Debug.Log("Exiting: " + _animName);
    }

    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }

    public virtual void AnimationFinishTrigger() { }

    public virtual void AnimationAttackTrigger() { }
}
