using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputData))]
public class VRObjectRotation : MonoBehaviour
{
    private InputData _inputData;
    private bool Yrotating;
    private bool Zrotating;
    public GameObject childObject;

    public float tiltSpeed = 50.0f; // Adjust the tilt speed as needed

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        Yrotating = false;
        Zrotating = false;
    }

    void Update()
    {
        // Input checks for headset tilts
        if (_inputData._HMD.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion headsetRotation))
        {
            // Convert the quaternion to Euler angles in the headset's local space
            Vector3 localEulerAngles = headsetRotation.eulerAngles;

            // Check the tilt around the local x-axis (forward/backward)
            if (localEulerAngles.x > 180)
                localEulerAngles.x -= 360;

            // Check the tilt around the local z-axis (left/right)
            if (localEulerAngles.z > 180)
                localEulerAngles.z -= 360;
            
            // Check the tilt around the local z-axis (left/right)
            if (localEulerAngles.y > 180)
                localEulerAngles.y -= 360;

            //forward and backward motion
            if (localEulerAngles.x > 15.0 && !Yrotating && !Zrotating)
            {
                // Rotate the object forward
                childObject.transform.Rotate(new Vector3(1, 0, 0), tiltSpeed * Time.deltaTime, Space.World);
            }
            else if (localEulerAngles.x < -15.0 && !Yrotating && !Zrotating)
            {
                // Rotate the object backward
                childObject.transform.Rotate(new Vector3(-1, 0, 0), tiltSpeed * Time.deltaTime, Space.World);
            }
            else if (localEulerAngles.z > 15.0 && !Yrotating)
            {
                Zrotating = true;
                // Rotate the object puppy left
                childObject.transform.Rotate(new Vector3(0, 0, 1), tiltSpeed * Time.deltaTime, Space.World);
            }
            else if (localEulerAngles.z < -15.0 && !Yrotating)
            {
                Zrotating = true;
                // Rotate the object puppy right 
                childObject.transform.Rotate(new Vector3(0, 0, -1), tiltSpeed * Time.deltaTime, Space.World);
            }
            else if (localEulerAngles.y > 15.0)
            {
                Yrotating = true;
                // Rotate the object shake left
                childObject.transform.Rotate(new Vector3(0, 1, 0), tiltSpeed * Time.deltaTime, Space.World);
            }
            else if (localEulerAngles.y < -15.0)
            {
                Yrotating = true;
                // Rotate the object shake right
                childObject.transform.Rotate(new Vector3(0, -1, 0), tiltSpeed * Time.deltaTime, Space.World);
            }else{
                Yrotating = false;
                Zrotating = false;
            }

        }
        
    }
}
