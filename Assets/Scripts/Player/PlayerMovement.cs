using UnityEngine;
using System.Collections;

public class PlayerMovement : Player
{
    [SerializeField] private float _speed;
    [SerializeField] private string _layer;

    private float _currentSpeed;

    private Vector3 _direction;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentSpeed = _speed;
        PlayerBattle.OnAttack += ChangeMove;
        _playerInput.Player.Jump.performed += ctx => Jump();
    }
        

    private void Update() => CheckInput();
    private void LateUpdate() => Move();

    private void Jump()
    {
        gameObject.layer = LayerMask.NameToLayer(_layer);
        _spriteRenderer.sortingLayerName = _layer;
    }

    private void CheckInput()
    {
        _direction = Vector3.zero;
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        _rbPlayer.MovePosition(transform.position + _direction * _currentSpeed * Time.deltaTime);

        if (_direction != Vector3.zero)
        {
            _animPlayer.SetFloat("moveX", _direction.x);
            _animPlayer.SetFloat("moveY", _direction.y);
            _animPlayer.SetBool("isMoving", true);
        }
        else
            _animPlayer.SetBool("isMoving", false);
    }

    private void ChangeMove() => StartCoroutine(ChangeSpeed());

    private IEnumerator ChangeSpeed()
    {
        _currentSpeed = 0;
        yield return new WaitForSeconds(0.4f);
        _currentSpeed = _speed;
    }
}
