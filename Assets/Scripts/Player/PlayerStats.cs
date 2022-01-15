using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerBattle _player;
    [SerializeField] private SpriteRenderer[] _spritesEnergy;
    [SerializeField] private Sprite _full;
    [SerializeField] private Sprite _empty;

    private void OnEnable()
    {
        _player.OnEnergyEmptyChanged += EmptyEnergy;
        _player.OnEnergyFillChanged += FillEnergy;
    }

    private void OnDisable() => _player.OnEnergyEmptyChanged -= EmptyEnergy;

    private void EmptyEnergy(int energy) => _spritesEnergy[energy].sprite = _empty;
    private void FillEnergy(int energy)
    {
        if(energy != 0)
            _spritesEnergy[energy - 1].sprite = _full;
    }

}
