using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected PlayerInput _playerInput;

    protected Animator _animator;
    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
