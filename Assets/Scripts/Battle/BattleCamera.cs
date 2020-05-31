using System.Collections;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    public void StartCamera() 
    {
        Transform endPoint = GameObject.Find("EndCameraPoint").GetComponent<Transform>();
        Transform targetObject = GameObject.Find("PostProcessing").GetComponent<Transform>();

        var timer = MoveToPosition(transform, endPoint, targetObject, 60f);
        StartCoroutine(timer);
    }

    private IEnumerator MoveToPosition(Transform currentObject, Transform endPoint, Transform targetObject, float time)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Slerp(currentObject.position, endPoint.position, t);
            transform.LookAt(targetObject);
            yield return null;
        }
    }
}
