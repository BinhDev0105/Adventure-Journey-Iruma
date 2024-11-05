using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
public class WorldAuthoring : MonoBehaviour
{
    public float3 playerPosition;
    public int WorldSize;
    public int ChunkSize;
    public int ChunkHeight;
    public float NoiseScale;
    public float WaterThreshold;
    class WorldBaker : Baker<WorldAuthoring>
    {
        public override void Bake(WorldAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new WorldSetting
            {
                playerPosition = authoring.playerPosition,
                WorldSize = authoring.WorldSize,
                ChunkSize = authoring.ChunkSize,
                ChunkHeight = authoring.ChunkHeight,
                NoiseScale = authoring.NoiseScale,
                WaterThreshold = authoring.WaterThreshold,
            });
        }
    }
}
