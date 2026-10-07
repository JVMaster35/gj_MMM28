using UnityEngine;

[CreateAssetMenu(fileName = "InputController", menuName = "Scriptable Objects/InputController")]
public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMovementInput();
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveGenderSwitchInput();

    public abstract bool RetrieveAttackInput();

    public abstract bool RetrieveInteractInput();

    public abstract void DisableGenderSwitch();

    public abstract void EnableGenderSwitch();
}
