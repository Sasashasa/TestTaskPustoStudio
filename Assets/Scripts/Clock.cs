using System;
using UnityEngine;

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
		DateTime dateTime = TimeManager.Instance.CurrentTime;

        float hourRotation = dateTime.Hour % 12 * 30f + dateTime.Minute * 0.5f;
        float minuteRotation = dateTime.Minute * 6f;
        float secondRotation = dateTime.Second * 6f;

        _hourHand.localRotation = Quaternion.Euler(0, 0, -hourRotation);
        _minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteRotation);
        _secondHand.localRotation = Quaternion.Euler(0, 0, -secondRotation);
    }
}