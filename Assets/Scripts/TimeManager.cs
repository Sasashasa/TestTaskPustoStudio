using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : MonoBehaviour
{
	public static TimeManager Instance { get; private set; }

	public event EventHandler OnCurrentTimeChanged;
	
	private const string _apiUrl = "https://yandex.com/time/sync.json";
	private const float _updateClockTime = 1f;
	
	private DateTime _currentTime;
	private float _updateClockTimer;

	private void Awake()
	{
		Instance = this;
		
		_updateClockTimer = _updateClockTime;
		
		StartCoroutine(GetTimeFromServer());
	}

	private void Update()
	{
		if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
		{
			StartCoroutine(GetTimeFromServer());
		}

		HandleCurrentTimeUpdate();
	}

	public DateTime GetCurrentTime()
	{
		return _currentTime;
	}

	private IEnumerator GetTimeFromServer()
	{
		UnityWebRequest request = UnityWebRequest.Get(_apiUrl);

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			string jsonResult = request.downloadHandler.text;
			ServerTimeData serverTimeData = JsonUtility.FromJson<ServerTimeData>(jsonResult);

			_currentTime = DateTimeOffset.FromUnixTimeSeconds(serverTimeData.unixtime).UtcDateTime.ToLocalTime();
			OnCurrentTimeChanged?.Invoke(this, EventArgs.Empty);
			
			Debug.Log("Server time: " + _currentTime);
		}
		else
		{
			_currentTime = DateTime.Now;
			Debug.LogError("Error in getting time: " + request.error);
		}
	}

	private void HandleCurrentTimeUpdate()
	{
		_updateClockTimer -= Time.deltaTime;

		if (_updateClockTimer <= 0)
		{
			_updateClockTimer = _updateClockTime;
			
			_currentTime = _currentTime.AddSeconds(1);
			OnCurrentTimeChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}