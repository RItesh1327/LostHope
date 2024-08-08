using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    // Heightmap resolution
    public int resolution = 512;

    // Scale of the terrain
    public Vector3 scale = Vector3.one;

    // Heightmap data
    private float[,] heightmapData;

    // Terrain game object
    private GameObject terrain;

    // Low poly rock prefab
    public GameObject[] lowPolyRockPrefabs;

    // Number of low poly rocks to generate
    public int numRocks = 100;

    // Minimum and maximum scale for the low poly rocks
    public Vector3 minRockScale = Vector3.one;
    public Vector3 maxRockScale = Vector3.one;

    // Minimum and maximum y-position for the low poly rocks
    public float minRockHeight = 0.0f;
    public float maxRockHeight = 1.0f;

    // Seed for the terrain and rock generation
    public int seed;

    // Random number generator
    private System.Random random;

    // Initialization
    void Start()
    {

        // Set the resolution and scale to the same value
        resolution = (int)scale.x;
        // Initialize the heightmap data
        heightmapData = new float[resolution, resolution];

        // Generate the heightmap data
        GenerateHeightmap();

        // Create the terrain game object
        terrain = Terrain.CreateTerrainGameObject(new TerrainData());

        // Set the scale and heightmap data for the terrain
        terrain.transform.localScale = scale;
        terrain.GetComponent<Terrain>().terrainData.size = scale;
        terrain.GetComponent<Terrain>().terrainData.SetHeights(0, 0, heightmapData);

        // Generate the low poly rocks
        GenerateRocks();
    }

    // Generates the heightmap data for the terrain
    void GenerateHeightmap()
    {
        // Initialize the random number generator with the specified seed
        random = new System.Random(seed);

        // Loop through each point in the heightmap data
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                // Generate a random height for the point
                heightmapData[x, y] = (float)random.NextDouble();
            }
        }
    }

    // Generates the low poly rocks on the terrain

    void GenerateRocks()
    {
        // Loop through the specified number of rocks
        for (int i = 0; i < numRocks; i++)
        {
            // Generate random position, scale, and rotation for the rock
            Vector3 position = new Vector3((float)random.NextDouble() * scale.x, minRockHeight + (float)random.NextDouble() * (maxRockHeight - minRockHeight), (float)random.NextDouble() * scale.z);
            Vector3 rockScale = new Vector3((float)random.NextDouble() * (maxRockScale.x - minRockScale.x) + minRockScale.x,
                                            (float)random.NextDouble() * (maxRockScale.y - minRockScale.y) + minRockScale.y,
                                            (float)random.NextDouble() * (maxRockScale.z - minRockScale.z) + minRockScale.z);
            Quaternion rotation = Quaternion.Euler(new Vector3((float)random.NextDouble() * 360.0f, (float)random.NextDouble() * 360.0f, (float)random.NextDouble() * 360.0f));

            // Choose a random low poly rock prefab from the list
            GameObject lowPolyRockPrefab = lowPolyRockPrefabs[random.Next(lowPolyRockPrefabs.Length)];

            // Instantiate the low poly rock game object
            GameObject rock = Instantiate(lowPolyRockPrefab, position, rotation);
            rock.transform.localScale = rockScale;
        }
    }
}
