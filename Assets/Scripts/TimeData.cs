using UnityEngine;

[System.Serializable]
public struct TimeData
{
    public int Year => year;
    public int Month => month;
    public int Day => day;
    public int Hour => hour;
    public int Minute => minute;
    public int Seconds => seconds;

    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private int seconds;
}