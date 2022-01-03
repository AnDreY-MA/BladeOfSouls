using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speedEnemy;
    [SerializeField] private float _knockForce;

    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;

    private Animator animEnemy;

    private Player _targetPlayer;

    private void Start()
    {
        _targetPlayer = FindObjectOfType<Player>();
        animEnemy = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckDistance();
        CheckAttack();
    }

    private void CheckDistance()
    {
        if (_targetPlayer != null)
        {
            if (Vector2.Distance(_targetPlayer.transform.position, transform.position) <= chaseRadius && Vector2.Distance(_targetPlayer.transform.position, transform.position) > attackRadius)
            {
                animEnemy.SetBool("isMoving", true);
                Movement();
            }
            else
                animEnemy.SetBool("isMoving", false);
        }
    }

    private void CheckAttack()
    {
        if (Vector3.Distance(_targetPlayer.transform.position, transform.position) <= chaseRadius && Vector3.Distance(_targetPlayer.transform.position, transform.position) <= attackRadius)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        animEnemy.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        animEnemy.SetBool("isAttack", false);
    }

    private void Movement()
    {
        animEnemy.SetFloat("moveX", _targetPlayer.transform.position.x - transform.position.x);
        animEnemy.SetFloat("moveY", _targetPlayer.transform.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, _targetPlayer.transform.position, _speedEnemy * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerWeapon>(out PlayerWeapon weaponPlayer))
            Damage(collision, weaponPlayer.Damage);
    }

    private void Damage(Collider2D player, int damage)
    {
        Vector2 difference = transform.position - player.transform.position;
        transform.position = new Vector2(transform.position.x + difference.x * _knockForce, transform.position.y + difference.y * _knockForce);
        _health -= damage;
        print(damage);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }
}
