using System;
using Unity.Entities;

[Serializable]
public struct WorldPrefab : IComponentData
{
    public Entity Prefab;
}
