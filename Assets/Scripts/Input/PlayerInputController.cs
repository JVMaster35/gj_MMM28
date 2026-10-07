using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerController", menuName = "Controllers/PlayerController")]
public class PlayerInputController : InputController
{
    private PlayerInputs _inputs;

    private void OnEnable()
    {
        if (_inputs == null)
        {
            _inputs = new PlayerInputs();
        }
        EnablePlayerInput();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    public override bool RetrieveJumpInput()
    {
        return _inputs.Player.Jump.triggered;
    }

    public override float RetrieveMovementInput()
    {
        return _inputs.Player.Move.ReadValue<Vector2>().x;
    }

    public override bool RetrieveGenderSwitchInput()
    {
        return _inputs.Player.GenderSwitch.triggered;
    }

    public void EnablePlayerInput()
    {
        _inputs.Player.Enable();
        _inputs.UI.Disable();
    }

    public override void DisableGenderSwitch()
    {
        _inputs.Player.GenderSwitch.Disable();
    }

    public override void EnableGenderSwitch()
    {
        _inputs.Player.GenderSwitch.Enable();
    }

    public override bool RetrieveAttackInput()
    {
        return _inputs.Player.Attack.triggered;
    }

    public override bool RetrieveInteractInput()
    {
        return _inputs.Player.Interact.triggered;
    }
}
