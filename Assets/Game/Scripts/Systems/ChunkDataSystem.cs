using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//public partial struct ChunkDataSystem : ISystem
//{
//    [BurstCompile]
//    public void OnCreate(ref SystemState state)
//    {
//        state.RequireForUpdate<ChunkData>();
//        state.RequireForUpdate<WorldSetting>();
//        state.RequireForUpdate<WorldPrefab>();
//    }
//    [BurstCompile]
//    public void OnUpdate(ref SystemState state)
//    {
//        state.Enabled = false;
//        var worldSetting = SystemAPI.GetSingleton<WorldSetting>();
//        int chunkSize = worldSetting.ChunkSize;
//        int chunkHeight = worldSetting.ChunkHeight;
//        int worldSize = worldSetting.WorldSize;
//        float noiseScale = worldSetting.NoiseScale;
//        float waterThreshold = worldSetting.WaterThreshold;
//        var worldRend = SystemAPI.GetSingleton<WorldPrefab>();
//        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
//        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
//        var chunkDataJob = new ChunkDataJob()
//        {
//            ChunkSize = chunkSize,
//            ChunkHeight = chunkHeight,
//            WorldSize = worldSize,
//            NoiseScale = noiseScale,
//            WaterThreshold = waterThreshold,
//            Prefab = worldRend.Prefab,
//            ECB = ecb,
//        };
//        chunkDataJob.Schedule();
//    }
//    [BurstCompile]
//    private partial struct ChunkDataJob : IJobEntity
//    {
//        public int ChunkSize;
//        public int ChunkHeight;
//        public int WorldSize;
//        public float NoiseScale;
//        public float WaterThreshold;
//        public Entity Prefab;
//        public EntityCommandBuffer ECB;
//        void Execute(DynamicBuffer<ChunkData> chunkDatas, ref LocalTransform transform)
//        {
//            GenerateBlock(chunkDatas, ChunkSize, ChunkHeight, NoiseScale, WaterThreshold, transform.Position);
//            int validBlockCount = 0;
//            foreach (var item in chunkDatas)
//            {
//                if (item.BlockId != BlockType.AIR)
//                {
//                    validBlockCount++;
//                }
//            }
//            var entities = new NativeArray<Entity>(validBlockCount, Allocator.Temp);
//            ECB.Instantiate(Prefab, entities);
//            for (int i = 0; i < chunkDatas.Length; i++)
//            {
//                if (chunkDatas.ElementAt(i).BlockId == BlockType.AIR)
//                {
//                    var blockPosition = ChunkHelper.GetBlockPositionFromIndex(ChunkSize, ChunkHeight,i);
//                    ECB.SetComponent<LocalTransform>(entities[i], LocalTransform.FromPosition(blockPosition));
//                }
//            }
//            entities.Dispose();
//        }
//    }

//    private static void GenerateBlock(DynamicBuffer<ChunkData> chunkDatas, int chunkSize, int chunkHeight, float noiseScale, float waterThreshold, float3 chunkPosition)
//    {
//        for (int x = 0; x < chunkSize; x++)
//        {
//            for (int z = 0; z < chunkSize; z++)
//            {
//                float noiseValue = Mathf.PerlinNoise((chunkPosition.x + x) * noiseScale,(chunkPosition.z + z) * noiseScale);
//                int groundPosition = Mathf.RoundToInt(noiseValue * chunkHeight);
//                for (int y = 0; y < chunkHeight; y++)
//                {
//                    BlockType blockID = BlockType.DIRT;
//                    if (y > groundPosition)
//                    {
//                        if (y < waterThreshold)
//                        {
//                            blockID = BlockType.WATER;
//                        }
//                        else
//                        {
//                            blockID = BlockType.AIR;
//                        }
//                    }
//                    else if (y == groundPosition)
//                    {
//                        blockID = BlockType.GRASS_DIRT;
//                    }
//                    ChunkHelper.SetBlock(chunkDatas, chunkSize, chunkHeight, new float3(x, y, z), blockID);
//                }
//            }
//        }
//    }

//    private static void BlockDebug()
//    {

//    }
//}
