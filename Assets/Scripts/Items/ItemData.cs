using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
	[SerializeField]
	private int _id = -1;

	[SerializeField]
	private Sprite _sprite;

	[SerializeField]
	private string _name;

	[NaughtyAttributes.OnValueChanged(nameof(resetSellPrice))]
	[SerializeField]
	private bool _isSellable;

	[NaughtyAttributes.EnableIf(nameof(_isSellable))]
	[SerializeField]
	private int _sellPrice;

	[SerializeField]
	private int _buyPrice;

	[SerializeField]
	private List<ItemTags> _itemTags;

	private void resetSellPrice()
	{
		if(!_isSellable)
			_sellPrice = 0;
	}

	public enum ItemTags { 

	}
}
