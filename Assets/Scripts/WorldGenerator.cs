using System;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("ChunkPresets")]
    [SerializeField]
    private int presetNumber;
    [SerializeField]
    private ChunkPreset[] chunkPresets;

    [Header("Misc")]
    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private Vector2Int chunkMapSize;
    [SerializeField]
    private Vector2 chunkSize;

    private Vector3[] linePointsGizmo;
    private Vector3[] hitPointsGizmo;

    [Serializable]
    private struct ChunkPreset
    {
        public NoiseGenerator noiseGenerator;
        public MeshGenerator meshGenerator;
        public Material meshMaterial;
        public ObjectGenerator[] objectGenerators;
    }
    private void Start()
    {
        BuildChunks(chunkPresets[presetNumber]);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            while (transform.childCount > 0) 
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            BuildChunks(chunkPresets[presetNumber]);
        }
    }
    private void BuildChunks(ChunkPreset chunkPreset)
    {
        ChunkBuilder chunkBuilder = new LandscapeChunkBuilder();
        chunkBuilder.SetTerrainNoiseGenerator(chunkPreset.noiseGenerator)
                    .SetTerrainMeshGenerator(chunkPreset.meshGenerator);

        if (chunkPreset.meshMaterial != null)
            chunkBuilder.SetTerrainMaterial(chunkPreset.meshMaterial);
        
        for (int x = 0; x < chunkMapSize.x; x++)
        {
            for (int y = 0; y < chunkMapSize.y; y++)
            {
                Vector2Int chunkPosition = new Vector2Int(x, y);
                chunkBuilder.SetChunkProperties(chunkPosition, chunkSize)
                            .BuildTerrainGameObject();
                
                foreach (ObjectGenerator objectGenerator in chunkPreset.objectGenerators)
                {
                    chunkBuilder.SetObjectGenerator(objectGenerator)
                                .GenerateObjects();
                }

                GameObject chunkParent = chunkBuilder.BuildChunk();
                chunkParent.transform.parent = transform;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (!showGizmos)
            return;

        if (linePointsGizmo == null || hitPointsGizmo == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLineList(linePointsGizmo);

        foreach (Vector3 center in hitPointsGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(center, 0.5f);
        }
    }
}