using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGridGenerator : MonoBehaviour
{
    private float[,,] voxelValues;

    private void Start()
    {
        voxelValues = new float[10, 10, 10];
         for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                for (int z = 0; z < 10; z++) {
                    voxelValues[x,y,z] = Random.Range(0f, 1f);
                    print(voxelValues[x,y,z]);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (voxelValues != null)
        {
            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {
                    for (int z = 0; z < 10; z++) {
                        Gizmos.color = Color.Lerp(Color.black, Color.white, voxelValues[x,y,z]);
                        Gizmos.DrawCube(new Vector3(x, y, z), Vector3.one * .2f);
                    }
                }
            }
        }
    }
}
