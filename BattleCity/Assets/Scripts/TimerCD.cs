using UnityEngine;

[System.Serializable]
public class TimerCD : MonoBehaviour
{
    #region Properties

    [SerializeField] private string _forWhat;
    [SerializeField] private float _cooldown;
    private float _timeUntil;
    public bool Ready => _timeUntil <= 0;
    public float Cooldown
    {
        get { return _cooldown; }
        set { if (value > 0) _cooldown = value; else _cooldown = 0; }
    }
    public float TimeUntil
    {
        get { return _timeUntil; }
        set { if (value > 0) _timeUntil = value; else _timeUntil = 0; }
    }
    public string ForWhat
    {
        get { return _forWhat; }
        set { _forWhat = value; }
    }

    #endregion

    public TimerCD(string forWhat, float cooldown)
    {
        ForWhat = forWhat;
        Cooldown = cooldown;
    }

    public void ActivateCD()
    {
        _timeUntil = _cooldown;
    }

    private void FixedUpdate()
    {
        TimeUntil -= Time.deltaTime;
    }
}