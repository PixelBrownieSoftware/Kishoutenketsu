using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class S_BMaths
{
    public static float Blend(float x, float y, float weight)
    {
        return x + (y - x) * (1.0f + weight) / 2;
    }

    public static float BSum(float input, float change)
    {
        float result = 0f;

        if (change >= 0)
        {
            result = Blend(input, 1.0f, change - 1.0f);
        }
        else
        {
            result = Blend(input, -1.0f, change + 1.0f);
        }

        return result;
    }


}
