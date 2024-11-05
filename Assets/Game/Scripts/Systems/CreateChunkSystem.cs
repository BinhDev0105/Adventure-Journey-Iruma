using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;

public partial struct CreateChunkSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<WorldSetting>();
        state.RequireForUpdate<WorldPrefab>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var worldSetting = SystemAPI.GetSingleton<WorldSetting>();
        var worldRenderer = SystemAPI.GetSingleton<WorldPrefab>();

        var chunks = WorldHelper.GetChunkPositionAroundPlayer(worldSetting.WorldSize, worldSetting.ChunkSize, worldSetting.ChunkHeight, new float3(0, 0, 0));

        var instances = new NativeArray<Entity>(chunks.Length, Allocator.Temp);

        state.EntityManager.Instantiate(worldRenderer.Prefab,instances);

        for (int index = 0; index < instances.Length; index++)
        {
            state.EntityManager.SetName(instances[index], $"Chunk {index}");
            state.EntityManager.SetComponentData(instances[index], LocalTransform.FromPosition(chunks[index]));
            state.EntityManager.AddBuffer<ChunkData>(instances[index]);
        }

        chunks.Dispose();
        instances.Dispose();
    }
}
