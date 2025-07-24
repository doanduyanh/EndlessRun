using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Pickups
{
    public int healAmount;
    public override void OnPickup()
    {
        if (playerScript != null) playerScript.Heal(healAmount);
    }
}
