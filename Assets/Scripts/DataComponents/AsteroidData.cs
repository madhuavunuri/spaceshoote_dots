using Unity.Entities;
[GenerateAuthoringComponent]
public struct AsteroidData : IComponentData
{
    public float speed;
    public bool isDestroy;
}
