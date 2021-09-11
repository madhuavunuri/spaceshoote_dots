using Unity.Entities;
using UnityEngine;
[GenerateAuthoringComponent]
public class PlayerInputData : IComponentData
{
    public KeyCode leftKey;
    public KeyCode rightKey;

    public KeyCode forwardKey;
    public KeyCode backwardKey;

    public KeyCode shootingKey;

}
