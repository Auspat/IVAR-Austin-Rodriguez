using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject XR_Origin;
    public TextMeshProUGUI UI_End;
    public TextMeshProUGUI Track1;
    public TextMeshProUGUI Obj1;
    public TextMeshProUGUI Track2;
    public TextMeshProUGUI Obj2;
    public TextMeshProUGUI Track3;
    public TextMeshProUGUI Obj3;
    public GameObject Data;

    public int spawn;
    public bool dead;
    public bool respawn;
    public int coin1;
    public int coin2;
    public int coin3;
    public int mistakes1;
    public int mistakes2;
    public int mistakes3;
    private float gameTime;

    public float startTime;
    public float trackTime1;
    public float trackTime2;
    public float trackTime3;
    public float objTime1;
    public float objTime2;
    public float objTime3;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawn = 0;
        Data.SetActive(true);
        startTime = GetGameTime();
    }

    // Update is called once per frame
    void Update()
    {
        //Update the game clock
        gameTime += Time.deltaTime;

        if(dead || respawn){
            Respawn(spawn);
        }
        Track1.text = coin1 + "/" + trackTime1.ToString("0.00");
        Obj1.text = mistakes1 + "/" + objTime1.ToString("0.00");
        Track2.text = coin2 + "/" + trackTime2.ToString("0.00");
        Obj2.text = mistakes2 + "/" + objTime2.ToString("0.00");
        Track3.text = coin3 + "/" + trackTime3.ToString("0.00");
        Obj3.text = mistakes3 + "/" + objTime3.ToString("0.00");
    }

    public void ChangeScene(string sceneName)
    {
        XR_Origin.SetActive(false);
        if(sceneName == "Controller Data"){
            XR_Origin.SetActive(true);
            //respawn = true;
            if(SceneManager.GetActiveScene().name == "Obstacle_1"){
                Debug.Log("starting track2");
                objTime1 = GetGameTime() - (trackTime1 + startTime);
            }
            if(SceneManager.GetActiveScene().name == "Obstacle_2"){
                Debug.Log("starting track3");
                objTime2 = GetGameTime() - (trackTime2 + objTime1 + trackTime1 + startTime);
            }
            if(SceneManager.GetActiveScene().name == "Obstacle_3"){
                Debug.Log("ending game");
                objTime3 = GetGameTime() - (trackTime3 + objTime2 + trackTime2 + objTime1 + trackTime1 + startTime);
            }
        }
        if(sceneName == "Obstacle_1"){
            trackTime1 = GetGameTime() - startTime;
        }
        if(sceneName == "Obstacle_2"){
            trackTime2 = GetGameTime() - (objTime1 + trackTime1 + startTime);
        }
        if(sceneName == "Obstacle_3"){
            trackTime3 = GetGameTime() - (objTime2 + trackTime2 + objTime1 + trackTime1 + startTime);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Respawn(int point){
       Transform targetTransform = XR_Origin.transform;
       switch(point)
        {
            case 1:
                targetTransform.position = new Vector3(326, 20, 50);
                break;
            case 2:
                targetTransform.position = new Vector3(411, 15, -11);
                break;
            case 3:
                targetTransform.position = new Vector3(330, 3, -103);
                break;
            default:
                break;
        }
       dead = false;
       respawn = false;
    }

    public float GetGameTime()
    {
        return gameTime;
    }
}
