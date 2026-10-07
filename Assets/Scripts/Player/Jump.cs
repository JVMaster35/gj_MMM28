using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    private Controller _controller;
    private PlayerData _data;
    private Rigidbody2D _body;
    private PlayerAnimations _anim;
    private Ground _ground;
    private GenderSwitch _genderSwitch;

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

    void Update()
    {
        GetFormData();
        _data._desiredJump |= _controller.input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _data._velocity = _body.linearVelocity;

        if (_onGround)
        {
            _data._jumpPhase = 0;
        }

        if (_data._desiredJump)
        {
            _data._desiredJump = false;
            JumpAction();
            _anim.PlayJumpAnim();
        }

        if (_body.linearVelocity.y > 0)
        {
            _body.gravityScale = _data._upwardMovementMultiplier;
        }
        else if (_body.linearVelocity.y < 0)
        {
            _body.gravityScale = _data._downwardMovementMultiplier;
        }
        else if (_body.linearVelocity.y == 0)
        {
            _body.gravityScale = _data._defaultGravityScale;
        }

        _body.linearVelocity = _data._velocity;
    }
    private void JumpAction()
    {
        if (_onGround || _data._jumpPhase < _data._maxAirJumps)
        {
            _data._jumpPhase += 1;

            _data._jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _data.jumpPower);

            if (_data._velocity.y > 0f)
            {
                _data._jumpSpeed = Mathf.Max(_data._jumpSpeed - _data._velocity.y, 0f);
            }
            else if (_data._velocity.y < 0f)
            {
                _data._jumpSpeed += Mathf.Abs(_body.linearVelocity.y);
            }
            _data._velocity.y += _data._jumpSpeed;
        }
    }

    private void GetFormData()
    {
        _data = _genderSwitch.GetActiveForm().GetComponent<PlayerFormData>()._data;
    }
}
