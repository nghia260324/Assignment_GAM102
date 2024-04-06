using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    public float moveSpeed;
    public float jumpForce;
    public float coinForce;
    public float timeDestroyCoin;

    public int damageCoin;

    public LayerMask ground;
    public GameObject coinPrefabs;
    public Transform firePos;


    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_BoxCollider;
    private Animator m_Animator;
    private ScoreManager m_ScoreManager;
    private DistanceTimeTracker m_DistanceTimeTracker;

    private float currentSpeed;

    private bool isJumping;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_ScoreManager = GetComponent<ScoreManager>();
        m_DistanceTimeTracker = GetComponent<DistanceTimeTracker>();
        currentSpeed = moveSpeed;
        isJumping = false;


    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
    }
    private void Update()
    {
        if (transform.position.y < -5)
        {
            Die();
        }
        float move = Input.GetAxisRaw("Horizontal");
        m_Animator.SetBool("isRunning", true);

        if (IsGround())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                m_Animator.SetTrigger("jump");
                m_Rigidbody.velocity = Vector2.up * jumpForce;
            }
            if (isJumping)
            {
                m_Animator.SetBool("isCutJumping", true);
            } else
            {
                m_Animator.SetBool("isCutJumping", false);
            }
            m_Animator.SetBool("isFalling", false);
        } else
        {
            m_Animator.SetBool("isFalling", true);
            if (!isJumping)
            {
                isJumping = true;
                m_Animator.SetTrigger("fall");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Attack();
        }

        //m_Rigidbody.velocity = new Vector2(move * currentSpeed, m_Rigidbody.velocity.y);
    }
    private void Attack()
    {
        if (m_ScoreManager.currentCoin <= 0) return;
        CreateCoin();
        m_ScoreManager.DecreaseCoins(1);
    }

    private void CreateCoin()
    {
        GameObject newCoint = Instantiate(coinPrefabs, firePos.position, Quaternion.identity);
        newCoint.GetComponent<Rigidbody2D>().velocity = Vector2.right * coinForce;
        newCoint.transform.rotation = Quaternion.Euler(0, 0, 90);
        newCoint.GetComponent<Coin>().damage = damageCoin;
        Destroy(newCoint, timeDestroyCoin);
    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.bounds.size, 0f, Vector2.down, 0.1f, ground);
    }
    public void Footstep()
    {
        AudioManager.instance.PlaySFXFootsteps();
    }
    public void Landing()
    {
        isJumping = false;
        AudioManager.instance.PlaySFXLanding();
        AudioManager.instance.PlaySFXFootsteps();
    }
    public void TakeDamage(int damage)
    {
        Die();
    }

    private void Die()
    {
        GameManager.instance.SaveData(m_DistanceTimeTracker.distanceTraveled, m_DistanceTimeTracker.gameTime, m_ScoreManager.hightScore);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            m_ScoreManager.UpdateCoinAndScore(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
