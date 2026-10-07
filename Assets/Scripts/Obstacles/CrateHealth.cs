using UnityEngine;

public class CrateHealth : MonoBehaviour, IDamageable
{
    public float crateHealth;

    public void Damage(float damageAmount, GenderType damageType)
    {
        crateHealth -= damageAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if(crateHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
