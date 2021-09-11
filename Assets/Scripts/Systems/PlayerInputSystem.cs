using Unity.Jobs;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
[AlwaysSynchronizeSystem]

public class PlayerInputSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
        var commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer();

        Entities.ForEach((ref Translation position,ref PlayerMoveData moveData, in PlayerInputData inputData) =>
        {

           
            moveData.horizontal_dir = 0;
            moveData.vertical_dir = 0;

            moveData.horizontal_dir += Input.GetKey(inputData.rightKey) ? 1 : 0;
            moveData.horizontal_dir -= Input.GetKey(inputData.leftKey) ? 1 : 0;

            moveData.vertical_dir -= Input.GetKey(inputData.backwardKey) ? 1 : 0;
            moveData.vertical_dir += Input.GetKey(inputData.forwardKey) ? 1 : 0;
            if (Input.GetKeyUp(inputData.shootingKey))
            {
                var bullet = commandBuffer.Instantiate(ECSManager.playerbullet);
                float3 pos = new float3(0, 0, 0);
                commandBuffer.SetComponent<Translation>(bullet, new Translation { Value = position.Value });
                //ECSManager.manager.SetComponentData<PhysicsVelocity>(bullet, new PhysicsVelocity { Linear = float3.zero });
                //var startPos = player.transform.position + UnityEngine.Random.insideUnitSphere * 2;
                //manager.SetComponentData(instance, new Translation { Value = startPos });
                // manager.SetComponentData(instance, new Rotation { Value = player.transform.rotation });
            }


        }).WithoutBurst().Run();

       

        return default;
	}

}
