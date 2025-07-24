using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ConstantsValue.ENEMY_TAG)
        {
            Destroy(other.gameObject);
        }
    }
}
