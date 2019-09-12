using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform tfTarget;
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, tfTarget.position.x, 3 * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
