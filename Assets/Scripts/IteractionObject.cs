using UnityEngine;

public class IteractionObject : MonoBehaviour
{
    private Animator _anim;

    private void Start() => _anim = GetComponent<Animator>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerWeapon>(out PlayerWeapon playerWeapon))
        {
            _anim.SetTrigger("Destroy");
            Destroy(gameObject, 0.5f);
        }
    }
}
