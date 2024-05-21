using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class icosaedro : MonoBehaviour
{
     Vector3[] vertices = {
        new Vector3(-1,  Phi,  0),
        new Vector3( 1,  Phi,  0),
        new Vector3(-1, -Phi,  0),
        new Vector3( 1, -Phi,  0),
        new Vector3( 0, -1,  Phi),
        new Vector3( 0,  1,  Phi),
        new Vector3( 0, -1, -Phi),
        new Vector3( 0,  1, -Phi),
        new Vector3( Phi,  0, -1),
        new Vector3( Phi,  0,  1),
        new Vector3(-Phi,  0, -1),
        new Vector3(-Phi,  0,  1)
    };

    int[] triangles = {
        0, 11,  5,
        0,  5,  1,
        0,  1,  7,
        0,  7, 10,
        0, 10, 11,
        1,  5,  9,
        5, 11,  4,
        11, 10,  2,
        10,  7,  6,
        7,  1,  8,
        3,  9,  4,
        3,  4,  2,
        3,  2,  6,
        3,  6,  8,
        3,  8,  9,
        4,  9,  5,
        2,  4, 11,
        6,  2, 10,
        8,  6,  7,
        9,  8,  1
    };

    Vector2[] uvs = {
                new Vector2(0.972128f, 0.94662f), // Vértice 0
                new Vector2(0.848105f, 0.77841f), // Vértice 1
                new Vector2(0.728964f, 0.948163f), // Vértice 2
                new Vector2(0.603964f, 0.779953f), // Vértice 3
                new Vector2(0.721151f, 0.612515f), // Vértice 4
                new Vector2(0.968221f, 0.610972f), // Vértice 5
                new Vector2(0.845174f, 0.439676f), // Vértice 6
                new Vector2(0.60201f, 0.438904f), // Vértice 7
                new Vector2(0.721151f, 0.266065f), // Vértice 8
                new Vector2(0.967245f,0.266837f), // Vértice 9           
                new Vector2(0.967245f,0.266837f), // Vértice 9           
                new Vector2(0.967245f,0.266837f), // Vértice 9           
     
        };

    private const float Phi = 1.618033988749895f; 

    void CreateCenteredIcosahedron()
    {
        // Calcular el centroide
        Vector3 centroid = Vector3.zero;
        foreach (Vector3 vertex in vertices)
        {
            centroid += vertex;
        }
        centroid /= vertices.Length;  // Promedio para encontrar el centroide

        // Ajustar vértices para centrar el icosaedro
        Vector3[] centeredVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            centeredVertices[i] = vertices[i] - centroid;
        }

        // Crear el mesh y asignar material
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.Clear();
        mesh.vertices = centeredVertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    // Se llama una vez al iniciar
    void Start()
    {
        CreateCenteredIcosahedron();
    }
}
