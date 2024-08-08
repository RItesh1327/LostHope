using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; // prefab of the asteroid to spawn
    public float spawnInterval = 1f; // interval between asteroid spawns
    public float spawnDistance = 10f; // distance in front of the player that asteroids will spawn
    public float asteroidSpeed = 5f; // speed that asteroids will move towards the player
    public float revolveDistance = 20f; // distance that asteroids will revolve around the player
    public float revolveDirection = 1f; // direction that asteroids will revolve (1 = clockwise, -1 = counterclockwise)
    public float travelDistance = 30f; // distance the player must travel to end the game
    private float distanceTraveled = 0f; // distance the player has traveled
    private Transform playerTransform; // transform of the player
    private float spawnTimer = 0f; // timer for spawning asteroids

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // get the player's transform
    }

    void Update()
    {
        distanceTraveled += Time.deltaTime * playerTransform.forward.magnitude; // update distance traveled
        spawnTimer += Time.deltaTime; // update the spawn timer

        // if the distance traveled is less than the required travel distance and the spawn timer has reached the spawn interval
        if (distanceTraveled < travelDistance && spawnTimer >= spawnInterval)
        {
            // determine the position to spawn the asteroid
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;
            // determine the rotation of the asteroid
            Quaternion spawnRotation = Quaternion.Euler(0f, revolveDirection * distanceTraveled * 360f / revolveDistance, 0f);

            // instantiate the asteroid at the spawn position and rotation
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
            // set the asteroid's speed
            asteroid.GetComponent<Rigidbody>().velocity = playerTransform.forward * asteroidSpeed;

            spawnTimer = 0f; // reset the spawn timer
        }
    }
}