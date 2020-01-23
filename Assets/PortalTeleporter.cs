using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public GameObject player;

    public Transform receiver;

    private bool isPlayerOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Position "+player.position);
        if(isPlayerOverlapping){
            //Debug.Log("Position "+player.position);
            Vector3 portalToPlayer = player.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            //Debug.Log("Before "+dotProduct);
            if(dotProduct < 0f) {
                if(tag=="Blue"){
                    Debug.Log("Dot "+dotProduct);
                }
                //Debug.Log("Triggering Teleport");
                CharacterController cc = player.GetComponent(typeof(CharacterController)) as CharacterController;
                float rotattionDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotattionDiff += 180;
                player.transform.Rotate(Vector3.up, rotattionDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotattionDiff, 0f) * portalToPlayer;
                cc.enabled = false;
                player.transform.position = receiver.position + positionOffset;
                cc.enabled = true;
                //Debug.Break();
                //Debug.Log("After "+player.position);
                isPlayerOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player") {
            isPlayerOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            isPlayerOverlapping = false;
        }
    }
}
