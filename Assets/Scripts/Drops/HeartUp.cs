using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUp : Pickups
{
    public int heartUpAmount;
    public override void OnPickup()
    {
        if (playerScript != null) playerScript.HeartUp(heartUpAmount);
    }
}
