using UnityEngine;

public class EnemyGround : MonoBehaviour
{
    public bool OnGround { get; private set; }
    public float Friction { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
        Friction = 0.0f;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 _normal = collision.GetContact(i).normal;
            OnGround |= _normal.y >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D _material = collision.rigidbody.sharedMaterial;

        Friction = 0;

        if (_material != null)
        {
            Friction = _material.friction;
        }
    }
}
