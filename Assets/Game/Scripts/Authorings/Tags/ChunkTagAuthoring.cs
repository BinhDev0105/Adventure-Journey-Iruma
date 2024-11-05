using UnityEngine;
using Unity.Entities;

public class ChunkTagAuthoring : MonoBehaviour
{
    class ChunkTagBaker : Baker<ChunkTagAuthoring>
    {
        public override void Bake(ChunkTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ChunkTag { });
        }
    }
}
