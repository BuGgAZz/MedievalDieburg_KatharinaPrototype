using UnityEngine;
using System.Collections;

public static class MathF 
{

	public static float Mapf(float baseValue,float a1, float a2, float b1, float b2)
	{
		float targetValue;

		targetValue = b1 + ((baseValue - a1) * (b2 - b1) / (a2 - a1));

		return targetValue;
	}
}
