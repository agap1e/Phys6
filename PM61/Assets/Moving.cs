using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public float velocity1 = 0f;
    public float velocity2 = 0f;
    public float mass1 = 0f;
    public float mass2 = 0f;
    public TMP_InputField velocityInput1;
    public TMP_InputField velocityInput2;
    public TMP_InputField massInput1;
    public TMP_InputField massInput2;
    public TMP_InputField angleInput;
    public TMP_Text velocityOut1;
    public TMP_Text velocityOut2;
    //public TMP_Text angleOut1;
    //public TMP_Text angleOut2;

    Rigidbody rb1;
    Rigidbody rb2;
    float newVelocity1 = 0f;
    float newVelocity2 = 0f;

    float angle1 = 0f;
    float angle2 = 0f;

    Object1 comp;
    Vector3 check1 = Vector3.forward;
    Vector3 check2 = Vector3.forward;

    void Start()
    {
        rb1 = obj1.GetComponent<Rigidbody>();
        rb2 = obj2.GetComponent<Rigidbody>();

        if (velocityInput1 != null)
        {
            velocityInput1.onEndEdit.AddListener(UpdateVel1);
        }
        if (velocityInput2 != null)
        {
            velocityInput2.onEndEdit.AddListener(UpdateVel2);
        }
        if (massInput1 != null)
        {
            massInput1.onEndEdit.AddListener(UpdateMass1);
        }
        if (massInput2 != null)
        {
            massInput2.onEndEdit.AddListener(UpdateMass2);
        }
        if (angleInput != null)
        {
            angleInput.onEndEdit.AddListener(UpdateAngle);
        }

        comp = obj1.GetComponent<Object1>();
    }

    // Update is called once per frame
    void Update()
    {
        rb1.velocity = check1 * newVelocity1;
        rb2.velocity = check2 * newVelocity2;

        if (comp.coll) 
        {
            if (angle1 != 0f)
            {
                newVelocity2 = velocity1 / (Mathf.Sin(angle2) / Mathf.Sin(angle1) * Mathf.Cos(angle1) + Mathf.Cos(angle2));
                newVelocity1 = newVelocity2 * Mathf.Sin(angle2) / Mathf.Sin(angle1);
                comp.coll = false;
                check1 = Quaternion.AngleAxis(angle1, Vector3.up) * check1;
                check2 = Quaternion.AngleAxis(angle2, Vector3.up) * check2;
            }
            else
            {
                newVelocity1 = (velocity1 * (mass1 - mass2) + 2 * mass2 * velocity2) / (mass1 + mass2);
                newVelocity2 = (velocity2 * (mass2 - mass1) + 2 * mass1 * velocity1) / (mass1 + mass2);
                comp.coll = false;
            }
        }

        velocityOut1.text = "—корость первого шара: " + rb1.velocity.magnitude.ToString("F1") + " м/с";
        velocityOut2.text = "—корость второго шара: " + rb2.velocity.magnitude.ToString("F1") + " м/с";
        //angleOut1.text = "”гол первого шара: " + angle1.ToString("F1");
        //angleOut2.text = "”гол второго шара: " + angle2.ToString("F1");
    }

    void UpdateVel1(string input)
    {
        if (float.TryParse(input, out float newSpeed))
        {
            velocity1 = newSpeed;
            newVelocity1 = velocity1;
        }
    }

    void UpdateVel2(string input)
    {
        if (float.TryParse(input, out float newSpeed))
        {
            velocity2 = newSpeed;
            newVelocity2 = velocity2;
        }
    }

    void UpdateMass1(string input)
    {
        if (float.TryParse(input, out float newMass))
        {
            mass1 = newMass;
            rb1.mass = mass1;
        }
    }

    void UpdateMass2(string input)
    {
        if (float.TryParse(input, out float newMass))
        {
            mass2 = newMass;
            rb2.mass = mass2;
        }
    }

    void UpdateAngle(string input)
    {
        if (float.TryParse(input, out float newAngle))
        {
            angle1 = Math.Clamp(newAngle, 1f, 89f);
            angleInput.text = angle1.ToString();
            angle2 = 90f - angle1;

            check1 = Quaternion.AngleAxis(-angle1, Vector3.up) * check1;
            obj1.transform.position += new Vector3(2, 0, 0);
        }
    }

}
