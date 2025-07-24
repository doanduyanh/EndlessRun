using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 20f;
    public float life = 1f;
    public float damage = 50f;

    private float lifeTimer;


    
    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        if (Time.time > lifeTimer + life)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.tag == ConstantsValue.ENEMY_TAG)
        {
            other.GetComponent<CharBase>().TakeDamage(damage);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (other != null && other.tag == ConstantsValue.GROUND_TAG)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public void Fire(Vector3 position, Vector3 target, float bulletDamage = 50f)
    {
        lifeTimer = Time.time;
        transform.position = position;
        Vector3 direction = target - position;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 vop = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
        transform.forward = vop;
        transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);
        this.damage = bulletDamage;

    }
}
