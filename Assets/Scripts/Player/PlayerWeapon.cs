using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private int _damage;
    public int Damage => _damage;

    private PlayerAttack _playerAttack;

    private void Start()
    {
        _playerAttack = GetComponentInParent<PlayerAttack>();
        _damage = _playerAttack.DamagePoint;
    }
}
