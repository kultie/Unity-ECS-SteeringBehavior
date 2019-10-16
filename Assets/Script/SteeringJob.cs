using System;
using Unity.Collections;
using UnityEngine;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public class SteeringJob: JobComponentSystem
{
    private struct Job : IJobForEach<SteeringAgentComponent, Translation, Rotation> {

        public float3 target;
        public float3 fleeTarget;
        public float dt;

        public void Execute(ref SteeringAgentComponent agent, ref Translation translation, ref Rotation rotation)
        {
            float3 force = applySteeringForce(agent, translation, target, fleeTarget);

            float3 agentVelocity = MathUtils.clampMagnitude(agent.currentVelocity + force, agent.maxSpeed);
            agentVelocity.z = 0;
            agent.currentVelocity = agentVelocity;
            translation.Value = translation.Value + agent.currentVelocity * dt;
            rotation.Value = quaternion.AxisAngle(new float3(0, 0, 1), math.atan2(agentVelocity.y, agentVelocity.x) - 90 * Mathf.Deg2Rad);
        }

        float3 applySteeringForce(SteeringAgentComponent agent, Translation translation, float3 target, float3 fleeTarget)
        {
            float3 steeringForce = seek(target, translation.Value, agent.maxVelocity, agent.currentVelocity) + flee(fleeTarget, translation.Value, agent.maxVelocity, agent.currentVelocity,agent.fleeRadius);

            steeringForce = MathUtils.clampMagnitude(steeringForce, agent.maxForce);
            steeringForce = steeringForce / agent.mass;
            return steeringForce;
        }

        float3 seek(float3 target, float3 currentPosition, float maxVelocity, float3 currentVelocity)
        {
            float3 desiredVelocity = math.normalize(target - currentPosition) * maxVelocity;
            float3 steering = desiredVelocity - currentVelocity;
            return steering;
        }

        float3 flee(float3 target, float3 currentPosition, float maxVelocity, float3 currentVelocity, float fleeRadius)
        {
            if (MathUtils.sqrMagnitude(target - currentPosition) <= fleeRadius * fleeRadius)
            {
                float3 desired_velocity = math.normalize(target - currentPosition) * maxVelocity * 100;
                float3 steering = -desired_velocity - currentVelocity;
                return steering;
            }
            return float3.zero;
        }


    }    

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float3 fleeTarget = float3.zero;
        float3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      

        float dt = Time.deltaTime;
        Job job = new Job
        {
            dt = dt,
            target = mousePos,
            fleeTarget = mousePos
        };
        return job.Schedule(this, inputDeps);
    }
}
