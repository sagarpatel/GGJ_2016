using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public int m_playerIndex = 0;
    Rigidbody m_bodyRigidbody;

    float m_moveSpeed = 40.0f;
    float m_moveFriction = 0.9f;
    float m_jumpPower = 200000.0f;

    void Start()
    {
        m_bodyRigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        float input_Horizontal = 0;
        float input_Up = 0;

        if(m_playerIndex == 0)
        {
            if (Input.GetKey(KeyCode.A) == true)
                input_Horizontal = -1.0f;
            else if (Input.GetKey(KeyCode.D) == true)
                input_Horizontal = 1.0f;

            if (Input.GetKeyDown(KeyCode.W) == true)
                input_Up = 1.0f;
        }
        else if(m_playerIndex == 1)
        {
            if (Input.GetKey(KeyCode.LeftArrow) == true)
                input_Horizontal = -1.0f;
            else if (Input.GetKey(KeyCode.RightArrow) == true)
                input_Horizontal = 1.0f;

            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                input_Up = 1.0f;
        }

        ProcessInputs(input_Horizontal, input_Up);
    }

    void ProcessInputs(float horizontal, float up)
    {
        if (up == 0)
        {
            m_bodyRigidbody.velocity += transform.forward * horizontal * m_moveSpeed * Time.deltaTime;            
        }
        Vector3 tempVel = m_bodyRigidbody.velocity;
        tempVel *= m_moveFriction;
        tempVel.y = m_bodyRigidbody.velocity.y;
        m_bodyRigidbody.velocity = tempVel;

        // need to implement cooldown
        Vector3 jumpForce = transform.up * up * m_jumpPower;
        m_bodyRigidbody.AddForce(jumpForce);
        //Debug.Log(jumpForce);
        
    }

}
