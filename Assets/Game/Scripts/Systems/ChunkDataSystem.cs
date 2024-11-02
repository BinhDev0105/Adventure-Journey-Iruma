using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct ChunkDataSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ChunkData>();
        state.RequireForUpdate<WorldSettingComponent>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var worldSetting = SystemAPI.GetSingleton<WorldSettingComponent>();
        int chunkSize = worldSetting.ChunkSize;
        int chunkHeight = worldSetting.ChunkHeight;
        int worldSize = worldSetting.WorldSize;
        float noiseScale = worldSetting.NoiseScale;
        float waterThreshold = worldSetting.WaterThreshold;
        var chunkDataJob = new ChunkDataJob()
        {
            ChunkSize = chunkSize,
            ChunkHeight = chunkHeight,
            WorldSize = worldSize,
            NoiseScale = noiseScale,
            WaterThreshold = waterThreshold,
        };
        chunkDataJob.Schedule();
    }
    [BurstCompile]
    private partial struct ChunkDataJob : IJobEntity
    {
        public int ChunkSize;
        public int ChunkHeight;
        public int WorldSize;
        public float NoiseScale;
        public float WaterThreshold;
        void Execute(DynamicBuffer<ChunkData> chunkDatas, ref LocalTransform transform)
        {
            //chunkDatas.Add(new ChunkData { BlockId = 1});
            //Debug.Log($"{chunkDatas.Length}");
            GenerateBlock(chunkDatas, ChunkSize, ChunkHeight, NoiseScale, WaterThreshold, transform.Position);
        }
    }

    private static void GenerateBlock(DynamicBuffer<ChunkData> chunkDatas, int chunkSize, int chunkHeight, float noiseScale, float waterThreshold, float3 chunkPosition)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                float noiseValue = Mathf.PerlinNoise((chunkPosition.x + x) * noiseScale,(chunkPosition.z + z) * noiseScale);
                int groundPosition = Mathf.RoundToInt(noiseValue * chunkHeight);
                for (int y = 0; y < chunkHeight; y++)
                {
                    int blockID = 1;//Dirt
                    if (y > groundPosition)
                    {
                        if (y < waterThreshold)
                        {
                            blockID = 2;
                        }
                        else
                        {
                            blockID = 0;
                        }
                    }
                    else if (y == groundPosition)
                    {
                        blockID = 3;
                    }
                    ChunkHelper.SetBlock(chunkDatas, chunkSize, chunkHeight, new float3(x, y, z), blockID);
                }
            }
        }
    }
}
