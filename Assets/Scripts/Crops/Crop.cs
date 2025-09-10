using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Crop : MonoBehaviour
{
	[SerializeField]
	CropData _cropData;

	[SerializeField]
	IntEvent _dayHasAdvancedEvent;

	private SpriteRenderer _spriteRenderer;

	private int _dayCount;
	private int _cropVersionIndex;
	private int _currentGrowthIndex;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_dayHasAdvancedEvent.Register(dayHasAdvanced);
	}

	private void Start()
	{
		spawn();
	}

	private void dayHasAdvanced(int pCurrentDay)
	{
		_dayCount++;

		int NewGrowthIndex = _cropData.GetSpriteIndexForGrowthDay(_dayCount);

		if (_currentGrowthIndex != NewGrowthIndex)
		{
			grow(NewGrowthIndex);
		}
	}

	private void spawn()
	{
		_cropVersionIndex = Random.Range(0, _cropData.GetCropVersionAmount());
		_dayCount = 0;
		_currentGrowthIndex = 0;

		_spriteRenderer.sprite = _cropData.GetSpriteAt(0, _cropVersionIndex);
	}

	private void grow(int pNewGrowthIndex)
	{
		_currentGrowthIndex = pNewGrowthIndex;

		_spriteRenderer.sprite = _cropData.GetSpriteAt(_currentGrowthIndex, _cropVersionIndex);
	}

	private void harvest() {
		if (_cropData.GetIsHarvestable(_currentGrowthIndex, _cropVersionIndex))
		{
			//TODO Make it drop loot


			if (_cropData.GetIsRegrowing())
			{
				_dayCount = _cropData.GetRegrowDay();
				grow(_cropData.GetRegrowSpriteIndex());
			}
			else
			{
				Destroy(this.gameObject); 
			}
		}
	}

	private void OnMouseDown()
	{
		harvest();
	}
}
