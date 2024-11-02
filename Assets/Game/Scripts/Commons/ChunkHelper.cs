using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct ChunkHelper
{
    private static float3 GetPositionFromIndex(int chunkSize, int chunkHeight, int index)
    {
        int x = index % chunkSize;
        int y = (index / chunkSize) % chunkHeight;
        int z = index / (chunkSize * chunkHeight);
        return new float3 (x, y, z);
    }

    private static int GetIndexFromPosition(int chunkSize, int chunkHeight, int x, int y, int z)
    {
        return x + chunkSize * y + chunkSize * chunkHeight * z;
    }

    private static bool InRangeSize(int chunkSize, int axist)
    {
        if (axist < 0 || axist >= chunkSize)
        {
            return false;
        }
        return true;
    }

    private static bool InRangeHeight(int chunkHeight, int y)
    {
        if (y < 0 || y >= chunkHeight)
        {
            return false;
        }
        return true;
    }

    public static void SetBlock(DynamicBuffer<ChunkData> chunkDatas, int chunkSize, int chunkHeight, float3 localPosition, int blockId)
    {
        if (InRangeSize(chunkSize, (int)localPosition.x) &&
            InRangeHeight(chunkHeight, (int)localPosition.y) &&
            InRangeSize(chunkSize, (int)localPosition.z))
        {
            int index = GetIndexFromPosition(chunkSize, chunkHeight, (int)localPosition.x, (int)localPosition.y, (int)localPosition.z);
            chunkDatas.Add(new ChunkData { BlockId = blockId});
        }
    }
}
