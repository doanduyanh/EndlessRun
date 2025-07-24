using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Pickups
{
    public int powerUpAmount;
    public override void OnPickup()
    {
        if (playerScript != null) playerScript.PowerUp(powerUpAmount);
    }
}
