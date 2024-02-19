using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

[RequireComponent(typeof(InputData))]
public class DisplayInputData : MonoBehaviour
{
    public TextMeshProUGUI leftScoreDisplay;
    public TextMeshProUGUI rightScoreDisplay;

    private InputData _inputData;
    //private float _leftMaxScore = 0f;
    //private float _rightMaxScore = 0f;
    private Vector3 _headsetRotation;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {
            float zLVelocity = leftVelocity.z;

            if (Mathf.Abs(zLVelocity) > 0.05f){
                leftScoreDisplay.text = "moving";
            }else{
                leftScoreDisplay.text = "not moving";
            }
            //_leftMaxScore = leftVelocity.y;
            //leftScoreDisplay.text = _leftMaxScore.ToString("F2");
            //Debug.Log("left controller velocity is: " + leftVelocity);
        }
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {
            float zRVelocity = rightVelocity.z;

            if (Mathf.Abs(zRVelocity) > 0.05f){
                rightScoreDisplay.text = "moving";
            }else{
                rightScoreDisplay.text = "not moving";
            }
            //_leftMaxScore = leftVelocity.y;
            //leftScoreDisplay.text = _leftMaxScore.ToString("F2");
            //Debug.Log("left controller velocity is: " + leftVelocity);
        }
        //if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        //{
            //_rightMaxScore = Mathf.Max(rightVelocity.magnitude, _rightMaxScore);
            //rightScoreDisplay.text = _rightMaxScore.ToString("F2");
        //}
        if (_inputData._HMD.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion headsetRotation))
        {
            // Convert the quaternion to Euler angles in headset's local space
            Vector3 localEulerAngles = headsetRotation.eulerAngles;

            //Check the tilt around the local x-axis (forward/backward)
            if (localEulerAngles.x > 180)
                localEulerAngles.x -= 360;

            //leftScoreDisplay.text = localEulerAngles.x.ToString("F2");
            if(localEulerAngles.x > 5.0){
                //rightScoreDisplay.text = "forward ";
            }else if (localEulerAngles.x < -5.0){
                //rightScoreDisplay.text = "backward ";
            }else{
                //rightScoreDisplay.text = "no x";
            }

            //Check the tilt around the local z-axis (left/right)
            if (localEulerAngles.z > 180)
                localEulerAngles.z -= 360;

            if(localEulerAngles.z > 5.0){
                //rightScoreDisplay.text += "left";
            }else if (localEulerAngles.z < -5.0){
                //rightScoreDisplay.text += "right";
            }else{
                //rightScoreDisplay.text += "no z";
            }
        }
    }
}
