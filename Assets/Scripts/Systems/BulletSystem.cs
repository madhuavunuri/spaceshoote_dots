using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using Unity.Physics.Systems;
public class BulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        float deltaTime = Time.DeltaTime;
        var jobHandle = Entities
            .WithName("BulletSystem")
            .ForEach((ref PhysicsVelocity physics,
                ref Translation position,
                ref Rotation rotation, ref BulletData bulletData) =>
            {

                physics.Angular = float3.zero;
                physics.Linear += bulletData.speed * math.forward(rotation.Value);
                //  physics.Angular = float3.zero;
                //  physics.Linear += deltaTime * bulletData.speed * math.forward(rotation.Value);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
   
  
}
