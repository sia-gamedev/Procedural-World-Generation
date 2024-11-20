using System;
using UnityEngine;

public class LandscapeChunkBuilder : ChunkBuilder
{
    private NoiseGenerator noiseGenerator;
    private MeshGenerator meshGenerator;
    private ObjectGenerator objectGenerator;
    private Material terrainMaterial;
    private Vector2Int chunkPosition = Vector2Int.zero;
    private Vector2 chunkSize = Vector2Int.zero;
    private GameObject chunkParent;
    private Vector3 chunkOffset;
    private int chunkSeed = 0;

    public override ChunkBuilder SetTerrainNoiseGenerator(NoiseGenerator noiseGenerator)
    {
        this.noiseGenerator = noiseGenerator;
        return this;
    }
    public override ChunkBuilder SetTerrainMeshGenerator(MeshGenerator meshGenerator)
    {
        this.meshGenerator = meshGenerator;
        return this;
    }
    public override ChunkBuilder SetTerrainMaterial(Material terrainMaterial)
    {
        this.terrainMaterial = terrainMaterial;
        return this;
    }
    public override ChunkBuilder BuildTerrainGameObject()
    {
        chunkOffset = new Vector3(chunkPosition.x * chunkSize.x, 0, chunkPosition.y * chunkSize.y);

        Vector3[] positions = meshGenerator.GenerateVertexGridPositions(chunkSize);

        Vector3[] offsetPositions = new Vector3[positions.Length];
        for (int i = 0; i < offsetPositions.Length; i++)
        {
            offsetPositions[i] = positions[i] + chunkOffset;
        }

        float[] heightNoiseValues = noiseGenerator.GetHeightNoiseValues(offsetPositions);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].y = heightNoiseValues[i];
        }

        Mesh mesh = meshGenerator.GenerateMesh(positions);

        // making a gameobject on the offset position and setting it's mesh and collider
        chunkParent = new GameObject($"Chunk {chunkPosition.x}, {chunkPosition.y}");
        chunkParent.transform.position = chunkOffset;

        MeshFilter meshFilter = chunkParent.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = chunkParent.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = terrainMaterial;

        MeshCollider meshCollider = chunkParent.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        return this;
    }
    public override ChunkBuilder SetObjectGenerator(ObjectGenerator objectGenerator)
    {
        this.objectGenerator = objectGenerator;
        return this;
    }
    public override ChunkBuilder GenerateObjects()
    {
        Vector3 minPlacementPosition = new Vector3(chunkOffset.x, 100, chunkOffset.z);
        Vector3 maxPlacementPosition = minPlacementPosition + new Vector3(chunkSize.x, 0, chunkSize.y);

        GameObject[] objects = objectGenerator.GenerateObjects(minPlacementPosition, maxPlacementPosition, chunkSeed);

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.parent = chunkParent.transform;
        }

        return this;
    }
    public override ChunkBuilder SetChunkProperties(Vector2Int chunkPosition, Vector2 chunkSize)
    {
        this.chunkPosition = chunkPosition;
        this.chunkSize = chunkSize;
        chunkSeed = int.Parse($"{chunkPosition.x}{chunkPosition.y}");
        Debug.Log($"Chunk {chunkPosition.x}, {chunkPosition.y} seed: {chunkSeed}");
        return this;
    }
    public override ChunkBuilder Reset()
    {
        noiseGenerator = null;
        meshGenerator = null;
        chunkPosition = Vector2Int.zero;
        chunkSize = Vector2.one;
        chunkParent = null;
        chunkOffset = Vector3.zero;

        return this;
    }
    public override GameObject BuildChunk()
    {
        return chunkParent;
    }
}
