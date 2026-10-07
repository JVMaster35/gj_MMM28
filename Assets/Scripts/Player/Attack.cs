using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Attack : MonoBehaviour
{
    private Controller _controller;
    private PlayerData _data;
    private Animator _animator;
    [SerializeField]
    private Transform _meleePoint;

    private void Awake()
    {
        _controller = GetComponentInParent<Controller>();
        _animator = GetComponent<Animator>();
        _data = GetComponent<PlayerFormData>()._data;
    }

    
    void Update()
    {
        _data = GetComponent<PlayerFormData>()._data;

        if (Time.time >= _data.nextAttackTime)
        {
            if (_controller.input.RetrieveAttackInput())
            {
                PlayerMelee();
                _data.nextAttackTime = Time.time + 1f / _data.attackRate;
            }
        }
    }

    void PlayerMelee()
    {
        //Hit & Damage Objects/Enemies
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_meleePoint.position, _data.attackRange, _data.DamageableLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Debug.Log("Hit: " + hitCollider.name);
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            IKnockBack knockable = hitCollider.GetComponent<IKnockBack>();

            if (damageable != null)
            {
                damageable.Damage(_data.attackDamage, _data.gender);
            }

            if(knockable != null)
            {
                float kbForce = _data.attackDamage / 10;
                knockable.Knockback(kbForce, new Vector2(2, 1), (int)_data._direction.x);
            }
        }
    }
}
