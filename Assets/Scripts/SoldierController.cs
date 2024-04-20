using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public Health health;
    public Soldier soldier;

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

    private float currentTimeAttack;
    private int currentHealth;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    GameObject[] enemies;

    private EnemyController enemyTarget;
    public void SetTarget(EnemyController targetEnemy)
    {
        this.enemyTarget = targetEnemy;
    }
    private void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        isFacingRight = false;
        currentTimeAttack = timeAttack;
        currentHealth = maxHealth;
        health.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        FindAndAttackEnemy();

        if (enemyTarget == null)
        {
            SetNewTarget();
        }

        currentTimeAttack += Time.deltaTime;
    }

    void FindAndAttackEnemy()
    {
        if (enemyTarget != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, enemyTarget.transform.position);
            Vector2 dir = enemyTarget.transform.position - transform.position;

            if (distanceToTarget > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, m_Speed * Time.deltaTime);
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
                AttackEnemy();
                currentTimeAttack = 0;
            }
        }
        else
        {
            m_Animator.SetBool("isMovingS", false);
            isMoving = false;
        }
    }

    public void AttackEnemy()
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
        if (enemies.Length > 0 && enemyTarget == null)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            enemyTarget = enemies[randomIndex].gameObject.GetComponent<EnemyController>();
        } else
        {
            enemyTarget = null;
        }
    }

    public void Attack()
    {
        isAttack = false;
        if (enemyTarget == null) return;
        enemyTarget.TakeDamage(damage);
        enemyTarget.SetTarget(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (enemyTarget == null) return;
        m_Animator.SetTrigger("hit");
        currentHealth -= damage;
        health.UpdateHealth(currentHealth, maxHealth);
        Vector2 attackDirection = (transform.position - enemyTarget.transform.position).normalized;
        m_Rigidbody.AddForce(Vector2.up * m_AttackForce, ForceMode2D.Impulse);
        m_Rigidbody.AddForce(attackDirection * m_AttackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
