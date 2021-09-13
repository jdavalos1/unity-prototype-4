using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityWarp : MonoBehaviour
{
    public float maxDistance = 15;
    public Vector3 offset;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if(playerDistance >= maxDistance)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
