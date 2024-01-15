using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedSwitcher : MonoBehaviour
{
    private void Update()
    {
        WorldSwitcherActivator.WorldSwitcherOff();
    }
}
