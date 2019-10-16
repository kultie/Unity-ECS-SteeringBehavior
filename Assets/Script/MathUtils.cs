using Unity.Mathematics;
public static class MathUtils
{

    public static float3 clampMagnitude(float3 src, float maxLength)
    {
        float sqrMag = src.x * src.x + src.y * src.y + src.z + src.z;
        if (sqrMag > maxLength * maxLength)
        {
            float mag = math.sqrt(sqrMag);

            float normalized_x = src.x / mag;
            float normalized_y = src.y / mag;
            float normalized_z = src.z / mag;
            return new float3(normalized_x * maxLength, normalized_y * maxLength, normalized_z * maxLength);
        }
        return src;
    }

    public static float sqrMagnitude(float3 src) {
        return src.x * src.x + src.y * src.y + src.z + src.z;
    }
}
