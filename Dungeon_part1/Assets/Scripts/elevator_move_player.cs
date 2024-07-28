using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator_move_player : MonoBehaviour
{
    GameObject lift;
    GameObject up_switch;
    GameObject down_switch;
    GameObject player;

    void Awake()
    {
        lift = GameObject.Find("elevetor/lift");
        up_switch = GameObject.Find("elevetor/switch_top");
        down_switch = GameObject.Find("elevetor/switch_bottom");
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        trigger_elevator();
    }

    void trigger_elevator() {
        // player presses E 
        if (Input.GetKeyDown(KeyCode.E))
        {
            // player in bottom switch trigger area and // evevator is up
            if (player.transform.position.y < 0 && player.transform.position.x > -8 && player.transform.position.x < -6.8 && lift.transform.position.y > 0)
            {

                up_switch.GetComponent<Animation>().Play("switch_rot_l");
                down_switch.GetComponent<Animation>().Play("switch_rot_r");
                lift.GetComponent<Animation>().Play("elevator_down");
            }
            // player in up switch trigger area and // evevator is down
            else if (player.transform.position.y > 0 && player.transform.position.x > -8 && player.transform.position.x < -6.8 && lift.transform.position.y < 0)
            {
                down_switch.GetComponent<Animation>().Play("switch_rot_l");
                up_switch.GetComponent<Animation>().Play("switch_rot_r");
                lift.GetComponent<Animation>().Play("elevator_up");
            }
        }   
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.transform.SetParent(lift.transform);
            // player take elevator
            if (lift.transform.position.y < 0) 
            {
                lift.GetComponent<Animation>().Play("elevator_up");
                down_switch.GetComponent<Animation>().Play("switch_rot_l");
                up_switch.GetComponent<Animation>().Play("switch_rot_r");
            }
            else 
            {
                lift.GetComponent<Animation>().Play("elevator_down");
                up_switch.GetComponent<Animation>().Play("switch_rot_l");
                down_switch.GetComponent<Animation>().Play("switch_rot_r");
            }

        }
    }

    void liftAniEnd()
    {
        player.transform.SetParent(null);
    }
}
