using System;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
	private int _currentDay;

	[SerializeField]
	private IntEvent _dayHasAdvanced;

	[SerializeField]
	private TextMeshProUGUI _textMeshProUGUI;

	public void AdvanceDay() {
		_currentDay++;
		_dayHasAdvanced.Invoke(_currentDay);
		_textMeshProUGUI.SetText("Day " + _currentDay.ToString());
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.T)) {
			AdvanceDay();
		}
	}
}
