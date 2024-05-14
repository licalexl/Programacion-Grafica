using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Cubito : MonoBehaviour
{
    public Material material;
    Vector3[] vertices = {


                           //Cube
                           new Vector3 (0, 0, 0), //0
                           new Vector3(0, 1, 0), //1
                           new Vector3(1, 1, 0), //2
                           new Vector3(1, 0, 0), //3
                           new Vector3(0, 1, 1), //4
                           new Vector3(0, 0, 1), //5
                           new Vector3(1, 1, 1), //6
                           new Vector3(1, 0, 1)  //7
                                                    };

    int[] triangles = { 
                        //Cube
                        0, 1, 2,
                        0, 2, 3,
                        1, 0, 5,
                        1, 5, 4,
                        2, 6, 3,
                        6, 7, 3,
                        1, 4, 2,
                        4, 6, 2,
                        5, 0, 3,
                        5, 3, 7,
                        4, 5, 7,
                        4, 7, 6
                        };




    void Cube()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }


    private void Start()
    {
        Cube();

    }
}