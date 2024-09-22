using System;
using UnityEngine;
using DG.Tweening;

public class Clock : MonoBehaviour
{
	[SerializeField] private Transform _hourHand;
	[SerializeField] private Transform _minuteHand;
	[SerializeField] private Transform _secondHand;

	private void Start()
	{
		TimeManager.Instance.OnCurrentTimeChanged += TimeManager_OnCurrentTimeChanged;
	}

	private void TimeManager_OnCurrentTimeChanged(object sender, EventArgs e)
	{
		UpdateClock();
	}

	private void UpdateClock()
	{
		DateTime time = TimeManager.Instance.GetCurrentTime();
		
		float hourRotation = time.Hour % 12 * 30f + time.Minute * 0.5f;
		float minuteRotation = time.Minute * 6f;
		float secondRotation = time.Second * 6f;
		
		_hourHand.DORotate(new Vector3(0, 0, -hourRotation), 1f);
		_minuteHand.DORotate(new Vector3(0, 0, -minuteRotation), 1f);
		_secondHand.DORotate(new Vector3(0, 0, -secondRotation), 1);

		
		// Реализация без использования DOTween
		/*
		_hourHand.localRotation = Quaternion.Euler(0, 0, -hourRotation);
		_minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteRotation);
		_secondHand.localRotation = Quaternion.Euler(0, 0, -secondRotation);
		*/
	}
}