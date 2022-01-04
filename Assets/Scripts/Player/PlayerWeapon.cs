using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private int _damage;
    public int Damage => _damage;

    private PlayerBattle _playerAttack;

    private void Start()
    {
        _playerAttack = GetComponentInParent<PlayerBattle>();
        _damage = _playerAttack.DamagePoint;
    }
}
