using Unity.Entities;
using System;

[Serializable]
public struct ChunkData : IBufferElementData
{
    public int BlockId;
}
