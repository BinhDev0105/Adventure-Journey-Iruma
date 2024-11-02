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
        state.RequireForUpdate<WorldSettingComponent>();
        state.RequireForUpdate<WorldRendererComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var worldSetting = SystemAPI.GetSingleton<WorldSettingComponent>();
        var worldRenderer = SystemAPI.GetSingleton<WorldRendererComponent>();

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
