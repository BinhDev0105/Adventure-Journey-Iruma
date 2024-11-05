using Unity.Entities;
using UnityEngine;

public class WorldPrefabAuthoring : MonoBehaviour
{
    public GameObject Prefab;

    class WorldPrefabBaker : Baker<WorldPrefabAuthoring>
    {
        public override void Bake(WorldPrefabAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new WorldPrefab { Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)});
        }
    }
}
