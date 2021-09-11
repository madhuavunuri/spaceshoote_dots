using Unity.Entities;

[GenerateAuthoringComponent]
public struct PlayerMoveData : IComponentData
{
    public int horizontal_dir;
    public int vertical_dir;

    public float speed;
    public float rotation;
}
