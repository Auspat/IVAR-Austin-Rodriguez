using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollision : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject XR_Origin;
    public AudioSource audioSource;
    public AudioClip money;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Obstacle1Start")){
            Transform targetTransform = XR_Origin.transform;
            targetTransform.position = new Vector3(326, 4, 50);
            gameManager.ChangeScene("Obstacle_1");
        }
        if(collision.gameObject.CompareTag("Obstacle1Finish")){
            gameManager.ChangeScene("Controller Data");
        }
        if(collision.gameObject.CompareTag("Obstacle2Start")){
            Transform targetTransform = XR_Origin.transform;
            targetTransform.position = new Vector3(411, 15, -11);
            gameManager.ChangeScene("Obstacle_2");
        }
        if(collision.gameObject.CompareTag("Obstacle2Finish")){
            gameManager.ChangeScene("Controller Data");
        }
        if(collision.gameObject.CompareTag("Obstacle3Start")){
            Transform targetTransform = XR_Origin.transform;
            targetTransform.position = new Vector3(330, 3, -103);
            gameManager.ChangeScene("Obstacle_3");
        }
        if(collision.gameObject.CompareTag("Obstacle3Finish")){
            gameManager.ChangeScene("Controller Data");
        }

        if(collision.gameObject.CompareTag("coin1"))
        {
            gameManager.coin1++;
            audioSource.PlayOneShot(money);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("coin2"))
        {
            gameManager.coin2++;
            audioSource.PlayOneShot(money);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("coin3"))
        {
            gameManager.coin3++;
            audioSource.PlayOneShot(money);
            Destroy(collision.gameObject);
        }
        
    }
}
