using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    static int count;

    private void Start()
    {
        count = 0;
    }

    public static void AddPentagram()
    {
        count++;
    }

    public static int PentagramCount() => count;

    void Update()
    {
        text.text = string.Format("{0}/5", count);
    }
}
