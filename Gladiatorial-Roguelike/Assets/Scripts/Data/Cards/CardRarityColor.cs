using System.Collections.Generic;
using Logic.Types;
using UnityEngine;

[CreateAssetMenu(fileName = "CardRarityColor", menuName = "Card/Color Configuration")]
public class CardRarityColor : ScriptableObject
{
    [SerializeField] private List<RarityColor> _rarityColors;

    public Dictionary<CardRarity, Color> RarityColorDictionary { get; private set; }

    private void OnEnable()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        RarityColorDictionary = new Dictionary<CardRarity, Color>();

        foreach (var rarityColor in _rarityColors)
        {
            if (!RarityColorDictionary.ContainsKey(rarityColor.Rarity))
            {
                RarityColorDictionary.Add(rarityColor.Rarity, rarityColor.Color);
            }
        }
    }
}