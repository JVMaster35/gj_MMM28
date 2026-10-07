using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IKnockBack
{
    private PlayerData _data;
    private GenderSwitch _genderSwitch;
    private Rigidbody2D _rb;
    private PlayerAnimations _anim;
    private float _currenHealth;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _genderSwitch = GetComponent<GenderSwitch>();
        _anim = GetComponent<PlayerAnimations>();
        GetFormData();

        _currenHealth = _data.maxHealth;
    }

    private void Update()
    {
        GetFormData();

        if (_currenHealth <= 0)
        {
            if (_genderSwitch.GetCanChange()) 
            {
                HealPlayer(_data.nearDeathHealthRegain);
                _genderSwitch.SwitchGender();
            }
            else
            {
                PlayerDeath();
            }
        }
    }

    public void Damage(float damageAmount, GenderType damageType)
    {
        float totalDamage;
        if (_data.gender.Equals(damageType))
        {
            totalDamage = damageAmount;
        }
        else
        {
            totalDamage = damageAmount - _data.defencePower;
        }
        _anim.PlayHurtAnim();
        _currenHealth -= totalDamage;
    }

    public void Knockback(float knockBackForce, Vector2 knockBackAngle, int faceDirection)
    {
        if (_data.gender.Equals(GenderType.FEMALE))
        {
            knockBackForce = knockBackForce * 2;
        }

        _rb.linearVelocity =
                    new Vector2(knockBackAngle.x * faceDirection, knockBackAngle.y)
                    * knockBackForce;
    }

    void PlayerDeath()
    {
        Destroy(gameObject);
    }

    void HealPlayer(float healthRestore)
    {
        _currenHealth += healthRestore;
    }

    public float GetPlayerHealth()
    {
        return _currenHealth;
    }

    private void GetFormData()
    {
        _data = _genderSwitch.GetActiveForm().GetComponent<PlayerFormData>()._data;
    }
}
