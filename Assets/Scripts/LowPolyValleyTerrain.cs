using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPolyValleyTerrain : MonoBehaviour
{
    // Height map for the terrain
    public Texture2D heightMap;

    // Material for the terrain
    public Material terrainMaterial;

    // Size of the terrain
    public Vector3 terrainSize = new Vector3(100, 10, 100);

    // Height of the terrain
    public float height = 10.0f;

    // Number of vertices in the x and z directions
    public int numVerticesX = 100;
    public int numVerticesZ = 100;

    // Smoothness of the terrain
    public float smoothness = 1.0f;

    // Height map offset
    public Vector2 heightMapOffset = Vector2.zero;

    // Perlin noise parameters
    public float perlinScale = 1.0f;
    public float perlinHeight = 1.0f;
    public int perlinOctaves = 1;
    public float perlinPersistence = 1.0f;
    public float perlinLacunarity = 2.0f;

    // Array of vertices for the terrain mesh
    private Vector3[] vertices;

    // Array of triangles for the terrain mesh
    private int[] triangles;

    // Array of normals for the terrain mesh
    private Vector3[] normals;

    // Array of colors for the terrain mesh
    private Color[] colors;

    // Mesh for the terrain
    private Mesh terrainMesh;

    // Mesh filter for the terrain
    private MeshFilter terrainMeshFilter;

    //Mesh collider for the terrain 
    private MeshCollider terrainMeshCollider;

    // Mesh renderer for the terrain
    private MeshRenderer terrainMeshRenderer;

    // Use this for initialization
    void Start()
    {
        // Generate the terrain mesh
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Generates the terrain mesh
    void GenerateTerrain()
    {
        // Create the mesh and mesh filter for the terrain
        terrainMesh = new Mesh();
        terrainMeshFilter = gameObject.AddComponent<MeshFilter>();
        terrainMeshFilter.mesh = terrainMesh;

        // Create the mesh renderer for the terrain
        terrainMeshRenderer = gameObject.AddComponent<MeshRenderer>();
        terrainMeshRenderer.material = terrainMaterial;

        // Calculate the number of vertices and triangles for the terrain
        int numVertices = numVerticesX * numVerticesZ;
        int numTriangles = (numVerticesX - 1) * (numVerticesZ - 1) * 2;

        if (numVerticesX % 2 == 0)
        {
            numTriangles -= numVerticesZ / 2;
        }
        if (numVerticesZ % 2 == 0)
        {
            numTriangles -= numVerticesX / 2;
        }

        // Initialize the arrays for the vertices, triangles, normals, and colors
        vertices = new Vector3[numVertices];
        triangles = new int[numTriangles * 3];
        normals = new Vector3[numVertices];
        colors = new Color[numVertices];

        // Generate the vertices and triangles for the terrain
        for (int z = 0; z < numVerticesZ; z++)
        {
            for (int x = 0; x < numVerticesX; x++)
            {
                // Calculate the index of the current vertex
                int index = z * numVerticesX + x;

                // Calculate the position of the vertex
                float xPos = terrainSize.x * ((float)x / (float)(numVerticesX - 1)) - terrainSize.x * 0.5f;
                float zPos = terrainSize.z * ((float)z / (float)(numVerticesZ - 1)) - terrainSize.z * 0.5f;
                float yPos = 0.0f;

                // Calculate the height of the vertex using a height map or Perlin noise
                if (heightMap != null && heightMap.width > x && heightMap.height > z)
                {
                    yPos = heightMap.GetPixel((int)(x * smoothness + heightMapOffset.x), (int)(z * smoothness + heightMapOffset.y)).grayscale * height;
                }
                else
                {
                    yPos = Mathf.PerlinNoise(xPos * perlinScale, zPos * perlinScale) * perlinHeight;
                    for (int i = 1; i < perlinOctaves; i++)
                    {
                        float scale = perlinLacunarity * i;
                        float height = Mathf.PerlinNoise(xPos * scale, zPos * scale) * perlinHeight * Mathf.Pow(perlinPersistence, i);
                        yPos += height;
                    }
                }

                // Set the position, normal, and color of the vertex
                vertices[index] = new Vector3(xPos, yPos, zPos);
                normals[index] = Vector3.up;
                colors[index] = Color.Lerp(Color.black, Color.white, yPos / height);
            }
        }

        // Generate the triangles for the terrain
        int triangleIndex = 0;
        for (int z = 0; z < numVerticesZ - 1; z++)
        {
            for (int x = 0; x < numVerticesX - 1; x++)
            {
                // Calculate the indices of the vertices for the current triangle
                int a = z * numVerticesX + x;
                int b = a + numVerticesX;
                int c = a + 1;
                int d = b + 1;

                // Add the triangles to the triangles array
                if (triangleIndex < triangles.Length)
                {
                    triangles[triangleIndex++] = a;
                    triangles[triangleIndex++] = b;
                    triangles[triangleIndex++] = c;
                }
                if (triangleIndex < triangles.Length)
                {
                    triangles[triangleIndex++] = b;
                    triangles[triangleIndex++] = d;
                    triangles[triangleIndex++] = c;
                }
            }
        }
        // Set the values for the mesh and apply the changes
        terrainMesh.vertices = vertices;
        terrainMesh.triangles = triangles;
        terrainMesh.normals = normals;
        terrainMesh.colors = colors;
        terrainMesh.RecalculateNormals();
        terrainMesh.RecalculateBounds();

        // Add a mesh collider to the terrain
        terrainMeshCollider = gameObject.AddComponent<MeshCollider>();
        terrainMeshCollider.sharedMesh = terrainMesh;
    }
}
