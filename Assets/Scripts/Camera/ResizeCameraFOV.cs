using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCameraFOV : MonoBehaviour {

    public Camera camera;
    public float reference_height = 1080;
    public float reference_width = 1920;
    public float reference_fov = 10;


	void Start () {
        ResizeFov();
    }
	
    void ResizeFov()
    {
        float device_height = Screen.height;
        float device_width = Screen.width;

        float c = reference_fov * (reference_height / reference_width) * (device_width / device_height);

        float c1 = reference_fov - (c);
        float final_cal = reference_fov - (-c1);
     
        camera.fieldOfView =final_cal;
    }
}
