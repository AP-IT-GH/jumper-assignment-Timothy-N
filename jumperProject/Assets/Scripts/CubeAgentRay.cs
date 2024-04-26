using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgentRay : Agent
{
    public GameObject prefabEnemy;
    public GameObject prefabCoin;
    private GameObject spawnedItem;
    private bool canJump = true;

    public override void Initialize()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        if (transform.localPosition.y < -10)
        {
            transform.localPosition = new Vector3(6, -3, 0);
            transform.localRotation = Quaternion.identity;
        }

        if (spawnedItem != null)
            Destroy(spawnedItem);

        int currentItem = Random.Range(1, 3);

        spawnedItem = Instantiate(currentItem == 1 ? prefabEnemy : prefabCoin,
                                   new Vector3(transform.position.x + 7f, transform.position.y, transform.position.z),
                                   Quaternion.identity);

        int randomSpeed = Random.Range(-6, -2);
        spawnedItem.GetComponent<Rigidbody>().AddForce(new Vector3(randomSpeed, 0, 0), ForceMode.Impulse);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Random.Range(1, 3)); // Random observation
        sensor.AddObservation(transform.localPosition); // Agent's position
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float jumpAction = actionBuffers.ContinuousActions[0];

        if (canJump && jumpAction > 0)
        {
            Debug.Log("Jumped");
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
           
            SetReward(-1f);
            Debug.Log("Touched box collider! -1f");
            EndEpisode();
        }
        else if (collision.collider.CompareTag("Coin"))
        {
            
            SetReward(1f);
            Debug.Log("Touched coin! +1f");
            EndEpisode();
        }
        else if (collision.collider.CompareTag("Floor"))
        {
            canJump = true;
        }
    }


}
