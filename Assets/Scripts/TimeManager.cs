using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager sharedInstance = null;
    private string _url = "http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php"; //change this to your own
    private string _timeData;
    private string _currentTime;
    private string _currentDate;


    //make sure there is only one instance of this always.
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
        // DontDestroyOnLoad(gameObject);
    }


    //time fether coroutine
    public IEnumerator getTime()
    {
        Debug.Log("connecting to php");
        WWW www = new WWW(_url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Error");
        }
        else
        {
            Debug.Log("got the php information");
        }
        _timeData = www.text;
        string[] words = _timeData.Split('/');
        //timerTestLabel.text = www.text;
        Debug.Log("The date is : " + words[0]);
        Debug.Log("The time is : " + words[1]);

        //setting current time
        _currentDate = words[0];
        _currentTime = words[1];
    }


    //get the current time at startup
    void Start()
    {
        Debug.Log("TimeManager script is Ready.");
        StartCoroutine("getTime");
    }

    //get the current date - also converting from string to int.
    //where 12-4-2017 is 1242017
    public int getCurrentDateNow()
    {
        string[] words = _currentDate.Split('-');
        int x = int.Parse(words[0] + words[1] + words[2]);
        return x;
    }


    //get the current Time
    public string getCurrentTimeNow()
    {
        return _currentTime;
    }

    public int GetMonth()
    {
        return (getCurrentDateNow() / 1000000);
    }

    public int GetDay()
    {
        return (getCurrentDateNow() / 10000) % 100;
    }

    public int GetYear()
    {
        return (getCurrentDateNow() % 10000);
    }

    public int GetHour()
    {
        return int.Parse(getCurrentTimeNow().Split(':')[0]);
    }

    public int GetMinute()
    {
        return int.Parse(getCurrentTimeNow().Split(':')[1]);
    }

    public int GetSecond()
    {
        return int.Parse(getCurrentTimeNow().Split(':')[2]);
    }

    public string ShowDate()
    {
        return GetYear() + "년 " + GetMonth() + "월 " + GetDay() + "일";
    }

    public int[] GetCurrentTime()
    {
        string[] time = _currentTime.Split(':');
        string[] date = _currentDate.Split('-'); // 월-일-년

        int[] answer = new int[3];

        for (int i = 0; i < 3; i++)
        {
            answer[i] = int.Parse(time[i]);
        }
        
        for (int i = 0; i < 3; i++)
        {
            answer[i + 3] = int.Parse(date[i]);
        }

        return answer;
    }

    public int[] GetKoreaCurrentTime()
    {
        string[] time = _currentTime.Split(':');
        string[] date = _currentDate.Split('-'); // 월-일-년

        // Debug.Log("진짜시간 : " + time[0]);

        int[] timeValue = new int[3];
        int[] dateValue = new int[3];

        for (int i = 0; i < 3; i++)
        {
            timeValue[i] = int.Parse(time[i]);
            dateValue[i] = int.Parse(date[i]);
        }

        timeValue[0] += 14;

        int h = timeValue[0];

        if (h > 24)
        {
            h -= 24;
            timeValue[0] = h;
            dateValue[1]++;
        }

        int[] answer = new int[6];

        answer[0] = dateValue[2];
        answer[1] = dateValue[0];
        answer[2] = dateValue[1];
        answer[3] = timeValue[0];
        answer[4] = timeValue[1];
        answer[5] = timeValue[2];

        // 달 변경도 추후에 넣어줘야함!

        return answer;
    }

    public int CheckAdCooldown()
    {
        PlayerData playerData = GameManager.instance.GetPlayerData();

        if ((playerData.adLastDate != GetDay()) || playerData.adLastTime != GetHour())
        {
            playerData.adLastDate = GetDay();
            playerData.adLastTime = GetHour();
            playerData.adCount = 50; // 광고 횟수 제한
        }

        return playerData.adCount;
    }

    // 기기 내의 시간 활용

/*    public bool isExpiredRegenTime(int[] lootedTime, int regenTime)
    {
        long year = GetYear();
        long month = GetMonth();
        long day = GetDay();
        long hour = GetHour();

        int lastMonthDayCount = GetDayCount(lootedTime[1]);

        int[] expiredTime = GetAfterTime(lootedTime, regenTime);
        long coolRegenTimeValue = GetAfterTimeValue(expiredTime);
        long NowTimeValue = hour + (day * 24) + (month * lastMonthDayCount * 24) + (year * 365 * 24);

        if (NowTimeValue > coolRegenTimeValue)
        {
            return true;
        }

        return false;
    }

    public int[] GetAfterTime(int[] lootedTime, int regenTime) // 년, 월, 일, 시까지 반환 int[4]
    {
        int year = lootedTime[0];
        int month = lootedTime[1];
        int day = lootedTime[2];
        int hour = lootedTime[3] + regenTime;
        int thisMonthDayCount = GetDayCount(lootedTime[1]);

        while (hour >= 24)
        {
            hour -= 24;
            day++;
        }

        if (GetDay() > thisMonthDayCount)
        {
            Debug.Log(GetDay() + ", " + thisMonthDayCount);
            day = 1;
            month++;
        }

        if (month > 12)
        {
            month = 1;
            year++;
        }

        int[] answer = new int[4];

        answer[0] = year;
        answer[1] = month;
        answer[2] = day;
        answer[3] = hour;

        return answer;
    }


    public long GetAfterTimeValue(int[] answer)
    {
        return answer[3] + (answer[2] * 24) + (answer[1] * GetDayCount(answer[1]) * 24) + (answer[0] * 365 * 24);
    }

    public int GetDayCount(int month)
    {
        if (month != GetMonth())
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    month = 31;
                    break;
                case 2:
                    if (GetYear() % 4 == 0)
                    {
                        month = 29;
                    }
                    else
                    {
                        month = 28;
                    }
                    break;
                default:
                    month = 30;
                    break;
            }
        }

        Debug.Log("리턴값 : " + month);

        return month;
    }

    public int[] GetIntervalTime(int[] lootedTime, int[] expiredTime)
    {
        int[] result = new int[6];

        for (int i = 0; i < lootedTime.Length; i++)
        {
            result[i] = expiredTime[i] - lootedTime[i];
        }

        return result;
    }*/
}


// 서버 시간 가져오기
/*public static TimeManager sharedInstance;
public string url = "http://naver.com";

private void Awake()
{
    sharedInstance = this;
}

IEnumerator WebChk()
{
    UnityWebRequest request = new UnityWebRequest();
    using (request = UnityWebRequest.Get(url))
    {
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string date = request.GetResponseHeader("date");

            DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

            int stopwatch =
                (int)timeSpan.TotalSeconds - PlayerPrefs.GetInt("net", (int)timeSpan.TotalSeconds);

            Debug.Log(stopwatch + "sec");
            PlayerPrefs.SetInt("net", (int)timeSpan.TotalSeconds);
        }
    }
}*/