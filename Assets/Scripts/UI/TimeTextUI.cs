using System;
using TMPro;
using UnityEngine;

public class TimeTextUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _timeText;
	
	private void Start()
	{
		TimeManager.Instance.OnCurrentTimeChanged += TimeManager_OnCurrentTimeChanged;
	}

	private void TimeManager_OnCurrentTimeChanged(object sender, EventArgs e)
	{
		_timeText.text = TimeManager.Instance.GetCurrentTime().ToString("HH:mm:ss");
	}
}