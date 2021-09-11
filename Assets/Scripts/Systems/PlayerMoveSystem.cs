using Unity.Jobs;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class PlayerMoveSystem : JobComponentSystem
{
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float deltaTime = Time.DeltaTime;
		float xBound = 7;
		float zBound = 9;


		Entities.ForEach((ref Translation trans, in PlayerMoveData data) =>
		{
			//trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);

			trans.Value.x = math.clamp(trans.Value.x + (data.speed * data.horizontal_dir * deltaTime), -xBound, xBound);
			trans.Value.z = math.clamp(trans.Value.z + (data.speed * data.vertical_dir * deltaTime), -zBound, zBound);
		}).Run();

		return default;
	}
	
}
