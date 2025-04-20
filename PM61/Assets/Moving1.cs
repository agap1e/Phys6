using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Moving1 : MonoBehaviour
{
    public float speed = 10f;
    public TMP_InputField velocityInput1;
    public TMP_InputField angleInput;

    [Tooltip("Угол направления движения в градусах вокруг оси Y")]
    [Range(0f, 360f)]
    public float angleDegrees = 45f;

    private Rigidbody rb;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (angleInput != null)
        {
            angleInput.onEndEdit.AddListener(UpdateAngle);
        }
        if (velocityInput1 != null)
        {
            velocityInput1.onEndEdit.AddListener(UpdateVel1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Получаем нормаль первого контакта
        Vector3 normal = collision.contacts[0].normal;

        // Отражаем скорость относительно нормали
        velocity = Vector3.Reflect(velocity, normal);

        // Обновляем скорость Rigidbody
        rb.velocity = velocity;
    }
    void UpdateVel1(string input)
    {
        if (float.TryParse(input, out float newSpeed))
        {
            speed = newSpeed;
            float angleRad = angleDegrees * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angleRad), 0f, Mathf.Cos(angleRad));
            velocity = direction.normalized * speed;
            rb.velocity = velocity;
        }
    }

    void UpdateAngle(string input)
    {
        if (float.TryParse(input, out float newAngle))
        {
            angleDegrees = newAngle;
        }
    }

}
