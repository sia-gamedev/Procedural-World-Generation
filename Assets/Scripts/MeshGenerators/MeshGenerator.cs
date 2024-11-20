using UnityEngine;

public abstract class MeshGenerator : ScriptableObject
{
    public abstract Mesh GenerateMesh(Vector3[] positions);
    public abstract Vector3[] GenerateVertexGridPositions(Vector2 meshSize);
}
