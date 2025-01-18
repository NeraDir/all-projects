using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]public float m_Health;

    public float m_MaxHealth;

    public Transform m_target;

    [SerializeField]
    private TMP_Text m_HealthFillingImage;

    [SerializeField]
    private GameObject m_Loosepane;

    private float neeedHealth;

    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private PlayerShooting m_Shooting;
    [SerializeField]
    private PlayerRotating m_Rotating;

    public void Start()
    {
        m_Health = m_MaxHealth;
        neeedHealth = m_Health;
        Loosepanel.gameOver = false;
    }

    public void TakeDamage(float inputdamage)
    {
        neeedHealth -= inputdamage;
        if (neeedHealth <= 0)
            Die();
    }

    private void LateUpdate() => UpdateHealingBar();

    private void Die() => Destroy(gameObject);

    private void OnDestroy()
    {
        m_Animator.SetInteger("PlayerAnimationState", 2);
        m_Rotating.enabled = false;
        m_Shooting.enabled = false;
        m_Loosepane.SetActive(true);
        Loosepanel.gameOver = true;
        Debug.Log(Loosepanel.gameOver);
    }

    private void UpdateHealingBar() 
    {
        m_Health = Mathf.MoveTowards(m_Health, neeedHealth, 20 * Time.deltaTime);
        m_HealthFillingImage.text = (m_Health).ToString("0");

        if (Input.GetKeyDown(KeyCode.W))
        {
            TakeDamage(11);
        }
    }
}
