using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
public class CropData : ScriptableObject
{
	[SerializeField]
	Sprite[] sprites;
	[SerializeField]
	Sprite[] altSprites;
	[SerializeField]
	int[] spriteDuration;
	[SerializeField]
	bool regrowing;
	[SerializeField]
	int regrowStage;
}
