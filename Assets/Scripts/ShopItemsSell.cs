using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShopItemsSell : IEnumerator,IEnumerable
{
    int position = -1;
    public List<ItemRarity> itemsRarity;
    public ShopItemsSell()=>itemsRarity = new List<ItemRarity>();

    public object Current => throw new System.NotImplementedException();

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)itemsRarity).GetEnumerator();
    }

    public bool MoveNext()
    {
        position++;
        return (position < itemsRarity.Count);
    }

    public void Reset() => position = 0;
        
}

[System.Serializable]
public class ItemRarity
{
    public Rarity rarity;
    public Sprite icon;
    public GameObject prefab;
    public float price;
    public Sprite bordersShop;

    public ItemRarity(Rarity _rarity, Sprite _icon, Sprite _bordersShop, GameObject _prefab, float _price)
    {
        rarity = _rarity;
        icon = _icon;
        bordersShop = _bordersShop;
        prefab = _prefab;
        price = _price;
    }

}

public enum Rarity
{
    Blue,
    Green,
    Purple,
    Gold
}