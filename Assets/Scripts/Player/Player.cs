using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected PlayerInput _playerInput;

    protected Animator _animPlayer;
    protected Rigidbody2D _rbPlayer;

    protected virtual void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animPlayer = GetComponent<Animator>();
        _rbPlayer = GetComponent<Rigidbody2D>();
    }
}
