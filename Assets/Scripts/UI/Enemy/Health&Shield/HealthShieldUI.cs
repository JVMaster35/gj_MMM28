using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthShieldUI : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;

    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private CanvasGroup _healthBarUI;

    [SerializeField] private GameObject _shieldIcon;
    [SerializeField]
    private Image _shieldImage;
    [SerializeField] private Sprite[] _shieldImgs;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;

    void Start()
    {
        if (_enemyHealth.GetEnemyData().isShielded)
        {
            _shieldIcon.SetActive(true);
            SetShieldImage(_enemyHealth.GetEnemyData().shieldType);
        }
        else
        {
            _shieldIcon.SetActive(false);
        }

        _healthBarSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + _enemyHealth.GetEnemyData().offsetUI;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthBarSlider.gameObject.SetActive(true);
        _healthBarSlider.value = currentHealth / maxHealth;
        new WaitForSeconds(2);
        StartCoroutine(FadeHealthBar(true));
    }

    public void UpdateShield(float currentShieldStr, float maxShieldStr)
    {
        _shieldImage.fillAmount = currentShieldStr / maxShieldStr;
    }

    private Image SetShieldImage(GenderType shieldType)
    {
        switch (shieldType)
        {
            case GenderType.MALE:
                _shieldImage.sprite = _shieldImgs[0];
                break;
            case GenderType.FEMALE:
                _shieldImage.sprite = _shieldImgs[1];
                break;
        }
        return _shieldImage;
    }

    IEnumerator FadeHealthBar(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                _healthBarUI.alpha = i;
                yield return null;
            }

            _healthBarSlider.gameObject.SetActive(false);
        }
    }
}
