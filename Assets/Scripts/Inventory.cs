using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	[SerializeField]
	private int _inventorySlotAmount;

	[SerializeField]
	private int _maxInventoryRows;

	[SerializeField]
	private RectTransform _hotbarContainer;

	[SerializeField]
	private RectTransform _inventoryContainer;

	[SerializeField]
	private Sprite _hotbarSlotSprite;

	[SerializeField]
	private Sprite _selectedHotbarSlotSprite;

	[SerializeField]
	private Sprite _inventorySlotSprite;

	[SerializeField]
	private Sprite _lockedInventorySlotSprite;

	[SerializeField]
	private GameObject _hotBarSlotPrefab;

	[SerializeField]
	private GameObject _invetorySlotPrefab;


	private int _currentInventoryRows = 1;

	private int _currentSelectedHotbarSlot = 0;

	private List<GameObject> _inventorySlotObjects = new List<GameObject>();

	private void Start()
	{
		_hotbarContainer.GetComponent<GridLayoutGroup>().constraintCount = _inventorySlotAmount;
		_inventoryContainer.GetComponent<GridLayoutGroup>().constraintCount = _inventorySlotAmount;

		for (int i = 0; i < _inventorySlotAmount; ++i) {
			GameObject go = GameObject.Instantiate(_hotBarSlotPrefab, _hotbarContainer);
			go.name = "UI_Hotbar_Sot_" + i;
			go.GetComponent<Image>().sprite = _hotbarSlotSprite;
			
			RectTransform rt = go.GetComponent<RectTransform>();

			_inventorySlotObjects.Add(go);
		}

		for (int i = 0; i < _inventorySlotAmount * _maxInventoryRows; ++i) {
			GameObject go = GameObject.Instantiate(_invetorySlotPrefab, _inventoryContainer);
			go.name = "UI_Inventory_Sot_" + i;

			if (i < _inventorySlotAmount * _currentInventoryRows) {
				go.GetComponent<Image>().sprite = _inventorySlotSprite;
			}
			else {
				go.GetComponent<Image>().sprite = _lockedInventorySlotSprite;
			}

			RectTransform rt = go.GetComponent<RectTransform>();

			_inventorySlotObjects.Add(go);
		}

		_inventorySlotObjects[_currentSelectedHotbarSlot].GetComponent<Image>().sprite = _selectedHotbarSlotSprite;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab)) {
			_inventoryContainer.gameObject.SetActive(!_inventoryContainer.gameObject.activeInHierarchy);
		}

	
		if (Input.mouseScrollDelta.y != 0.0f)
		{
			_inventorySlotObjects[_currentSelectedHotbarSlot].GetComponent<Image>().sprite = _hotbarSlotSprite;

			_currentSelectedHotbarSlot -= Mathf.FloorToInt(Input.mouseScrollDelta.y);

			while (_currentSelectedHotbarSlot > _inventorySlotAmount - 1)
			{
				_currentSelectedHotbarSlot = _currentSelectedHotbarSlot - _inventorySlotAmount;
			}

			while (_currentSelectedHotbarSlot < 0)
			{
				_currentSelectedHotbarSlot = _inventorySlotAmount + _currentSelectedHotbarSlot;
			}

			_inventorySlotObjects[_currentSelectedHotbarSlot].GetComponent<Image>().sprite = _selectedHotbarSlotSprite;
		}
	}
}
