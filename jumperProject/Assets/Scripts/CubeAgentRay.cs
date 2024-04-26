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
    private Vector3 barryPosition;
    public override void Initialize()
    {
        barryPosition = this.GetComponent<Transform>().position;
    }
    int currentItem;
    public override void OnEpisodeBegin()
    {
        Debug.Log("====NEW ROUND====");
        if (this.transform.localPosition.y < -10)
            this.transform.localPosition = new Vector3(6, -3, 0); this.transform.localRotation = Quaternion.identity;

        if (spawnedItem != null)
            Destroy(spawnedItem);

        currentItem = Random.Range(1, 3);

        if(currentItem == 1)
			spawnedItem = Instantiate(prefabEnemy, new Vector3(barryPosition.x + 6.25f, barryPosition.y, barryPosition.z), Quaternion.identity);
        else
			spawnedItem = Instantiate(prefabCoin, new Vector3(barryPosition.x + 6.25f, barryPosition.y, barryPosition.z), Quaternion.identity);

        int randomSpeed = Random.Range(-7, -4);

		spawnedItem.GetComponent<Rigidbody>().AddForce(
                new Vector3(randomSpeed, 0, 0),
                ForceMode.Impulse
            );
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en Agent posities
        sensor.AddObservation(currentItem);
        sensor.AddObservation(this.transform.localPosition);

    }

    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5;
    private bool canJump = true;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Acties, size = 2
        if (canJump && actionBuffers.ContinuousActions[0] > 0)
        {
            Debug.Log("jumped");
            canJump = false;
            this.GetComponent<Rigidbody>()
                .AddForce(
                new Vector3(0, 7, 0),
                ForceMode.Impulse
            );
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Contains("Enemy"))
        {
            Debug.Log("Touched box collider! -1f");
            SetReward(-1f);
            EndEpisode();
        }
        if (collision.collider.tag.Contains("Coin"))
        {
            Debug.Log("Touched coin! +1f");
            SetReward(1f);
            EndEpisode();
        }
        if (collision.collider.tag.Contains("Floor"))
        {
            canJump = true;
            Debug.Log("can jump");
        }
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
    }
    
    public void Barrier_MushroomTouched()
    {
        Debug.Log("Full reward! +1f");
        SetReward(1f);
        EndEpisode();
    }

    public void Barrier_CoinTouched()
    {
        Debug.Log("Didn't touch the coin!, -1f");
        SetReward(-1f);
        EndEpisode();
    }

}