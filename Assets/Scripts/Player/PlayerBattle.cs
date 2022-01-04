using UnityEngine;
using System.Collections;
using System;
public class PlayerBattle : Player
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _stamina;
    [SerializeField] private float _reloadStamina;

    public Action<int> OnHealthChanged;
    public Action<int> OnEnergyEmptyChanged;
    public Action<int> OnEnergyFillChanged;

    private int _currentStamina;
    public int DamagePoint => _damage;

    protected override void Awake()
    {
        base.Awake();

        _playerInput.Player.Attack.performed += ctx => Attack();
        _currentStamina = _stamina;
    }

    private void Attack()
    {
        if(_currentStamina > 0)
            StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        _currentStamina--;
        OnEnergyEmptyChanged?.Invoke(_currentStamina);
        StartCoroutine(FillingStamina());

        _animPlayer.SetBool("isAttacking", true);      
        yield return new WaitForSeconds(.3f);
        _animPlayer.SetBool("isAttacking", false);       
    }
    
    private IEnumerator FillingStamina()
    {
        if (_currentStamina < _stamina)
        {
            yield return new WaitForSeconds(_reloadStamina);
            _currentStamina += 1;
            OnEnergyFillChanged?.Invoke(_currentStamina);
        }
    }
}
