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
       
       
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref Translation position, ref AsteroidData asteroid) =>
            {
                
                if (asteroid.isDestroy)
                {
                    var breakasteroid = ECSManager.manager.Instantiate(ECSManager.asteroidBreak);
                    
                    ECSManager.manager.SetComponentData<Translation>(breakasteroid, new Translation { Value = position.Value });

                    EntityManager.DestroyEntity(entity);
                }

            })
            .Run();


        float deltaTime = Time.DeltaTime;
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref AliveTimeData aliveTimeData) =>
            {
                aliveTimeData.timeLeft -= deltaTime;
                if (aliveTimeData.timeLeft <= 0)
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
