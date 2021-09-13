using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    private Transform target;
    private bool isHoming;
    private float rocketStrength = 15.0f;
    private float rocketSpeed = 15.0f;
    private float aliveTimer = 5.0f;

    void Update()
    {
        if(isHoming && target != null)
        {
            Vector3 lookNormDir = (target.transform.position - transform.position).normalized;
            transform.position += lookNormDir * rocketSpeed * Time.deltaTime;
            transform.LookAt(target);
        }
        if(target == null)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
        isHoming = true;
        Destroy(gameObject, aliveTimer);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRb.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
