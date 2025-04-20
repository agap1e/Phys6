using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BilliardBallCollision : MonoBehaviour
{
    public Rigidbody rbMoving;   // движущийся шар
    public Rigidbody rbStatic;   // неподвижный шар

    public TMP_InputField velocityInput1;


    public Vector3 initialVelocity = new Vector3(5f, 0f, 2f); // начальная скорость первого шара

    private bool collisionProcessed = false;

    void Start()
    {
        transform.Rotate(new Vector3(0, 45, 0));
        if (velocityInput1 != null)
        {
            velocityInput1.onEndEdit.AddListener(UpdateVel1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionProcessed) return;

        if ((collision.rigidbody == rbStatic && rbMoving != null) ||
            (collision.rigidbody == rbMoving && rbStatic != null))
        {
            Vector3 posMoving = rbMoving.position;
            Vector3 posStatic = rbStatic.position;

            Vector3 n = (posMoving - posStatic).normalized;

            Vector3 v1 = rbMoving.velocity;
            Vector3 v2 = rbStatic.velocity; // должен быть Vector3.zero

            float m1 = rbMoving.mass;
            float m2 = rbStatic.mass;

            if (Mathf.Abs(m1 - m2) > 0.00001f)
            {
                Debug.LogWarning("Массы должны быть равны для корректной симуляции.");
                return;
            }

            float vRelDotN = Vector3.Dot(v1 - v2, n);

            if (vRelDotN > 0)
            {
                // Шары расходятся, удар не происходит
                return;
            }

            // Скорости после удара
            Vector3 v1After = v1 - n * vRelDotN;
            Vector3 v2After = v2 + n * vRelDotN;

            // Применяем новые скорости
            rbMoving.velocity = v1After;
            rbStatic.velocity = v2After;

            collisionProcessed = true;

            Debug.Log($"После удара:\nШар 1 скорость: {v1After}\nШар 2 скорость: {v2After}");
        }
    }
    void UpdateVel1(string input)
    {
        if (float.TryParse(input, out float newSpeed))
        {
            initialVelocity = new Vector3(0, 0, newSpeed);
            rbMoving.velocity = initialVelocity;

        }
    }
}