using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticales : CharBase
{
    public int score = 50;
    private PlayerScore playerScript;
    public float yOffsetPickupSpawn = 1.5f;


    public int pickupChange;
    public GameObject[] pickups;


    private void Start()
    {
        base.Start();
        playerScript = GameObject.FindGameObjectWithTag(ConstantsValue.PLAYER_TAG).GetComponent<PlayerScore>();
    }
    public void UpdateStatByWeitgh(int weight)
    {
        health += (float)(weight * ConstantsValue.ENEMYGROWPERWEIGHT * health);
        damage += (float)(weight * ConstantsValue.ENEMYGROWPERWEIGHT * damage);
        score += (int)(weight * ConstantsValue.ENEMYGROWPERWEIGHT * score);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == ConstantsValue.PLAYER_TAG)
        {
            other.GetComponent<CharBase>().TakeDamage(damage);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public override void OnDie()
    {
        playerScript.ObsticaleKilled(score);
        int ranNum = Random.Range(0, 101);
        if (ranNum < pickupChange)
        {
            Vector3 finalPos = transform.position;
            finalPos.y += yOffsetPickupSpawn;
            Instantiate(pickups[Random.Range(0, pickups.Length)], finalPos, transform.rotation);
        }
        Destroy(gameObject);
    }
}
