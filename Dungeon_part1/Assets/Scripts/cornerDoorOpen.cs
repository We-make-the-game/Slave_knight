using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cornerDoorOpen : MonoBehaviour
{
    GameObject player;
    Animation doorAni;
    GameObject pressETxt;

    void Awake()
    {
        player = GameObject.Find("player");
        doorAni = GetComponent<Animation>();
        pressETxt = GameObject.Find("pressE");
        
        pressETxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > -0.3 && player.transform.position.x < 1)
        {
            pressETxt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                doorAni.Play("door_open_corner");
            }
        }
    }

    void doorAniStart()
    {
        CharacterControl.playerControl = false;
        pressETxt.SetActive(false);
    }

    void doorAniEnd()
    {
        CharacterControl.playerControl = true;
        SceneManager.LoadScene("castle_courtyard");
    }
}
