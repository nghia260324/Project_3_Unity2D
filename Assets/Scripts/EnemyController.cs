using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Health health;

    [Header("Setting")]
    public float m_Speed;
    public float m_AttackForce;
    public float timeAttack;
    public int maxHealth;
    public int damage;
    public float attackRange = 0.5f;

    private bool isMoving;
    private bool isFacingRight;
    private bool isAttack;

    private float currentSpeed;
    private float currentTimeAttack;
    private int currentHealth;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    GameObject[] soldiers;

    public GameObject playerTarget;

    public void SetTarget(GameObject player)
    {
        this.playerTarget = player;
    }
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        isFacingRight = false;
        currentTimeAttack = timeAttack;
        currentHealth = maxHealth;
        currentSpeed = m_Speed;
        health.UpdateHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (isAttack)
        {
            currentSpeed = m_Speed / 3;
        } else
        {
            currentSpeed = m_Speed;
        }
        soldiers = GameObject.FindGameObjectsWithTag("Player");

        FindAndAttackSoldier();

        if (playerTarget == null)
        {
            SetNewTarget();
        }

        currentTimeAttack += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (playerTarget == null) return;
        m_Animator.SetTrigger("hit");
        currentHealth -= damage;
        health.UpdateHealth(currentHealth, maxHealth);
        Vector2 attackDirection = (transform.position - playerTarget.transform.position).normalized;
        m_Rigidbody.AddForce(Vector2.up * m_AttackForce, ForceMode2D.Impulse);
        m_Rigidbody.AddForce(attackDirection * m_AttackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        LevelManager.Instance.EnemyDefeated(50);
        Destroy(gameObject);
    }
    void FindAndAttackSoldier()
    {
        if (playerTarget != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, playerTarget.transform.position);
            Vector2 dir = playerTarget.transform.position - transform.position;

            if (distanceToTarget > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, currentSpeed * Time.deltaTime);
                m_Animator.SetBool("isMovingS", true);
                isMoving = true;
            }
            else
            {
                m_Animator.SetBool("isMovingS", false);
                isMoving = false;
            }

            if (dir.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (dir.x < 0 && isFacingRight)
            {
                Flip();
            }

            if (distanceToTarget <= attackRange && currentTimeAttack > timeAttack && !isAttack)
            {
                AttackSoldier();
                currentTimeAttack = 0;
            }
        }
        else
        {
            m_Animator.SetBool("isMovingS", false);
            isMoving = false;
        }
    }

    public void AttackSoldier()
    {
        m_Animator.SetTrigger("attackS");
        isAttack = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void SetNewTarget()
    {
        soldiers = GameObject.FindGameObjectsWithTag("Player");
        if (soldiers.Length > 0 && playerTarget == null)
        {
            int randomIndex = Random.Range(0, soldiers.Length);
            SetTarget(soldiers[randomIndex]);
        }
        else
        {
            SetTarget(null);
        }
    }

    public void Attack()
    {
        isAttack = false;
        if (playerTarget == null) return;
        if (playerTarget.name == "Player")
        {
            playerTarget.GetComponent<PlayerController>().TakeDamage(damage);
        }
        else
        {
            playerTarget.GetComponent<SoldierController>().TakeDamage(damage);
            playerTarget.GetComponent<SoldierController>().SetTarget(GetComponent<EnemyController>());
        }
    }
}
