using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour
{
    public static float WindForce = 0.1f;
    public static float WindDirection = 1;

    public static void StartWind()
    {
        WindDirection = Random.Range(0, 2);
        if (WindDirection <= 0) WindDirection = -1;

        WindForce = Random.Range(0.0000f, 0.0150f);
    }
}
