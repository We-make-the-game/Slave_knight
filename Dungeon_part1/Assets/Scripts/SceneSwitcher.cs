using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    GameObject player;
    string sceneName;

    void Start()
    {
        player = GameObject.Find("player");
        sceneName = CameraFollowPlayer.sceneName;
    }

    // Update is called once per frame
    void Update()
    {
         switch (sceneName)
        {
            case "dungeon_elevator":
            if (player.transform.position.x < -10) {
                if (player.transform.position.y > 0) {
                    SceneManager.LoadScene("dungeon_ground");
                } else {
                    SceneManager.LoadScene("dungeon_underground");
                }
            }
                break;
            case "dungeon_cell":
                if (player.transform.position.x > 7) {
                    SceneManager.LoadScene("dungeon_underground");
                }
                break;
            case "dungeon_underground":
                if (player.transform.position.x > -8 && player.transform.position.x < -6.2f) {
                    if (Input.GetKeyDown(KeyCode.E)) {
                        SceneManager.LoadScene("dungeon_cell");
                    }
                } else if (player.transform.position.x > 31) {
                    SceneManager.LoadScene("dungeon_elevator");
                }
                break;
            case "dungeon_ground":
                if (player.transform.position.x > 60) {
                    SceneManager.LoadScene("dungeon_elevator");
                } else if (player.transform.position.x < -9.5f) {
                    SceneManager.LoadScene("dungeon_corner_room");
                }
                break;
            case "castle_courtyard":
                if (player.transform.position.x > -11.5f && player.transform.position.x < -9.5f) {
                    if (Input.GetKeyDown(KeyCode.E)) {
                        SceneManager.LoadScene("dungeon_corner_room");
                    }
                } else if (player.transform.position.x > 61) {
                    SceneManager.LoadScene("castle_entry");
                }
                break;
            case "castle_entry":
                if (player.transform.position.x > 24 && player.transform.position.x < 28.5f) {
                    if (Input.GetKeyDown(KeyCode.E)) {
                        SceneManager.LoadScene("dungeon_bridge");
                    }
                } else if (player.transform.position.x < -3) {
                    SceneManager.LoadScene("castle_courtyard");
                }
                break;
            case "dungeon_bridge":
                if (player.transform.position.x < 3.5f) {
                    SceneManager.LoadScene("castle_entry");
                }
                break;
            case "dungeon_corner_room":
                if (player.transform.position.x > 6.5f) {
                    SceneManager.LoadScene("dungeon_ground");
                }
                break;
            case "dungeon_guardian_room":
                break;
            default:
                break;
        }
    }
}
