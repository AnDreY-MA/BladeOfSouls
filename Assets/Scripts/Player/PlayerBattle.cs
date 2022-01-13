using UnityEngine;
using System.Collections;
using System;
public class PlayerBattle : Player
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float maxComboDelay = 1f;
    [SerializeField] private int _stamina;
    [SerializeField] private float _reloadStamina;
    public int DamagePoint => _damage;

    public Action<int> OnHealthChanged;
    public Action<int> OnEnergyEmptyChanged;
    public Action<int> OnEnergyFillChanged;
    public static Action OnAttack;
    public static int NoOfClicks = 0;

    private float _nextAttackTime = 0f;
    private float _lastCkickedTime = 0f;
    
    private float _time;

    private int _currentStamina;

    private AnimatorStateInfo _animatorState;
    
    protected override void Awake()
    {
        base.Awake();

        _playerInput.Player.Attack.performed += ctx => ApplyAttack();
        _currentStamina = _stamina;
        _animatorState = _animPlayer.GetCurrentAnimatorStateInfo(0);
    }

    private void Update()
    {
        _time = Time.time;
        CheckTimeToAttack();
    }

    private void CheckTimeToAttack()
    {
        if (_animPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _animPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack-1"))
            _animPlayer.SetBool("Attack-1", false);
        if (_animPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _animPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack-2"))
        {
            _animPlayer.SetBool("Attack-2", false);
            NoOfClicks = 0;
        }
        if (Time.time - _lastCkickedTime > maxComboDelay)
        {
            NoOfClicks = 0;
            _lastCkickedTime = 0;
        }
    }

    private void ApplyAttack()
    {
        print("Click attack");
        if (Time.time > _nextAttackTime)
            ComboAttack();
    }

    private void ComboAttack()
    {
        _lastCkickedTime = Time.time;
        NoOfClicks++;

        if (NoOfClicks == 1)
            _animPlayer.SetBool("Attack-1", true);

        NoOfClicks = Mathf.Clamp(NoOfClicks, 0, 3);

         if(NoOfClicks >= 2 && _animPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _animPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack-1"))
        {
            _animPlayer.SetBool("Attack-1", false);
            _animPlayer.SetBool("Attack-2", true);
        }
    }

    private void Attack()
    {
        if (_currentStamina > 0)
        {
            StartCoroutine(AttackCorouritine());
            OnAttack?.Invoke();
        }       
    }

    private IEnumerator AttackCorouritine()
    {
        ChangeStamina();

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

    private void ChangeStamina()
    {
        _currentStamina--;
        OnEnergyEmptyChanged?.Invoke(_currentStamina);
        StartCoroutine(FillingStamina());
    }
}
