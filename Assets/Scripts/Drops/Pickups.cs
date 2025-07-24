using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Pickups : MonoBehaviour
{
    public event Action pickupAction;
    protected PlayerController playerScript;
    public AudioClip pickupSound;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == ConstantsValue.PLAYER_TAG)
        {
            OnPickup();
            if (GameRef.GetMusicState() == 0)
            {
                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag(ConstantsValue.PLAYER_TAG) != null)
        playerScript = GameObject.FindGameObjectWithTag(ConstantsValue.PLAYER_TAG).GetComponent<PlayerController>();
    }
    public abstract void OnPickup();
}
