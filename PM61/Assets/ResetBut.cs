using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetBut : MonoBehaviour
{
    public GameObject ball;
    public GameObject plane;
    public TMP_Text text;
    public TMP_Text text1;
    public TMP_InputField inputField;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
   public void ResetButt()
   {
        SceneManager.LoadScene(0);
   }
    public void ChangeAngle() 
    { 
        ball.transform.position = new Vector3(1.1f, 0.5f, 0);
        plane.GetComponent<Moving>().enabled = false;
        ball.GetComponent<BilliardBallCollision>().enabled = true;

        text.enabled = false;
        text1.enabled = false;
        
        inputField.gameObject.SetActive(false);
        inputField1.gameObject.SetActive(false);
        inputField2.gameObject.SetActive(false);

    }
}
