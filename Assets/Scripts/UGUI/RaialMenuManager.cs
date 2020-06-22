using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RaialMenuManager : MonoBehaviour
{
    public VRTK_RadialMenu RadialMenu;
    public void SpawnModem()
    {
        EventCenter.Broadcast(EventDefine.SpawnModem);
    }

    public void SpawnDrill()
    {
        EventCenter.Broadcast(EventDefine.SpawnDrill);
    }

    public void SpawnCable()
    {
        EventCenter.Broadcast(EventDefine.SpawnCable);
    }
}
