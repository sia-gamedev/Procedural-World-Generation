using UnityEngine;

[CreateAssetMenu(fileName = "DiscreteGridMeshGenerator", menuName = "SO/MeshGenerator/DiscreteGridMeshGenerator")]
public class DiscreteGridMeshGenerator : MeshGenerator
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
                tris[triNumber + 2] = tris[triNumber + 3] = vertexNumber + 2;
                tris[triNumber + 5] = vertexNumber + 3;

                vertexNumber += 4;
                triNumber += 6;
            }
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
        Vector3[] gridPositions = new Vector3[cellCount.x * cellCount.y * 4];
        Vector2 cellSize = meshSize / cellCount;
        Vector2 cellSizeHalved = cellSize / 2f;

        int vertexNumber = 0;
        for (int x = 0; x < cellCount.x; x++) 
        {
			for (int z = 0; z < cellCount.y; z++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize.x + cellSizeHalved.x, 0, z * cellSize.y + cellSizeHalved.y);

				gridPositions[vertexNumber] = new Vector3(-cellSizeHalved.x, 0, -cellSizeHalved.y) + cellPosition;
				gridPositions[vertexNumber + 1] = new Vector3(-cellSizeHalved.x, 0, cellSizeHalved.y) + cellPosition;
				gridPositions[vertexNumber + 2] = new Vector3(cellSizeHalved.x, 0, -cellSizeHalved.y) + cellPosition;
				gridPositions[vertexNumber + 3] = new Vector3(cellSizeHalved.x, 0, cellSizeHalved.y) + cellPosition;

                vertexNumber += 4;
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