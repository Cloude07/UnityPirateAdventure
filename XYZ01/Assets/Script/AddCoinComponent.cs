using System.Collections;
using PixelCrew.Creatures;
using UnityEngine;

public class AddCoinComponent : MonoBehaviour
{
    [SerializeField] int _numCoins;
    Hero _hero;

    private void Start()
    {
        //������ �� ����� � ����
        _hero = FindObjectOfType<Hero>();
    }

    public void Add()
    {
        _hero.AddCoin(_numCoins);
    }
}
