using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Parchment : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    static int count;

    private void Start()
    {
        count = 0;
    }

    public static void AddParchment()
    {
        count++;
    }

    public static int ParchmentCount() => count;

    void Update()
    {
        text.text = string.Format("{0}/7", count);
    }
}
