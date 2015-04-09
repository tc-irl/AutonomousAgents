using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public GameObject MainCamera, TPCamera, FPCamera;

	// Use this for initialization
	void Start () 
    {
        TPCamera.SetActive(true);
        MainCamera.SetActive(false);
        FPCamera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            TPCamera.SetActive(true);
            MainCamera.SetActive(false);
            FPCamera.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            TPCamera.SetActive(false);
            MainCamera.SetActive(false);
            FPCamera.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            TPCamera.SetActive(false);
            MainCamera.SetActive(true);
            FPCamera.SetActive(false);
        }
	}
}
