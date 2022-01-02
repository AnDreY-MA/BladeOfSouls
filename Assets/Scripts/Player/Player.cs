using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected PlayerInput _playerInput;

    protected Animator _animPlayer;
    protected Rigidbody _rbPlayer;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animPlayer = GetComponent<Animator>();
        _rbPlayer = GetComponent<Rigidbody>();
    }
}
