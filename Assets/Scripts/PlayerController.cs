using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Health health;
    public LayerMask m_LayerGrounded;
    public Transform groundPos;

    [Header("Setting")]
    public float m_Speed;
    public float m_JumpForce;
    public float checkRadius;
    public float timeAttack;
    public int maxHealth;
    public int damage;


    Rigidbody2D m_Rigidbody;
    Animator m_Animator;

    private bool isPhysical;
    private bool isGrounded;
    private bool isJumping;
    private bool isFacingRight;
    private bool isAttack;
    private float currentTimeAttack;
    private int currentHealth;
    private EnemyController targetEnemy;


    public void SetTarget(EnemyController targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void Start()
    {
        m_Speed = FirebaseDataAccess.Instance.character.speed;
        damage = FirebaseDataAccess.Instance.character.damage;
        maxHealth = FirebaseDataAccess.Instance.character.health;

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        isFacingRight = false;
        isAttack = false;
        currentTimeAttack = timeAttack;
        currentHealth = maxHealth;
        health.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, m_LayerGrounded);
        float move = Input.GetAxisRaw("Horizontal");

        if (PhysicalManager.Instance.GetPhysicalBarFillAmount() >= PhysicalManager.Instance.maxValue)
        {
            isPhysical = true;
        }
        if (PhysicalManager.Instance.GetPhysicalBarFillAmount() <= 0.01f) {
            isPhysical = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && isPhysical)
        {
            m_Rigidbody.velocity = new Vector2(move * (m_Speed + m_Speed/3), m_Rigidbody.velocity.y);
        } else
        {
            m_Rigidbody.velocity = new Vector2(move * m_Speed, m_Rigidbody.velocity.y);
        }

         

        if (move != 0)
        {
            m_Animator.SetBool("isMoving", true);
        }
        else
        {
            m_Animator.SetBool("isMoving", false);
        }

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_Animator.SetTrigger("takeOff");
            isJumping = true;
            m_Rigidbody.velocity = Vector2.up * m_JumpForce;
        }
        if (isGrounded)
        {
            m_Animator.SetBool("isJumping", false);
        } else
        {
            m_Animator.SetBool("isJumping", true);
        }


        currentTimeAttack += Time.deltaTime;
        if (move > 0 && !isFacingRight) Flip();
        else if (move < 0 && isFacingRight) Flip();

        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!isAttack && Input.GetMouseButton(0) && !isJumping && currentTimeAttack > timeAttack)
        {
            m_Animator.SetTrigger("attack");
            isAttack = true;
            currentTimeAttack = 0;
        }
    }

    public void Attack()
    {
        isAttack = false;
        if (targetEnemy == null) return;
        float distance = Vector2.Distance(transform.position,targetEnemy.transform.position);
        if (distance < 2f)
        {
            targetEnemy.TakeDamage(damage);
            targetEnemy.SetTarget(gameObject);
        }

    }

    public void Jumping()
    {
        if (isJumping)
        {
            isJumping = false;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.UpdateHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {

        }
    }
}
