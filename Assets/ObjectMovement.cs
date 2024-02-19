using UnityEngine;
using UnityEngine.XR;

public class ObjectMovement : MonoBehaviour
{
    public XRNode controllerNode = XRNode.RightHand;
    public float movementSpeed = 3f;
    public float forwardDistance = 1f; // Adjust this value based on where the controller is considered "forward"

    private bool isMoving = false;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = Camera.main.transform; // Assuming the camera is the player's head
    }

    void Update()
    {
        // Check if the trigger on the right controller is pressed
        if (InputDevices.GetDeviceAtXRNode(controllerNode).TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.5f){
                isMoving = true;
            }else{
                isMoving = false;
            }
        }

        // Translate the object forward or backward based on the trigger state
        if (isMoving)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.World);
        }
    }
}
