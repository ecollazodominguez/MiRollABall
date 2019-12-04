using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
    public float force = 11.0f; //used for accelerometer controls

    public Text countText;
    public Text winText;

    [Tooltip("Objeto programado para poder reiniciar el juego.")]
    public GameObject restart;

    private Rigidbody rb;
    private int count;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";
        restart.active=false;
    }

         void Main ()
         {
                 // Preventing mobile devices going in to sleep mode 
                 //(actual problem if only accelerometer input is used)
                 Screen.sleepTimeout = SleepTimeout.NeverSleep;
         }
 
     void Update()
         {
          
         if (SystemInfo.deviceType == DeviceType.Desktop) 
         {
             // Exit condition for Desktop devices
             if (Input.GetKey("escape"))
                 Application.Quit();
         }
         else
         {
             // Exit condition for mobile devices
             if (Input.GetKeyDown(KeyCode.Escape))
                 Application.Quit();            
         }
        }

    void FixedUpdate ()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
         {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

         Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

            rb.AddForce (movement * speed);
        }
        else
         {
                     // Player movement in mobile devices
                 // Building of force vector 
                 Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
                 // Adding force to rigidbody
                 rb.AddForce(movement * (speed*2));
         }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag ("Enemigo"))
        {
            winText.text = "You lose";
            countText.text = "0";
            // vuelve al centro del tablero
            rb.transform.position = Vector3.zero;
            rb.gameObject.active = false;
            other.gameObject.active = false;
            restart.active=true;
        }
        if (other.gameObject.CompareTag ("Barril"))
        {
            rb.AddExplosionForce(1000f,rb.position,5f);
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }

        
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString () + "/13";
        if (count >= 13)
        {
            winText.text = "You Win!";
            restart.active=true;
            GameObject.Find("Enemigo").active=false;
            rb.gameObject.active=false;
        }
    }
}

