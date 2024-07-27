using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider coll;
    Animator playerAni;
    float speed = 5f;
    float jumpForce = 250f;
    bool isOnGround;
    int jumpCount;
    float moveX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>(); // 人物碰撞体
        playerAni = GetComponent<Animator>(); // 人物运动动画
    }

    // Update is called once per frame
    void Update()
    {
        // 获取左右移动按键输入
        moveX = Input.GetAxis("Horizontal");
        // 检测跳跃按键
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }        
        //人物速度
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
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
    }


    void Jump()
    {
        //在地面上,设置连跳次数为2
        if (isOnGround)
        {
            jumpCount = 2;
        }
        //在地面上跳跃 or  //在空中跳跃
        if (isOnGround || (jumpCount > 0 && !isOnGround))
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            playerAni.SetTrigger("jump");
            isOnGround = false;
            jumpCount--;
        }
    }

    // 碰撞持续时调用
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 如果碰撞对象有Ground标签，设置isOnGround为true
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    // 碰撞结束时调用
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 如果离开碰撞对象有Ground标签，设置isOnGround为false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
