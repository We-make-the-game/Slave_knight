using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public static Scene currentScene;
    public static string sceneName;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        switch (sceneName)
        {
            case "dungeon_elevator":
                if (player.transform.position.x > 0.5f) {
                    transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
                } else if (player.transform.position.x < -4) {
                    transform.position = new Vector3(-4, transform.position.y, transform.position.z);
                } else {
                    transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                }
                break;
            default:
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                break;
        }
    }
}
