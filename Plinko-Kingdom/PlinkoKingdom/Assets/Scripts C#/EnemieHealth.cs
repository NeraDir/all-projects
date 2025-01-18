using UnityEngine.UI;
using UnityEngine;

public class EnemieHealth : MonoBehaviour
{
    public float m_Health;

    public int m_GivingValue;

    public float m_MaxHealth;

    public int m_AddValue;

    [SerializeField]
    private Image m_HealthFillingImage;

    public void Init(float inputHealth) 
    {
        m_MaxHealth = inputHealth;
        m_Health = inputHealth;
    }

    public void TakeDamage(float inputdamage) 
    {
        m_Health -= inputdamage;

        if (m_Health <= 0)
            Die();
    }

    private void OnDestroy()
    {
        GameManager.currentDestroyedCount += m_AddValue;
        GameManager.currentWinValue += Random.Range(2, 10);
    }

    private void LateUpdate() => UpdateHealingBar();

    private void Die() => Destroy(gameObject);

    private void UpdateHealingBar() => m_HealthFillingImage.fillAmount = Mathf.Lerp(m_HealthFillingImage.fillAmount, m_Health/m_MaxHealth, 8 * Time.deltaTime);
}
