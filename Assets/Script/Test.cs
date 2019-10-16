using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;

public class Test : MonoBehaviour
{
    public int entitiesCount = 10;

    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    public float maxSpeed;
    public float maxVelocity;
    public float maxForce;
    public float mass;
    public float fleeRadius;

    NativeArray<Entity> entities;
    EntityManager entityManager;

    NativeArray<SteeringAgentComponent> agents;
    NativeArray<Translation> translations;
    NativeArray<Rotation> rotations;

    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.Active.EntityManager;
        EntityArchetype archetype = entityManager.CreateArchetype(
            typeof(SteeringAgentComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Scale),
            typeof(Rotation)
        );

        entities = new NativeArray<Entity>(entitiesCount, Allocator.Temp);

  

        for (int i = 0; i < entitiesCount; i++) {
            entities[i] = entityManager.CreateEntity(archetype);
            entityManager.SetComponentData(entities[i], new SteeringAgentComponent
            {
                maxSpeed = maxSpeed,
                maxVelocity = maxVelocity,
                maxForce = maxForce,
                mass = mass,
                time = 0,
                fleeRadius = fleeRadius
            });
            entityManager.SetComponentData(entities[i], new Translation { Value = new float3(UnityEngine.Random.Range(-5f,5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f)) });
            entityManager.SetComponentData(entities[i], new Scale { Value = 0.2f });
            entityManager.SetSharedComponentData(entities[i], new RenderMesh
            {
                mesh = mesh,
                material = material
            });           
        }
        entities.Dispose();
    }

    private void Update()
    {
        //    float dt = Time.deltaTime;

        //    agents = new NativeArray<SteeringAgentComponent>(entitiesCount, Allocator.TempJob);
        //    translations = new NativeArray<Translation>(entitiesCount, Allocator.TempJob);
        //    rotations = new NativeArray<Rotation>(entitiesCount, Allocator.TempJob);

        //    for (int i = 0; i < entitiesCount; i++) {
        //        agents[i] = entityManager.GetComponentData<SteeringAgentComponent>(entities[i]);
        //        translations[i] = entityManager.GetComponentData<Translation>(entities[i]);
        //        rotations[i] = entityManager.GetComponentData<Rotation>(entities[i]);
        //    }
        //    //float3 fleeTarget = float3.zero;
        //    //float3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    float3 fleeTarget = float3.zero;
        //    float3 mousePos = float3.zero;

        //    //if (Input.GetMouseButton(0)) {
        //    //    fleeTarget = mousePos;
        //    //}

        //    mousePos.z = 0;
        //    SteeringJob job = new SteeringJob
        //    {
        //        agents = agents,
        //        translations = translations,
        //        rotations = rotations,
        //        dt = dt,
        //        target = mousePos,
        //        fleeTarget = fleeTarget,
        //        fleeRadius = fleeRadius
        //    };

        //    JobHandle jobHandle = job.Schedule(entitiesCount, entitiesCount/10);
        //    jobHandle.Complete();
        //    for (int i = 0; i < entitiesCount; i++) {
        //        entityManager.SetComponentData(entities[i], translations[i]);
        //        entityManager.SetComponentData(entities[i], agents[i]);
        //        entityManager.SetComponentData(entities[i], rotations[i]);
        //    }

        //    agents.Dispose();
        //    translations.Dispose();
        //    rotations.Dispose();
    }
}
