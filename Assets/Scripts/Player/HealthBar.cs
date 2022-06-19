using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;
    [SerializeField] private int _maxHearts;

    private void Start()
    {
        PlayerBattle.OnHealthChanged += ChangeHealth;
        InitHearts();
    }

    private void InitHearts()
    {
        for (int i = 0; i < _maxHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = heartFull;
        }
    }

    private void ChangeHealth(int health)
    {
        for (int i = 0; i < _maxHearts; i++)
        {
            if (health > 1)
                hearts[i].sprite = heartFull;
            else if (health == 1)
                hearts[i].sprite = heartHalf;
            else
                hearts[i].sprite = heartEmpty;

            health -= 2;
        }
    }
}
