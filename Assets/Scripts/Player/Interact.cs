using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Interact : MonoBehaviour
{
    private IInteractable Interractable { get; set; }

    private PlayerData _data;
    private Controller _controller;
    private GenderSwitch _genderSwitch;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
        _genderSwitch = GetComponent<GenderSwitch>();
        GetFormData();
    }

    void Update()
    {
        _data.interaction |= _controller.input.RetrieveInteractInput();
        GetFormData();

        if (_data.interaction)
        {
            Interractable?.Interact(this);
        }
    }

    private void GetFormData()
    {
        _data = _genderSwitch.GetActiveForm().GetComponent<PlayerFormData>()._data;
    }
}
