using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
public class CropData : ScriptableObject
{
	//TODO Make a list of lists for more variations
	[SerializeField]
	private List<SpriteList> _sprites;
	[SerializeField]
	private int[] _growthStageDuration;
	[SerializeField]
	private bool _isRegrowing;
	[SerializeField]
	private int _regrowSpriteIndex;

	public List<SpriteList> GetSprites() { return _sprites; }

	public SpriteList GetSpritesAt(int pIndex) { return _sprites[pIndex]; }

	public Sprite GetSpriteAt(int pSpriteIndex, int pVersionIndex) { 
		return _sprites[pVersionIndex].Sprites[pSpriteIndex];
	}

	public int GetCropVersionAmount() { return _sprites.Count; }

	public int[] GetGrowthStageDurations() { return _growthStageDuration; }

	public bool GetIsRegrowing() { return _isRegrowing; }

	public int GetRegrowSpriteIndex() { return _regrowSpriteIndex; }

	public int GetRegrowDay() {
		int DaySum = 0;
		
		for (int i = 0; i < _regrowSpriteIndex; ++i)
		{
			DaySum += _growthStageDuration[i];
		}

		return DaySum;
	}

	public bool GetIsHarvestable(int pCurrentGrowthIndex, int pVersionIndex) {
		return pCurrentGrowthIndex + 1 == _sprites[pVersionIndex].Sprites.Count;
	}

	public int GetSpriteIndexForGrowthDay(int pGrowthDay)
	{
		int CurrentDay = 0;
		int CurrentIndex = 0;

		foreach (int i in _growthStageDuration) {
			CurrentDay += i;

			if (CurrentDay >= pGrowthDay) {
				break;
			}

			CurrentIndex++;
		}

		return CurrentIndex;
	}

	//TODO Add What Loot is dropped and how much
}

[Serializable]
public struct SpriteList { 
	public List<Sprite> Sprites;
}