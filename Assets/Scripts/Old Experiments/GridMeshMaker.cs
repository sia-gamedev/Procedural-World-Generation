using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GridMeshMaker : MonoBehaviour
{
    private Vector2 gridCellSize; // x is x, y is z
    private Vector2Int gridCellCount; // x is x, y is z
    private float noiseScale = 0.0001f;
    
    private Vector3[] vertices;
    private int[] tris;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
    }

    private void GenerateYValues()
    {
        int i = 0;
        for (int z = 0; z <= gridCellCount.y; z++) 
        {
			for (int x = 0; x <= gridCellCount.x; x++) 
            {
				vertices[i].y = Mathf.PerlinNoise(vertices[i].x / noiseScale, vertices[i].z / noiseScale) * 10f;
                print(vertices[i].x.ToString() + " | " + vertices[i].z.ToString());
                print(vertices[i].y);
                i++;
            }
        }
    }

    private void PopulateWithPerlinNoiseValues(Vector3[] points, float scale, int octaves, float lacunarity, float persistence)
    {
        if (scale < 0.0001f)
            scale = 0.0001f;
        
        int i = 0;
        for (int z = 0; z <= gridCellCount.y; z++) 
        {
			for (int x = 0; x <= gridCellCount.x; x++) 
            {
				points[i].x /= scale;
                points[i].z /= scale;
                i++;
            }
        }
    }

    private void GenerateContiguousGridMesh()
    {
        vertices = new Vector3[(gridCellCount.x + 1) * (gridCellCount.y + 1)];

        int vertexNumber = 0;
        for (int z = 0; z <= gridCellCount.y; z++) 
        {
			for (int x = 0; x <= gridCellCount.x; x++) 
            {
				vertices[vertexNumber] = new Vector3(x * gridCellSize.x, 0, z * gridCellSize.y);
                vertexNumber++;
            }
        }

        GenerateYValues();

        tris = new int[gridCellCount.x * gridCellCount.y * 6];

        vertexNumber = 0;
        int triNumber = 0;

        for (int z = 0; z < gridCellCount.y; z++) 
        {
			for (int x = 0; x < gridCellCount.x; x++) 
            {
				tris[triNumber] = vertexNumber;
                tris[triNumber + 1] = tris[triNumber + 4] = vertexNumber + (gridCellCount.y + 1);
                tris[triNumber + 2] = tris[triNumber + 3] = vertexNumber + 1;
                tris[triNumber + 5] = vertexNumber + (gridCellCount.y + 1) + 1;
                vertexNumber++;
                triNumber += 6;
            }
            vertexNumber++;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    private void GenerateDiscreteGridMesh()
    {
        vertices = new Vector3[gridCellCount.x * gridCellCount.y * 4];

        int vertexNumber = 0;
        for (int z = 0; z <= gridCellCount.y; z++) 
        {
			for (int x = 0; x <= gridCellCount.x; x++) 
            {
				vertices[vertexNumber] = new Vector3(x * gridCellSize.x, 0, z * gridCellSize.y);
                vertexNumber++;
            }
        }

        GenerateYValues();

        
        meshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) 
        {
			Gizmos.DrawSphere(transform.TransformPoint(vertices[i]), 0.1f);
		}
	}
}
