using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class BulletCollisionSystem : JobComponentSystem
{
    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle  = new CollisionEvent_Job
        {
            bulletDataGroup = GetComponentDataFromEntity<BulletData>(),
            asteroidGroup = GetComponentDataFromEntity<AsteroidData>()


        }.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps);
        jobHandle.Complete();
        return jobHandle;

    }

    private struct CollisionEvent_Job : ICollisionEventsJob
    {
        public ComponentDataFromEntity<BulletData> bulletDataGroup;
        public ComponentDataFromEntity<AsteroidData> asteroidGroup;

  
        public void Execute(CollisionEvent collideEvent)
        {
            if (bulletDataGroup.HasComponent(collideEvent.EntityA))
            {
                if (asteroidGroup.HasComponent(collideEvent.EntityB))
                {
                    AsteroidData asteroids = asteroidGroup[collideEvent.EntityB];
                    asteroids.isDestroy = true;
                    asteroidGroup[collideEvent.EntityB] = asteroids;

                    BulletData bullets = bulletDataGroup[collideEvent.EntityA];
                    bullets.isDestroy = true;
                    bulletDataGroup[collideEvent.EntityA] = bullets;
                }
            }

            if (bulletDataGroup.HasComponent(collideEvent.EntityB))
            {
                if (asteroidGroup.HasComponent(collideEvent.EntityA))
                {
                    AsteroidData asteroids = asteroidGroup[collideEvent.EntityA];
                    asteroids.isDestroy = true;
                    asteroidGroup[collideEvent.EntityA] = asteroids;


                    BulletData bullets = bulletDataGroup[collideEvent.EntityB];
                    bullets.isDestroy = true;
                    bulletDataGroup[collideEvent.EntityB] = bullets;

                }
            }
        }
    }

}
