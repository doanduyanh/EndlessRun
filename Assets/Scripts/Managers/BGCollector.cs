using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        if(other.tag == ConstantsValue.BG_TAG || other.tag == ConstantsValue.GROUND_TAG)
        {
            other.gameObject.SetActive(false);
        }
    }
}
