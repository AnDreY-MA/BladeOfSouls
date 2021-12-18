using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMove;

    private Animator _animPlayer;

    private PlayerInput _playerInput;

    private Vector3 _direction;

    private void Awake()
    {
        _animPlayer = GetComponent<Animator>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void Update()
    {
        _direction = Vector3.zero;
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
        Move();
    }

    private void Move()
    {
        if (_direction != Vector3.zero)
        {
            transform.Translate(_direction * _speedMove * Time.deltaTime);
            _animPlayer.SetFloat("moveX", _direction.x);
            _animPlayer.SetFloat("moveY", _direction.y);
            _animPlayer.SetBool("isMoving", true);
        }
        else
            _animPlayer.SetBool("isMoving", false);
    }
}
