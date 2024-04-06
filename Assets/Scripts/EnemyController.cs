using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Setting")]
    public float moveSpeed;
    public float distaceTarget;
    public float jumpForce;
    public float attackInterval;

    public int damage;
    public int dropCoin;

    [Header("Health")]
    public int maxHealth;

    public Transform body;
    public Transform posA;
    public Transform posB;

    public GameObject dropManyCoin;
    public LayerMask ground;
    public BoxCollider2D m_BoxCollider;
    public CapsuleCollider2D m_CapsuleCollider;

    [Header("Plant")]
    public bool isPlant;
    public GameObject bulletPrefabs;
    public Transform firePos;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private Health m_Health;

    private Transform player;
    private Transform currentPosTarget;

    private bool isFacingRight;
    private bool isTarget;
    private bool isDefTarget;

    private float currentSpeed;
    private float currentAttackInterval;

    private int currentHealth;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Health = GetComponent<Health>();

        currentSpeed = moveSpeed;
        if (!isPlant)
        {
            currentPosTarget = posB;
        }

        isFacingRight = false;
        isTarget = false;
        isDefTarget = false;

        currentHealth = maxHealth;
        currentAttackInterval = attackInterval;

        player = GameObject.Find("Player").gameObject.transform;
    }

    private void Update()
    {
        currentAttackInterval += Time.deltaTime;
        float distancePlayerTarget;
        if (player == null)
        {
            if (isDefTarget)
            {
                currentPosTarget = posB;
                isDefTarget = false;
            }
            player = GameObject.Find("Player")?.transform;
            return;
        } else
        {
            distancePlayerTarget = Vector2.Distance(transform.position, player.position);
            if (isPlant)
            {
                if (distancePlayerTarget < distaceTarget)
                {
                    Attack();
                }
                //Attack();
                return;
            }
            if (distancePlayerTarget < distaceTarget)
            {
                currentPosTarget = player.transform;
                isTarget = true;
                isDefTarget = true;
            }
            else
            {
                isTarget = false;
                if (isDefTarget)
                {
                    currentPosTarget = posA;
                    isDefTarget = false;
                }
            }
        }

        Vector2 directionPosTarget = transform.position - currentPosTarget.position;
        m_Animator.SetBool("isRunning", true);

        if (IsGround())
        {
            m_BoxCollider.enabled = true;
            if (IsGroundInFront())
            {
                m_Rigidbody.velocity = Vector2.up * jumpForce;
                m_BoxCollider.enabled = false;
            }
        }

        if (Vector2.Distance(transform.position,currentPosTarget.position) <= 0.2f)
        {
            currentPosTarget = currentPosTarget == posA ? posB : posA;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentPosTarget.position.x, transform.position.y), moveSpeed * Time.deltaTime);

        if ((directionPosTarget.x > 0 && isFacingRight) || (directionPosTarget.x < 0 && !isFacingRight))
        {
            Flip();
        }
    }
    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefabs, firePos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * jumpForce, ForceMode2D.Impulse);
        newBullet.GetComponent<Bullet>().damage = damage;
        Destroy(newBullet, 3);
    }
    private void Attack()
    {
        if (isPlant)
        {
            if(currentAttackInterval > attackInterval)
            {
                m_Animator.SetTrigger("attack");
                currentAttackInterval = 0;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth-=damage;
        m_Health?.UpdateHealth(currentHealth, maxHealth);
        if (currentHealth <=0)
        {
            bool random = Random.Range(0,2) == 0 ? true : false;
            if (random)
            {
                GameObject manyCoin = Instantiate(dropManyCoin, transform.position * 1.5f,Quaternion.identity);
                Destroy(manyCoin, 5);
                player.GetComponent<ScoreManager>().UpdateCoinAndScore(dropCoin);
            }
            Destroy(gameObject);
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = body.localScale;
        localScale.x *= -1;
        body.transform.localScale = localScale;
    }
    private bool IsGround()
    {
        return Physics2D.CapsuleCast(m_CapsuleCollider.bounds.center, m_CapsuleCollider.bounds.size, m_CapsuleCollider.direction, 0f, Vector2.down, 0.1f, ground);
    }

    private bool IsGroundInFront()
    {
        return Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.bounds.size, 0f, Vector2.right, 0.1f, ground);
    }
}
