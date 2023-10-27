using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HealthBar : MonoBehaviour
{
    public GameObject Target;

    private Vector3 _cachedPos;
    [SerializeField] Image _healthFill, _damageFill;
    [SerializeField] private float _damageFillDecay = 0.5f;

    [SerializeField] private Vector3 _posOffset = new Vector3(0, 1, 0);


    private void Update()
    {
        if (Target != null)
        {
            _cachedPos = Target.transform.position;
        }

        //transform.position = Camera.main.WorldToScreenPoint(_cachedPos + _posOffset);
        transform.position = _cachedPos + _posOffset;
        //transform.position = Camera.main.WorldToViewportPoint(_cachedPos);


        if (_damageFill.fillAmount != _healthFill.fillAmount)
        {
            float dif = _damageFill.fillAmount - _healthFill.fillAmount;
            _damageFill.fillAmount -= dif * Time.deltaTime * _damageFillDecay;
        }
    }

    public void SetFill(float health, float maxHealth)
    {
        _healthFill.fillAmount = health / maxHealth;
    }
}