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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 CameraPos = Camera.transform.position;
        Vector2 TargetPos = CameraTarget.transform.position;
        float SpeedSnap = 3f*Time.deltaTime;
        Camera.transform.position = Vector2.Lerp(CameraPos, TargetPos, SpeedSnap);
    }
}
