using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AceHelper
{


    public static int GetRandomNumberFromArray(GameObject[] objects)
    {

        return UnityEngine.Random.Range(0, objects.Length);
    }

    public static int GetRandomNumberFromArray(Transform[] objects)
    {

        return UnityEngine.Random.Range(0, objects.Length);
    }

    public static int GetRandomNumberFromArray(List<Transform> objects)
    {

        return UnityEngine.Random.Range(0, objects.Count);
    }


    public static void randomizeList(List<GameObject> AllObjects)
    {
        for (int i = 0; i < AllObjects.Count; i++)
        {
            GameObject temp = AllObjects[i];
            int randomIndex = UnityEngine.Random.Range(i, AllObjects.Count);
            AllObjects[i] = AllObjects[randomIndex];
            AllObjects[randomIndex] = temp;
        }
    }

    public static Vector3 GetRandomPointInBoxCollider(BoxCollider box)
    {
        Vector3 bLocalScale = box.transform.localScale;
        Vector3 boxPosition = box.transform.localPosition;
        boxPosition += new Vector3(bLocalScale.x * box.center.x, bLocalScale.y * box.center.y, bLocalScale.z * box.center.z);

        Vector3 dimensions = new Vector3(bLocalScale.x * box.size.x,
                                         bLocalScale.y * box.size.y,
                                         bLocalScale.z * box.size.z);

        Vector3 newPos = new Vector3(UnityEngine.Random.Range(boxPosition.x - (dimensions.x / 2), boxPosition.x + (dimensions.x / 2)),
                                      UnityEngine.Random.Range(boxPosition.y - (dimensions.y / 2), boxPosition.y + (dimensions.y / 2)),
                                      UnityEngine.Random.Range(boxPosition.z - (dimensions.z / 2), boxPosition.z + (dimensions.z / 2)));
        return newPos;
    }

    //Example usage
    //StartCoroutine ( AceHelper.waitThenCallback(2,() => {
    //	Ace_InGameUiController.Static.GameEnd();
    //} ) );
    //

    public static IEnumerator waitThenCallback(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float RemapClamped(this float value, float from1, float to1, float from2, float to2)
    {
        var val = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        if (val > to2) val = to2;

        return val;
    }


}