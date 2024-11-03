using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : MonoBehaviour
{
	public static TimeManager Instance { get; private set; }

	public event EventHandler OnCurrentTimeChanged;

	public DateTime CurrentTime { get; private set; }

	private const string _apiUrl = "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";
	private const float _updateInterval = 1f;

	private float _timer;
	private bool _isGotTimeFromServer;


    private void Awake()
	{
		Instance = this;

        StartCoroutine(GetTimeFromServer());
	}

    private void Update()
    {
		if (!_isGotTimeFromServer)
            return;

        HandleTimeUpdate();

        HandleHourSynchronization();
    }


    private IEnumerator GetTimeFromServer()
	{
        _isGotTimeFromServer = false;

        UnityWebRequest request = UnityWebRequest.Get(_apiUrl);

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			TimeData timeData = JsonUtility.FromJson<TimeData>(request.downloadHandler.text);

            CurrentTime = new DateTime(timeData.Year, timeData.Month, timeData.Day, timeData.Hour, timeData.Minute, timeData.Seconds);

            OnCurrentTimeChanged?.Invoke(this, EventArgs.Empty);

            _timer = _updateInterval;
            _isGotTimeFromServer = true;
        }
	}

	private void HandleTimeUpdate()
	{
        if (_timer <= 0)
        {
            _timer = _updateInterval;

            CurrentTime = CurrentTime.AddSeconds(1);

            OnCurrentTimeChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

	private void HandleHourSynchronization()
	{
        if (CurrentTime.Second == 0 && CurrentTime.Minute == 0)
        {
            StartCoroutine(GetTimeFromServer());
        }
    }
}