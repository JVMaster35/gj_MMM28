using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Controller _controller;
    private Animator _animator;
    private Ground _ground;
    private GenderSwitch _genderSwitch;


    private void Awake()
    {
        _controller = GetComponent<Controller>();
        _genderSwitch = GetComponent<GenderSwitch>();
        _animator = _genderSwitch.GetActiveAnimator();
        _ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator = _genderSwitch.GetActiveAnimator();
    }

    public void PlayMoveAnim()
    {
        if (_ground.OnGround)
        {
            _animator.SetFloat("Speed", Mathf.Abs(_controller.input.RetrieveMovementInput()));
        }
    }

    public void PlayJumpAnim()
    {
        _animator.SetTrigger("Jump");
    }

    public void PlayGroundFallAnims()
    {
        _animator.SetBool("IsGrounded", _ground.OnGround);
    }

    public void PlayHurtAnim()
    {
        _animator.SetTrigger("Hurt");
    }
}
