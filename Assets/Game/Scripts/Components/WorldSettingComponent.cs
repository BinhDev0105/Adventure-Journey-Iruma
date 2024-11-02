using Unity.Entities;
using System;
using Unity.Mathematics;

[Serializable]
public struct WorldSettingComponent : IComponentData
{
    public float3 playerPosition;
    public int WorldSize;
    public int ChunkSize;
    public int ChunkHeight;
    public float NoiseScale;
    public float WaterThreshold;
}
