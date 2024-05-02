using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Matriz : MonoBehaviour
{
    public Vector3 Traslation(Vector4 position, Vector4 delta)

    {
        float[,] identity = new float[,]
        {
            {1,0,0, delta.x},
            {0,1,0, delta.y},
            {0,0,1, delta.z},
            {0,0,0, 1},

        };

        float x = (identity[0, 0] * position.x) + (identity[0, 1] * position.y) + (identity[0, 2] * position.z) + (identity[0, 3] * 1);
        float y = (identity[1, 0] * position.x) + (identity[1, 1] * position.y) + (identity[1, 2] * position.z) + (identity[1, 3] * 1);
        float z = (identity[2, 0] * position.x) + (identity[2, 1] * position.y) + (identity[2, 2] * position.z) + (identity[2, 3] * 1);
        //float w = (identity[3, 0] * position.x) + (identity[3, 1] * position.y) + (identity[3, 2] * position.z) + (identity[3, 3] * 1);

        Vector3 pos = new Vector3(x, y, z);
        return pos;

    }

    public Vector3 RotZ(Vector4 position, float angle)

    {
        float rad = 30 * Mathf.Deg2Rad;

        float[,] identity = new float[,]
        {
            {Mathf.Cos(rad),-Mathf.Sin(rad),0, 0},
            {Mathf.Sin(rad),Mathf.Cos(rad),0, 0},
            {0,0,1, 0},
            {0,0,0, 1},

        };

        float x = (identity[0, 0] * position.x) + (identity[0, 1] * position.y) + (identity[0, 2] * position.z) + (identity[0, 3] * 1);
        float y = (identity[1, 0] * position.x) + (identity[1, 1] * position.y) + (identity[1, 2] * position.z) + (identity[1, 3] * 1);
        float z = (identity[2, 0] * position.x) + (identity[2, 1] * position.y) + (identity[2, 2] * position.z) + (identity[2, 3] * 1);
        //float w = (identity[3, 0] * position.x) + (identity[3, 1] * position.y) + (identity[3, 2] * position.z) + (identity[3, 3] * 1);

        Vector3 pos = new Vector3(x, y, z);
        return pos;

    }

    public Vector3 RotX(Vector4 position, float angle)

    {
        float rad = 30 * Mathf.Deg2Rad;

        float[,] identity = new float[,]
        {
            {1  ,0  ,0  ,0},
            {0  ,Mathf.Cos(rad) ,-Mathf.Sin(rad)    , 0},
            {0  ,Mathf.Sin(rad) ,Mathf.Cos(rad) , 0},
            {0  ,0  ,0  ,1},

        };

        float x = (identity[0, 0] * position.x) + (identity[0, 1] * position.y) + (identity[0, 2] * position.z) + (identity[0, 3] * 1);
        float y = (identity[1, 0] * position.x) + (identity[1, 1] * position.y) + (identity[1, 2] * position.z) + (identity[1, 3] * 1);
        float z = (identity[2, 0] * position.x) + (identity[2, 1] * position.y) + (identity[2, 2] * position.z) + (identity[2, 3] * 1);
        //float w = (identity[3, 0] * position.x) + (identity[3, 1] * position.y) + (identity[3, 2] * position.z) + (identity[3, 3] * 1);

        Vector3 pos = new Vector3(x, y, z);
        return pos;

    }

    public Vector3 RotY(Vector4 position, float angle)

    {
        float rad = 30 * Mathf.Deg2Rad;

        float[,] identity = new float[,]
        {
            {Mathf.Cos(rad) ,0    ,Mathf.Sin(rad)  , 0},
            {0  ,1     ,0  ,0},
            {-Mathf.Sin(rad)  ,0  ,Mathf.Cos(rad)  , 0},
            {0  ,0  ,0  , 1},

        };

        float x = (identity[0, 0] * position.x) + (identity[0, 1] * position.y) + (identity[0, 2] * position.z) + (identity[0, 3] * 1);
        float y = (identity[1, 0] * position.x) + (identity[1, 1] * position.y) + (identity[1, 2] * position.z) + (identity[1, 3] * 1);
        float z = (identity[2, 0] * position.x) + (identity[2, 1] * position.y) + (identity[2, 2] * position.z) + (identity[2, 3] * 1);
        //float w = (identity[3, 0] * position.x) + (identity[3, 1] * position.y) + (identity[3, 2] * position.z) + (identity[3, 3] * 1);

        Vector3 pos = new Vector3(x, y, z);
        return pos;

    }
}
