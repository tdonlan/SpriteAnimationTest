using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMoveController : MonoBehaviour {

    public Vector3 destination;
    public Camera mainCamera;

    private float speed = 10;
    private Vector3 Velocity;

    private Text debugText;

    private Rigidbody2D playerRigidBody;


	// Use this for initialization
	void Start () {
        debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();


	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButton(0)){
             var mousePos = Input.mousePosition;
       
             destination = mainCamera.ScreenToWorldPoint(mousePos);
             destination = new Vector3(destination.x, destination.y, 0);
        }

        Velocity = destination - gameObject.transform.position;

        if (Vector3.Distance(destination, gameObject.transform.position) > 1)
        {
            Velocity.Normalize();
        }
       

        playerRigidBody.velocity = Velocity * speed;

        mainCamera.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,mainCamera.transform.position.z);


        debugText.text = string.Format("{0}, {1}", playerRigidBody.velocity.x,playerRigidBody.velocity.y);

	}
}
