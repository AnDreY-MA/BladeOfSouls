using UnityEngine;
using System.Collections;
using System;
public class PlayerAttack : Player
{
    [SerializeField] private int _damage;
    [SerializeField] private int _enegry;
    [SerializeField] private float _reloadEnergy;

    public Action<int> OnEnergyEmptyChanged;
    public Action<int> OnEnergyFillChanged;

    private int _currentEnergy;
    public int DamagePoint => _damage;

    private Action OnFillEnergy;

    protected override void Awake()
    {
        base.Awake();

        _playerInput.Player.Attack.performed += ctx => Attack();
        _currentEnergy = _enegry;
        OnFillEnergy += Fill;
    }

    private void Attack()
    {
        if(_currentEnergy > 0)
            StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        _currentEnergy--;
        OnEnergyEmptyChanged?.Invoke(_currentEnergy);
        OnFillEnergy?.Invoke();

        _animPlayer.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.3f);
        _animPlayer.SetBool("isAttacking", false);       
    }

    private void Fill() => StartCoroutine(FillingEnergy());
    
    private IEnumerator FillingEnergy()
    {
        if (_currentEnergy < _enegry)
        {
            yield return new WaitForSeconds(_reloadEnergy);
            _currentEnergy += 1;
            OnEnergyFillChanged?.Invoke(_currentEnergy);
        }
    }
}
