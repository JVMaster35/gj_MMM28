using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IKnockBack
{
    [SerializeField] private Enemy _enemy;
    [SerializeField]
    private HealthShieldUI _healthShield;
    private EnemyData _data;

    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _currentShieldStrength;
    [SerializeField]
    private Coroutine shieldRegen;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _data = _enemy.GetEnemyData();
        _healthShield = GetComponent<HealthShieldUI>();
    }

    private void Start()
    {
        _currentHealth = _data.maxHealth;
        _currentShieldStrength = _data.maxShieldStr;

        if (_data.isShielded)
        {
            _data.isBroken = false;
        }
    }

    public void Damage(float damageAmount, GenderType damageType)
    {
        if (_data.isShielded)
        {
            if (damageType.Equals(_data.shieldType) || damageType.Equals(GenderType.TRANS))
            {
                _currentShieldStrength -= damageAmount;
                _healthShield.UpdateShield(_currentShieldStrength, _data.maxShieldStr);
            }
            else
            {
                _currentShieldStrength -= damageAmount/_data.shieldReducution;
                _healthShield.UpdateShield(_currentShieldStrength, _data.maxShieldStr);
            }
        }
        else
        {
            DamageEnemy(damageAmount);
        }

        if (_currentShieldStrength <= 0)
        {
            _data.isBroken = true;

            _enemy.SwitchState(_enemy.GetStunState());

            if (shieldRegen != null)
            {
                StopCoroutine(shieldRegen);
            }

            shieldRegen = StartCoroutine(ShieldRegenation());
        }

        if(_data.isBroken)
        {
            DamageEnemy(damageAmount);
        }
    }

    public void DamageEnemy(float damageAmount)
    {
        _currentHealth -= damageAmount;
        _healthShield.UpdateHealthBar(_currentHealth, _data.maxHealth);
        if(_currentHealth <= 0)
        {
            _enemy.SwitchState(_enemy.GetDeathState());
        }
        else
        {
            _enemy.SwitchState(_enemy.GetDamageState());
        }
    }

    public void Knockback(float knockBackForce, Vector2 knockBackAngle, int faceDirection)
    {
        if (!_data.isHeavy)
        {
            _enemy.GetDamageState().kBForce = knockBackForce;
            _enemy.GetDamageState().kBAngle = knockBackAngle;
            _enemy.GetDamageState().kBDir = faceDirection;
            _enemy.SwitchState(_enemy.GetDamageState());
        }
    }

    private IEnumerator ShieldRegenation()
    {
        yield return new WaitForSeconds(_data.stunTime);

        _enemy.SwitchState(_enemy.GetPatrolState());

        while (_currentShieldStrength < _data.maxShieldStr)
        {
            _currentShieldStrength += _data.shieldRegenRate / _data.maxShieldStr;
            _healthShield.UpdateShield(_currentShieldStrength, _data.maxShieldStr);
            yield return _data.shieldRegenTime;
        }

        shieldRegen = null;
        _data.isBroken = false;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetCurrentShieldStrength()
    {
        return _currentShieldStrength;
    }

    public EnemyData GetEnemyData()
    {
        return _data;
    }
}
