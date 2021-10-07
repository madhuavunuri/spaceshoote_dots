using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class AsteroidSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        var jobHandle = Entities
            .WithName("AsteroidSystem")
            .ForEach((ref PhysicsVelocity physics,
                ref Translation position,
                ref Rotation rotation, ref AsteroidData asteroidData) =>
            {

                // physics.Angular = float3.zero;
                // physics.Linear -= asteroidData.speed * math.forward(rotation.Value) ;
                position.Value -= asteroidData.speed * deltaTime * math.forward(rotation.Value);
               
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
