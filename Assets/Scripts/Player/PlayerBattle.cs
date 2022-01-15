using UnityEngine;
using System.Collections;
using System;
public class PlayerBattle : Player
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _stamina;
    [SerializeField] private float _reloadStamina;
    public int DamagePoint => _damage;

    public Action<int> OnHealthChanged;
    public Action<int> OnEnergyEmptyChanged;
    public Action<int> OnEnergyFillChanged;
    public static Action OnAttack;

    private int _currentStamina;

    private bool _isAttack = false;
    
    protected override void Awake()
    {
        base.Awake();

        _playerInput.Player.Attack.performed += ctx => Attack();
        _currentStamina = _stamina;
    }

    private void FixedUpdate() => _animator.SetFloat("StateTime", Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));

    private void Attack()
    {
        if (_currentStamina > 0)
        {
            _animator.SetTrigger("MeleeAttack");
            ChangeStamina();
            OnAttack?.Invoke();
        }
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

    private void ChangeStamina()
    {
        if(_currentStamina > 0)
            _currentStamina--;
        OnEnergyEmptyChanged?.Invoke(_currentStamina);
        StartCoroutine(FillingStamina());
    }
}
