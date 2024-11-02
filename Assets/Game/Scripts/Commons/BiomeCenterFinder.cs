using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public struct BiomeCenterFinder
{
    public static NativeArray<float2> neighboursDirections;

    public static NativeArray<float3> CalculateBiomeCenter(float3 playerPosition, int worldSize, int ChunkSize)
    {
        int biomeLength = worldSize * ChunkSize;

        float3 origin = new float3 ( Mathf.RoundToInt(playerPosition.x / biomeLength) * biomeLength,
                                     0,
                                     Mathf.RoundToInt(playerPosition.z / biomeLength) * biomeLength);

        NativeHashSet<float3> biomeTemp = new NativeHashSet<float3>(0, Allocator.Persistent);
        biomeTemp.Add (origin);

        foreach (float2 offsetXZ in InitializeNeighboursDirections())
        {
            float3 biomePoint1 = new float3 (origin.x + offsetXZ.x * biomeLength, 0, origin.z + offsetXZ.y * biomeLength);
            float3 biomePoint2 = new float3(origin.x + offsetXZ.x * biomeLength, 0, origin.z + offsetXZ.y * 2 * biomeLength);
            float3 biomePoint3 = new float3(origin.x + offsetXZ.x * 2 * biomeLength, 0, origin.z + offsetXZ.y * biomeLength);
            float3 biomePoint4 = new float3(origin.x + offsetXZ.x * 2 * biomeLength, 0, origin.z + offsetXZ.y * 2 * biomeLength);

            biomeTemp.Add (biomePoint1);
            biomeTemp.Add (biomePoint2);
            biomeTemp.Add (biomePoint3);
            biomeTemp.Add (biomePoint4);
        }

        NativeArray<float3> biomeCenter = biomeTemp.ToNativeArray(Allocator.Temp);
        biomeTemp.Dispose();
        neighboursDirections.Dispose();
        return biomeCenter;
    }

    public static NativeArray<float2> InitializeNeighboursDirections()
    {
        neighboursDirections = new NativeArray<float2>(8, Allocator.Temp);
        neighboursDirections[0] = new float2 (0, 1);
        neighboursDirections[1] = new float2 (1, 1);
        neighboursDirections[2] = new float2 (1, 0);
        neighboursDirections[3] = new float2 (1, -1);
        neighboursDirections[4] = new float2 (0, -1);
        neighboursDirections[5] = new float2 (-1, -1);
        neighboursDirections[6] = new float2 (-1, 0);
        neighboursDirections[7] = new float2 (-1, 1);

        return neighboursDirections;
    }
}
