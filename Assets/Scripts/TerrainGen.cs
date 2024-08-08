using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }
    void CreateShape2()
    {
        // Generate a random number of vertices
        int numVertices = Random.Range(100, 200);

        // Initialize the vertices array
        vertices = new Vector3[numVertices];

        // Generate random positions for the vertices
        for (int i = 0; i < numVertices; i++)
        {
            float x = Random.Range(-10.0f, 10.0f);
            float y = Random.Range(-10.0f, 10.0f);
            float z = Random.Range(-10.0f, 10.0f);
            vertices[i] = new Vector3(x, y, z);
        }

        // Generate a random number of triangles
        int numTriangles = Random.Range(50, 100);

        // Initialize the triangles array
        triangles = new int[numTriangles * 3];

        // Generate random indices for the triangles
        for (int i = 0; i < numTriangles; i++)
        {
            int a = Random.Range(0, numVertices);
            int b = Random.Range(0, numVertices);
            int c = Random.Range(0, numVertices);
            triangles[i * 3] = a;
            triangles[i * 3 + 1] = b;
            triangles[i * 3 + 2] = c;
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        // optionally, add a mesh collider (As suggested by Franku Kek via Youtube comments).
        // To use this, your MeshGenerator GameObject needs to have a mesh collider
        // component added to it.  Then, just re-enable the code below.
        /*
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        //*/
    }

   
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i=0; i<vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    

}