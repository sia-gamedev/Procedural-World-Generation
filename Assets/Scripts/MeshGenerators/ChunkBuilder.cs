using UnityEngine;

public abstract class ChunkBuilder
{
    public abstract ChunkBuilder SetTerrainNoiseGenerator(NoiseGenerator noiseGenerator);
    public abstract ChunkBuilder SetTerrainMeshGenerator(MeshGenerator meshGenerator);
    public abstract ChunkBuilder SetTerrainMaterial(Material terrainMaterial);
    public abstract ChunkBuilder BuildTerrainGameObject();
    public abstract ChunkBuilder SetObjectGenerator(ObjectGenerator objectGenerator);
    public abstract ChunkBuilder GenerateObjects();
    public abstract ChunkBuilder SetChunkProperties(Vector2Int chunkPosition, Vector2 chunkSize);
    public abstract ChunkBuilder Reset();
    public abstract GameObject BuildChunk();
}
