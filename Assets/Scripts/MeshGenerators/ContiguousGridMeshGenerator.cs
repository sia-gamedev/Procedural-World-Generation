using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ContiguousGridMeshGenerator", menuName = "SO/GridMeshGenerator/ContiguousGridMeshGenerator")]
public class ContiguousGridMeshGenerator : MeshGenerator
{
    [SerializeField]
    private Vector2Int cellCount; // maximum possible map size is 128x128
    [SerializeField]
    private Color color;

    public override Mesh GenerateMesh(Vector3[] positions)
    {
        int[] tris = new int[cellCount.x * cellCount.y * 6];

        int vertexNumber = 0;
        int triNumber = 0;
        for (int x = 0; x < cellCount.x; x++)
        {
            for (int z = 0; z < cellCount.y; z++)
            {
                tris[triNumber] = vertexNumber;
                tris[triNumber + 1] = tris[triNumber + 4] = vertexNumber + 1;
                tris[triNumber + 2] = tris[triNumber + 3] = vertexNumber + (cellCount.y + 1);
                tris[triNumber + 5] = vertexNumber + +(cellCount.y + 1) + 1;

                vertexNumber++;
                triNumber += 6;
            }
            vertexNumber++;
        }

        Color[] colors = GenerateColors(positions);

        Mesh mesh = new Mesh();
        mesh.vertices = positions;
        mesh.triangles = tris;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        return mesh;
    }

    public override Vector3[] GenerateVertexGridPositions(Vector2 meshSize)
    {
        Vector3[] gridPositions = new Vector3[(cellCount.x + 1) * (cellCount.y + 1)];
        Vector2 cellSize = meshSize / cellCount;

        int vertexNumber = 0;
        for (int x = 0; x <= cellCount.x; x++) 
        {
			for (int z = 0; z <= cellCount.y; z++)
            {
				gridPositions[vertexNumber] = new Vector3(x * cellSize.x, 0, z * cellSize.y);
                vertexNumber++;
            }
        }

        return gridPositions;
    }
    private Color[] GenerateColors(Vector3[] positions)
    {
        Color[] colors = new Color[positions.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }
        return colors;
    }
}
