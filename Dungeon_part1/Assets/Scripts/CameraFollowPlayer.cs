using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public static Scene currentScene;
    public static string sceneName;
    string previousSceneName;

    void Awake()
    {
        previousSceneName = sceneName;
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (previousSceneName == null) {
            previousSceneName = "None";
        }
    }

    void Start()
    {
        GetPlayerTransform();
    }

    void Update()
    {
        UpdateCameraPos();
    }

    void GetPlayerTransform()
    {
        Debug.Log("previousSceneName: " + previousSceneName);
        Debug.Log("currentSceneName: " + sceneName);
        switch (sceneName)
        {
            case "dungeon_elevator":
                if (previousSceneName == "dungeon_underground") {
                    player.transform.position = new Vector3(-9.4f, -2.3f, -1.5f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                    transform.position = new Vector3(transform.position.x, 2, transform.position.z);
                } else if (previousSceneName == "dungeon_ground") {
                    player.transform.position = new Vector3(-9.4f, 8.2f, -1.5f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                    transform.position = new Vector3(transform.position.x, 12.5f, transform.position.z);
                }
                break;
            case "dungeon_cell":
                if (previousSceneName == "None") {
                    player.transform.position = new Vector3(-5, -1.81f, -3.37f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else if (previousSceneName == "dungeon_underground") {
                    player.transform.position = new Vector3(6.5f, -1.81f, -3.37f);
                    player.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                break;
            case "dungeon_underground":
                if (previousSceneName == "dungeon_cell") {
                    player.transform.position = new Vector3(-7, -2.3f, -2.19f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else if (previousSceneName == "dungeon_elevator") {
                    player.transform.position = new Vector3(30.6f, -2.3f, -2.19f);
                    player.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                break;
            case "dungeon_ground":
                if (previousSceneName == "dungeon_corner_room") {
                    player.transform.position = new Vector3(-9, -2.3f, -1.5f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else if (previousSceneName == "dungeon_elevator") {
                    player.transform.position = new Vector3(59.33f, -2.3f, -1.5f);
                    player.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                break;
            case "castle_courtyard":
                if (previousSceneName == "dungeon_corner_room") {
                    player.transform.position = new Vector3(-10.5f, -2.6f, -3.21f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else if (previousSceneName == "castle_entry") {
                    player.transform.position = new Vector3(60.5f, -3.33f, -3.21f);
                    player.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                break;
            case "castle_entry":
                if (previousSceneName == "castle_courtyard") {
                    player.transform.position = new Vector3(-2.2f, -3.3f, -2.19f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else if (previousSceneName == "dungeon_bridge") {
                    player.transform.position = new Vector3(26.5f, -3.3f, -2.19f);
                    player.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                break;
            case "dungeon_bridge":
                if (previousSceneName == "castle_entry") {
                    player.transform.position = new Vector3(4, -9, 38.44f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                break;
            case "dungeon_corner_room":
                if (previousSceneName == "castle_courtyard") {
                    player.transform.position = new Vector3(0.2f, -1.85f, -2.19f);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                } else if (previousSceneName == "dungeon_ground") {
                    player.transform.position = new Vector3(6, -1.85f, -2.19f);
                    player.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                break;
            case "dungeon_guardian_room":
                break;
            default:
                break;
        }
    }

    void UpdateCameraPos()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        switch (sceneName)
        {
            case "dungeon_elevator":
                transform.position = LimitCameraPos(-4, 0.5f, player.transform.position.x, transform.position);
                break;
            case "dungeon_cell":
                transform.position = LimitCameraPos(-2.5f, -0.5f, player.transform.position.x, transform.position);
                break;
            case "dungeon_underground":
                transform.position = LimitCameraPos(-2.5f, 25.5f, player.transform.position.x, transform.position);
                break;
            case "dungeon_ground":
                transform.position = LimitCameraPos(-4, 54.5f, player.transform.position.x, transform.position);
                break;
            case "castle_courtyard":
                transform.position = LimitCameraPos(-1.6f, 51, player.transform.position.x, transform.position);
                break;
            case "castle_entry":
                transform.position = LimitCameraPos(7.6f, 26, player.transform.position.x, transform.position);
                break;
            case "dungeon_bridge":
                transform.position = LimitCameraPos(8, 35, player.transform.position.x, transform.position);
                break;
            case "dungeon_corner_room":
                transform.position = LimitCameraPos(-18, 1, player.transform.position.x, transform.position);
                break;
            case "dungeon_guardian_room":
                transform.position = LimitCameraPos(-24, -6, player.transform.position.x, transform.position);
                break;
            default:
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                break;
        }
    }

    Vector3 LimitCameraPos(float x_l, float x_r, float player_x, Vector3 cameraPos) 
    {
        if (cameraPos.x > x_r) {
            return new Vector3(x_r, cameraPos.y, cameraPos.z);
        } else if (cameraPos.x < x_l) {
            return new Vector3(x_l, cameraPos.y, cameraPos.z);
        } else {
            return new Vector3(player_x, cameraPos.y, cameraPos.z);
        }
    }
}
