using Unity.Entities;
using Unity.Mathematics;

public struct SteeringAgentComponent : IComponentData
{
    public float maxSpeed;
    public float maxVelocity;
    public float maxForce;
    public float mass;
    public float3 currentVelocity;
    public float fleeRadius;

    public float time;
}
