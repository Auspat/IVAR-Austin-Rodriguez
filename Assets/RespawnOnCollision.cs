using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnOnCollision : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip crash;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Wall")){
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            audioSource.PlayOneShot(crash);
            
            if(SceneManager.GetActiveScene().name == "Obstacle_1"){
                gameManager.mistakes1++;
            }
            else if(SceneManager.GetActiveScene().name == "Obstacle_2"){
                gameManager.mistakes2++;
            }
            else if(SceneManager.GetActiveScene().name == "Obstacle_3"){
                gameManager.mistakes3++;
            }
        }
    }
}
