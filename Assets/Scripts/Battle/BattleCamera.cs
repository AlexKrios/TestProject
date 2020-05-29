using System.Collections;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    public void StartCamera() 
    {
        Transform endPoint = GameObject.Find("EndCameraPoint").GetComponent<Transform>();
        Transform targetObject = GameObject.Find("PostProcessing").GetComponent<Transform>();

        StartCoroutine(MoveToPosition(transform, endPoint, targetObject, 120));
    }

    private IEnumerator MoveToPosition(Transform currentObject, Transform endPoint, Transform targetObject, float time)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(currentObject.position, endPoint.position, t);
            transform.LookAt(targetObject);
            yield return null;
        }
    }
}
