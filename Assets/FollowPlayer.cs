using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 _offset;
    Rigidbody playerRb;

    Quaternion lastFrameLookR;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        _offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerRb.velocity.sqrMagnitude > 0.1f)
        {
            //Where to look next
            Quaternion thisFrameLookR = Quaternion.LookRotation(playerRb.velocity);

            //Where we looked previously
            Quaternion realLookR = Quaternion.Slerp(lastFrameLookR, thisFrameLookR, 0.001f);

            lastFrameLookR = realLookR;

            //
            Vector3 rotatedOffset = realLookR * _offset;
            transform.position = player.transform.position + rotatedOffset;
            transform.LookAt(player.transform.position);
        }

    }
}
