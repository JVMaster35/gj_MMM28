using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    private Controller _controller;
    private PlayerData _data;
    private Rigidbody2D _body;
    private PlayerAnimations _anim;
    private Ground _ground;
    private GenderSwitch _genderSwitch;

    private float _maxSpeedChange, _acceleration;
    private bool _onGround;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
        _genderSwitch = GetComponent<GenderSwitch>();
        _anim = GetComponent<PlayerAnimations>();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        GetFormData();
    }

    private void Update()
    {
        GetFormData();
        _data._direction.x = _controller.input.RetrieveMovementInput();
        _data._desiredVelocity = new Vector2(_data._direction.x, 0f) * Mathf.Max(_data.moveSpeed - _ground.Friction, 0f);

        Flip();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _data._velocity = _body.linearVelocity;

        _anim.PlayMoveAnim();

        _acceleration = _onGround ? _data._maxAcceleration : _data._maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _data._velocity.x = Mathf.MoveTowards(_data._velocity.x, _data._desiredVelocity.x, _maxSpeedChange);

        _body.linearVelocity = _data._velocity;
    }

    private void Flip()
    {
        if (_data._direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_data._direction.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void GetFormData()
    {
        _data = _genderSwitch.GetActiveForm().GetComponent<PlayerFormData>()._data;
    }
}
