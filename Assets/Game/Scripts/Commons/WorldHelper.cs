using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public struct WorldHelper
{
    public static float3 ChunkPositionFromBlockCoords(int chunkSize, int chunkHeight, float3 worldBlockPosition)
    {
        return new float3
        {
            x = Mathf.FloorToInt(worldBlockPosition.x / chunkSize) * chunkSize,
            y = Mathf.FloorToInt(worldBlockPosition.y / chunkHeight) * chunkHeight,
            z = Mathf.FloorToInt(worldBlockPosition.z / chunkSize) * chunkSize
        };
    }

    public static NativeArray<float3> GetChunkPositionAroundPlayer(int worldSize,int chunkSize, int chunkHeight, float3 playerPosition)
    {
        /*Dinstance from x, z to player can see*/
        int startX = (int)(playerPosition.x - worldSize * chunkSize);
        int endX = (int)(playerPosition.x + worldSize * chunkSize);
        int startZ = (int)(playerPosition.z - worldSize * chunkSize);
        int endZ = (int)(playerPosition.z + worldSize * chunkSize);
        /*Calculate number of chunk around player*/
        int chunkCountX = (endX - startX) / chunkSize + 1;
        int chunkCountZ = (endZ - startZ) / chunkSize + 1;
        int totalChunk = chunkCountX * chunkCountZ;

        NativeArray<float3> chunkPositions = new NativeArray<float3>(totalChunk, Allocator.Temp);

        int chunkIndex = 0;
        /*Calculate chunk position*/
        for (int x = startX; x <= endX; x += chunkSize)
        {
            for (int z = startZ; z <= endZ; z += chunkSize)
            {
                float3 chunkPosition = ChunkPositionFromBlockCoords(chunkSize, chunkHeight, new float3(x, 0, z));

                chunkPositions[chunkIndex] = chunkPosition;

                chunkIndex++;
            }
        }

        return chunkPositions;
    }


}
