using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using Unity.Physics.Systems;

public class DestroySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref AliveTimeData aliveTimeData,ref AsteroidData asteroid) =>
            {
                aliveTimeData.timeLeft -= deltaTime;
                if (aliveTimeData.timeLeft <= 0)
                {
                    EntityManager.DestroyEntity(entity);
                }
                if(asteroid.isDestroy)
                {
                    EntityManager.DestroyEntity(entity);
                }
                
            })
            .Run();

        Entities.WithoutBurst().WithStructuralChanges()
      .ForEach((Entity entity, ref BulletData bullet) =>
      {
         
          if (bullet.isDestroy)
          {
              EntityManager.DestroyEntity(entity);
          }
      })
      .Run();

        return inputDeps;
    }
}
