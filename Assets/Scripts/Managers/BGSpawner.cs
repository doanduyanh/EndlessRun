using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    private GameObject[] BGs;
    private float lastX;
    private GameObject[] Grounds;
    private float lastGroundX;

    private void Awake()
    {
        GetBGLastX();
        GetGroundLastX();
    }

    void GetBGLastX()
    {
        BGs = GameObject.FindGameObjectsWithTag("BG");
        lastX = BGs[0].transform.position.x;

        for(int i = 1; i<BGs.Length; i++)
        {
            if(lastX < BGs[i].transform.position.x)
            {
                lastX = BGs[i].transform.position.x;
            }
        }
    }
    void GetGroundLastX()
    {
        Grounds = GameObject.FindGameObjectsWithTag("Ground");
        lastGroundX = Grounds[0].transform.position.x;

        for(int i = 1; i< Grounds.Length; i++)
        {
            if(lastGroundX < Grounds[i].transform.position.x)
            {
                lastGroundX = Grounds[i].transform.position.x;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ConstantsValue.BG_TAG || other.tag == ConstantsValue.GROUND_TAG)
        {
            if(other.tag == ConstantsValue.BG_TAG && other.transform.position.x >= lastX)
            {
                Vector3 temp = other.transform.position;
                //float width = ((BoxCollider)other).size.x;
                float width = other.transform.localScale.x;

                for(int i = 0; i < BGs.Length; i++)
                {
                    if (!BGs[i].activeInHierarchy)
                    {
                        //temp.y = lastY - height;
                        temp.x += width;

                        lastX = temp.x;
                        BGs[i].transform.position = temp;
                        BGs[i].SetActive(true);
                    }
                }
            }
            else if(other.tag == ConstantsValue.GROUND_TAG && other.transform.position.x >= lastGroundX)
            {
                Vector3 temp = other.transform.position;
                //float width = ((BoxCollider)other).size.x;
                float width = other.transform.localScale.x;

                for(int i = 0; i < Grounds.Length; i++)
                {
                    if (!Grounds[i].activeInHierarchy)
                    {
                        //temp.y = lastY - height;
                        temp.x += width;

                        lastGroundX = temp.x;
                        Grounds[i].transform.position = temp;
                        Grounds[i].SetActive(true);
                    }
                }
            }
        }
    }
}
