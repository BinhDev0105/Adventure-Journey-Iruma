using System;
using Unity.Entities;

[Serializable]
public struct WorldRendererComponent : IComponentData
{
    public Entity Prefab;
    public int None;
}
