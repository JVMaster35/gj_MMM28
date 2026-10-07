using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerData _data;
    private GenderSwitch _genderSwitch;
    private float _playerHealth;
    private float _changeRate;
    [SerializeField] private Image _healthCircle;
    [SerializeField] private Image _genderChangeCircle;

    void Start()
    {
        _genderSwitch = GetComponentInParent<GenderSwitch>();
        GetPlayerHealthData();
        GetPlayerChangeData();
        GetFormData();
        ChangeHealthColor();
    }

    
    void Update()
    {
        GetPlayerHealthData();
        GetPlayerChangeData();
        GetFormData();
        ChangeHealthColor();
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        _healthCircle.fillAmount = _playerHealth / _data.maxHealth;
        _genderChangeCircle.fillAmount = _changeRate / _data.changeRecharge;
    }

    void ChangeHealthColor()
    {
        if (_data.gender.Equals(GenderType.MALE))
        {
            _healthCircle.color = Color.blue;
        }
        else
        {
            _healthCircle.color = Color.pink;
        }
    }

    private void GetPlayerHealthData()
    {
        _playerHealth = GetComponentInParent<Health>().GetPlayerHealth();
    }

    private void GetPlayerChangeData()
    {
        _changeRate = _genderSwitch.GetChangeTime();
    }

    private void GetFormData()
    {
        _data = _genderSwitch.GetActiveForm().GetComponent<PlayerFormData>()._data;
    }
}
