using UnityEngine;

public class PlayerMovement : Player
{
    [SerializeField] private float _speedMove;

    private Vector3 _direction;

    private void Update() => CheckInput();
    private void LateUpdate() => Move();

    private void CheckInput()
    {
        _direction = Vector3.zero;
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        _rbPlayer.MovePosition(transform.position + _direction * _speedMove * Time.deltaTime);

        if (_direction != Vector3.zero)
        {
            _animPlayer.SetFloat("moveX", _direction.x);
            _animPlayer.SetFloat("moveY", _direction.y);
            _animPlayer.SetBool("isMoving", true);
        }
        else
            _animPlayer.SetBool("isMoving", false);
    }
}
