using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    static int count;
    private void Start()
    {
        count = 0;
    }

    public static void AddCandle()
    {
        count++;
    }

    public static int CandleCount() => count;

    void Update()
    {
        text.text = string.Format("{0}/3", count);
    }
}
