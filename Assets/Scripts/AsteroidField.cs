    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
using UnityEngine.SceneManagement;
    public class AsteroidField : MonoBehaviour
    {
        // Rigidbody component for the player's jet
        public Rigidbody rb;

        // Prefab for the asteroids
        public GameObject asteroidPrefab;

    public SceneManagement scenemanager1;

        // Number of asteroids to generate
        public int numAsteroids = 1000;

        // Range of the asteroid field
        public float fieldRange = 50.0f;

        // Minimum size of the asteroids
        public float minSize = 1.0f;

        // Maximum size of the asteroids
        public float maxSize = 5.0f;

        // Distance in front of the player at which to spawn new asteroids
        public float spawnDistance = 50.0f;

        // Time remaining in the level
        public float timeRemaining = 120.0f;

        // UI text element to display the time remaining
        public TMP_Text timeText;

        // List to store the asteroids
        private List<GameObject> asteroids;

    // Delete distance which the astroid should be deleted
    public float deleteDistance;
        // Use this for initialization
        void Start()
        {
            // Initialize the list of asteroids
            asteroids = new List<GameObject>();

            // Generate the initial set of asteroids
            GenerateAsteroids();
        }
        // Update is called once per frame
        void Update()
        {
            // Decrement the time remaining
            timeRemaining -= Time.deltaTime;
            // Update the time remaining text
            timeText.text = "Time Remaining: " + timeRemaining.ToString("F2");

            // Get the player's position
            Vector3 playerPosition = rb.transform.position;

            // Check if the player is a certain distance away from the end of the field
            if (playerPosition.z > transform.position.z + fieldRange - spawnDistance || playerPosition.x > transform.position.x + fieldRange - spawnDistance || playerPosition.y > transform.position.y + fieldRange - spawnDistance)
            {
                // Move the asteroid belt forward by twice the field range
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + fieldRange * 2);
                // Spawn new asteroids in front of the player
                if(asteroids.Count <= numAsteroids)
                    GenerateAsteroids();
            }

        for (int i = asteroids.Count - 1; i >= 0; i--)
        {
            GameObject asteroid = asteroids[i];
            // Check if the asteroid is far enough behind the player
            if (asteroid.transform.position.z < playerPosition.z - deleteDistance)
            {
                // Destroy the asteroid
                Destroy(asteroid);
                // Remove the asteroid from the list
                asteroids.RemoveAt(i);
            }
        }

        if(timeRemaining <= 0 && SceneManager.GetActiveScene().name == "Asteroid1" )
        {
            timeRemaining = 0;
            scenemanager1.StartCoroutine("afterasteroid1");
            

        }
        if(timeRemaining <= 0 && SceneManager.GetActiveScene().name == "Asteroid2")
        {
            timeRemaining = 0;
            scenemanager1.StartCoroutine("pluto");

        }


    }



    // Function to generate a set of asteroids
    void GenerateAsteroids()
    {
        // Get the position and orientation of the player
        Vector3 playerPosition = rb.transform.position;
        Quaternion playerRotation = rb.transform.rotation;

        // Loop through the desired number of asteroids
        for (int i = 0; i < numAsteroids; i++)
        {
            // Generate a random size for the asteroid
            float size = Random.Range(minSize, maxSize);

            // Generate a random position for the asteroid
            // The x, y, and z positions will be fixed distances from the player's position,
            // but rotated to match the player's orientation
            Vector3 offset = new Vector3(Random.Range(-120.0f, 120.0f), Random.Range(-120.0f, 120.0f), Random.Range(-200.0f, 200.0f));

         

            Vector3 position = playerPosition + playerRotation * offset;
            // Generate a random rotation for the asteroid
            Quaternion rotation = Quaternion.Euler(new Vector3(Random.Range(0.0f, 360.0f),
                                                                 Random.Range(0.0f, 360.0f),
                                                                 Random.Range(0.0f, 360.0f)));
            // Instantiate the asteroid at the random position and rotation
            GameObject asteroid = Instantiate(asteroidPrefab, position, rotation);
            // Set the asteroid's size
            asteroid.transform.localScale = new Vector3(size, size, size);
            // Add the asteroid to the list
            asteroids.Add(asteroid);
        }
    }

         // Function to destroy all the asteroids
        void DestroyAsteroids()
        {
            // Loop through all the asteroids in the list
            foreach (GameObject asteroid in asteroids)
            {
                // Destroy the asteroid game object
                Destroy(asteroid);
            }
            // Clear the list of asteroids
            asteroids.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the player has collided with an asteroid
            if (other.transform.tag == "Player")
            {
                // End the game
                GameOver();
            }
        }

        // Function to end the game
        void GameOver()
        {
            // Disable the player's jet
            rb.gameObject.SetActive(false);
            // Destroy all the asteroids
            DestroyAsteroids();
            // Update the game over text
          //  gameOverText.text = "Game Over!";
            // Set the game over text to be active
        //    gameOverText.gameObject.SetActive(true);
        }
    }

