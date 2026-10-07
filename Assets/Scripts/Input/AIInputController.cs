using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "Controllers/AIController")]
public class AIInputController : InputController
{
    public override void DisableGenderSwitch()
    {
        throw new System.NotImplementedException();
    }

    public override void EnableGenderSwitch()
    {
        throw new System.NotImplementedException();
    }

    public override bool RetrieveAttackInput()
    {
        return true;
    }

    public override bool RetrieveGenderSwitchInput()
    {
        throw new System.NotImplementedException();
    }

    public override bool RetrieveInteractInput()
    {
        throw new System.NotImplementedException();
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMovementInput()
    {
        return 1f;
    }
}
