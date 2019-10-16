using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
public class Test2 : MonoBehaviour
{
    EntityManager entityManager;

    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    NativeArray<Entity> entities;
    NativeArray<Rotation> rotations;
    NativeArray<Translation> translations;
    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.Active.EntityManager;
        EntityArchetype archetype = entityManager.CreateArchetype(typeof(Translation), typeof(Rotation), typeof(RenderMesh));
        entities = new NativeArray<Entity>(1, Allocator.Persistent);
        for (int i = 0; i < 1; i++) {
            entities[i] = entityManager.CreateEntity(archetype);
        }
    }

    private void OnDisable()
    {
        entities.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        rotations = new NativeArray<Rotation>(1, Allocator.TempJob);
        translations = new NativeArray<Translation>(1, Allocator.TempJob);

        for (int i = 0; i < 1; i++)
        {
            rotations[i] = entityManager.GetComponentData<Rotation>(entities[i]);
            translations[i] = entityManager.GetComponentData<Translation>(entities[i]);
        }

        float3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        RotationJob job = new RotationJob
        {
            target = mousePos,
            rotations = rotations,
            translations = translations
        };

        JobHandle jobHandle = job.Schedule(1, 1);
        jobHandle.Complete();

        for (int i = 0; i < 1; i++)
        {
            entityManager.SetComponentData(entities[i], translations[i]);
            entityManager.SetComponentData(entities[i], rotations[i]);
        }

        rotations.Dispose();
        translations.Dispose();
    }
}


public struct RotationJob : IJobParallelFor
{
    public float3 target;
    public NativeArray<Rotation> rotations;
    public NativeArray<Translation> translations;

    public void Execute(int index)
    {
        var forward = target - translations[index].Value;

        //var rotation = Quaternion.LookRotation(forward);

        rotations[index] = new Rotation
        {
            Value = new quaternion(0, 0, math.atan2(forward.y, forward.x),rotations[index].Value.value.w)
        };
    }
}
