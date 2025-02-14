using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public static bool playerControl;

    Rigidbody rb;
    BoxCollider coll;
    Animator playerAni;
    float speed = 5f;
    float jumpForce = 200f;
    bool isOnGround;
    int jumpCount;
    float moveX;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = true;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>(); // 人物碰撞体
        playerAni = GetComponent<Animator>(); // 人物运动动画
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl)
        {
            playerMoveUpdate();
        }
    }

    void playerMoveUpdate() 
    {
        // 获取左右移动按键输入
        moveX = Input.GetAxis("Horizontal");
        // 检测跳跃按键
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }        
        //人物速度
        rb.velocity = new Vector3(moveX * speed, rb.velocity.y, 0);
        // 根据行走方向改变人物动画方向
        if (moveX > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (moveX < 0)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        //传递speed参数给animator controller
        playerAni.SetFloat("speed", Mathf.Abs(moveX));

        // 确保在地面时，重置跳跃状态
        if (isOnGround && !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            playerAni.SetBool("jump", false);
        }
    }

    void Jump()
    {
        //在地面上,设置连跳次数为2
        if (isOnGround)
        {
            jumpCount = 2;
        }
        Debug.Log("Jump isOnGround: " + isOnGround);
        //在地面上跳跃 or  //在空中跳跃
        if (isOnGround || (jumpCount > 0 && !isOnGround))
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
            playerAni.SetTrigger("jump");
            isOnGround = false;
            jumpCount--;
        }
    }

    // 跳跃-碰撞持续时调用
    private void OnCollisionStay(Collision collision)
    {
        // 如果碰撞对象有Ground标签，设置isOnGround为true
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    // 跳跃-碰撞结束时调用
    private void OnCollisionExit(Collision collision)
    {
        // 如果离开碰撞对象有Ground标签，设置isOnGround为false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
