using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour {

    private float speed = .01f;
    private float maxVelocity = 3.0f;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator animator;

    void Awake()
    {
       
    }

    // Use this for initialization
    void Start () {
       GetComponent<Rigidbody2D>().freezeRotation = true;
	}
	
	// Update is called once per frame
    // remember: Also used for method that is using transform.
	void Update () {
		
	}

    // remember: Also used for method that is using physics.
    private void FixedUpdate()
    {
        PlayerWalkKeyBoard();
    }

    void PlayerWalkKeyBoard() {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidBody.velocity.x);

        // determining either we move to left side or right side.
        float h = Input.GetAxis("Horizontal");

        if(h > 0){
            // moving right
            if(velocity < maxVelocity) {
                force = speed;
            }
            Vector3 scale = transform.localScale;
            scale.x = 1.0f;
            transform.localScale = scale;

            animator.SetBool("Walk", true);
       
        } else if (h < 0) {
            // moving left
            if (velocity < maxVelocity)
            {
                force = -speed;
            }
            Vector3 scale = transform.localScale;
            scale.x = -1.0f;
            transform.localScale = scale;

            animator.SetBool("Walk", true);

        } else if(h == 0) {
            
            animator.SetBool("Walk", false);
        }

        myRigidBody.AddForce(new Vector2(force, 0));
    }
}
