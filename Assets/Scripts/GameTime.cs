using System;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
	private int _currentDay;

	private float _currentTimeInSeconds;

	[SerializeField]
	private float _dayStartsAtSeconds;

	[SerializeField]
	private float _dayEndsAtSeconds;

	[SerializeField]
	private float _secondsPerSecond;
	//TODO Make diffrent speed for e.g. indoors or in the mine

	[SerializeField]
	private IntEvent _dayHasAdvanced;

	[SerializeField]
	private TextMeshProUGUI _textGUIDay;

	[SerializeField]
	private TextMeshProUGUI _textGUITime;

	private void Start()
	{
		_currentTimeInSeconds = _dayStartsAtSeconds;
	}

	private void advanceDay() {
		_currentDay++;
		_dayHasAdvanced.Invoke(_currentDay);
		_textGUIDay.SetText("Day " + _currentDay.ToString());
		_currentTimeInSeconds = _dayStartsAtSeconds;
	}

	public void Update()
	{
		_currentTimeInSeconds += Time.deltaTime * _secondsPerSecond;

		TimeSpan t = TimeSpan.FromSeconds(_currentTimeInSeconds);

		_textGUITime.SetText("Time: " + string.Format("{0:D2}:{1:D2}:{2:D2}",t.Hours,t.Minutes,t.Seconds));

		if (_currentTimeInSeconds >= _dayEndsAtSeconds) {
			advanceDay();
		}

		//TODO Replace with sleeping
		if (Input.GetKeyDown(KeyCode.T)) {
			advanceDay();
		}
	}
}
