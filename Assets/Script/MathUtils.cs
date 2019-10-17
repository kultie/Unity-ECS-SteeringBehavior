using Unity.Mathematics;
public static class MathUtils
{

    public static float3 clampMagnitude(float3 src, float maxLength)
    {
        float sqrMag = sqrMagnitude(src);

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

    public static float sqrDistance(float3 a, float3 b) {
        return sqrMagnitude(a - b);
    }

    public static float distance(float3 a, float3 b) {
        return math.sqrt(sqrDistance(a, b));
    }
}
