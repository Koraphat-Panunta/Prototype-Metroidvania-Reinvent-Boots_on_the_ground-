using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Camera;
    public GameObject CameraTarget;
    void Start()
    {
        Camera = GetComponent<Camera>();
        Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, -100);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraPos = Camera.transform.position;
        Vector3 TargetPos = new Vector3(CameraTarget.transform.position.x, CameraTarget.transform.position.y, -100);
        float SpeedSnap = 3f*Time.deltaTime;
        Camera.transform.position = Vector3.Lerp(CameraPos, TargetPos, SpeedSnap);
    }
}
