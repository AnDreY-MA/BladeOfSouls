using UnityEngine;
using System.Collections;
public class PlayerAttack : Player
{
    [SerializeField] private int _damage;
    [SerializeField] private int _enegry;

    public int DamagePoint => _damage;

    protected override void Awake()
    {
        base.Awake();
        _playerInput.Player.Attack.performed += ctx => Attack();
    }

    private void Attack()
    {
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        _animPlayer.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.3f);
        _animPlayer.SetBool("isAttacking", false);
    }
}
