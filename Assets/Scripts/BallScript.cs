using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [ExecuteInEditMode]

public class BallScript : MonoBehaviour {

    private float forceX, forceY;

    [SerializeField]
    private bool moveLeft, moveRight;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private GameObject originalBall;

    private GameObject ball1, ball2;

    private BallScript ballScript1, ballScript2;

    private void Awake()
    {
        SetBallSpeed();
        InstantiateBalls();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveBall();
	}

    public void SetMoveLeft(bool moveLeft) {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;
    }
    public void SetMoveRight(bool moveRight) {
        this.moveRight = moveRight;
        this.moveLeft = !moveRight;
        
    }

    void InstantiateBalls() {

        if(this.gameObject.tag != "SmallestBall") {
            ball1 = Instantiate(originalBall);
            ball2 = Instantiate(originalBall);

            ballScript1 = ball1.GetComponent<BallScript>();
            ballScript2 = ball2.GetComponent<BallScript>();

            ball1.SetActive(false);
            ball2.SetActive(false);
        }
    }


    void MoveBall(){
        if(moveLeft){
            Vector3 temp = transform.position;
            temp.x -= (forceX * Time.deltaTime);
            transform.position = temp;
        }
        if (moveRight)
        {
            Vector3 temp = transform.position;
            temp.x += (forceX * Time.deltaTime);
            transform.position = temp;
        }
    } // move the ball

    void InitializedBallsAndTurnOffCurrentBalls() {

        Vector3 position = transform.position;

        ball1.transform.position = position;
        ballScript1.SetMoveLeft(true);

        ball2.transform.position = position;
        ballScript2.SetMoveRight(true);

        ball1.SetActive(true);
        ball2.SetActive(true);

        switch(gameObject.tag){
            
            case "LargestBall":
                if(transform.position.y > 1 && transform.position.y <= 1.3){
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                } else if (transform.position.y > 1.3f) {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                } else if(transform.position.y < 1) {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                }
                break;

            case "LargeBall":
                if (transform.position.y > 1 && transform.position.y <= 1.3)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                }
                else if (transform.position.y > 1.3f)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                }
                else if (transform.position.y < 1)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                }
                break;

            case "MediumBall":
                if (transform.position.y > 1 && transform.position.y <= 1.3)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                }
                else if (transform.position.y > 1.3f)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                }
                else if (transform.position.y < 1)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                }
                break;

            case "SmallBall":
                if (transform.position.y > 1 && transform.position.y <= 1.3)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                }
                else if (transform.position.y > 1.3f)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                }
                else if (transform.position.y < 1)
                {
                    ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                    ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                }
                break;
        }
        gameObject.SetActive(false);

    }

    void SetBallSpeed() {
        forceX = 2.5f;

        switch(this.gameObject.tag) {

            // base on the tag this will set the diff speed of each ball size.
            case "LargestBall":
                forceY = 11.5f;
                break;
            case "LargeBall":
                forceY = 10.5f;
                break;
            case "MediumBall":
                forceY = 9f;
                break;
            case "SmallBall":
                forceY = 8f;
                break;
            case "SmallestBall":
                forceY = 7f;
                break;
        } // ball speed
    }

    // this is fired when trigger in unity is clicked.
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "FirstArrow" || target.tag == "SecondArrow" || target.tag == "FirstStickyArrow"
           || target.tag == "SecondStickyArrow") {

            if (gameObject.tag != "SmallestBall") {
                InitializedBallsAndTurnOffCurrentBalls();
            } else {
                gameObject.SetActive(false);
            }
        } // if the ball hits the arrow

        if(target.tag == "BottomBrick") {
            // so ball wouldn't fall through brick.
            myRigidBody.velocity = new Vector2(0, forceY);
            
        }

        if(target.tag == "LeftBrick") {
            moveLeft = false;
            moveRight = true;
        }
        if (target.tag == "RightBrick")
        {
            moveLeft = true;
            moveRight = false;
        }


    }





} // class
