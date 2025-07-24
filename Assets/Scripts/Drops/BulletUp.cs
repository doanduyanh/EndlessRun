using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUp : Pickups
{
    public override void OnPickup()
    {

        if (playerScript != null) playerScript.BulletUp();
    }
}
