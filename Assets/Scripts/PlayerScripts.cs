using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour {

    public static PlayerScripts instance;

    private float speed = .01f;
    private float maxVelocity = 3.0f;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject[] arrows;

    // to calculate the camera height
    private float height;

    private bool canWalk;

    [SerializeField]
    private AnimationClip clip;

    [SerializeField]
    private AudioClip shootclip;

    public bool shootOnce, shootTwice;

    // initialization
    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        float cameraHeight = Camera.main.orthographicSize;
        // position the arrow exactly below the camera
        height = -cameraHeight - 0.8f;
        canWalk = true;

        shootOnce = true;
        shootTwice = true;
    }

    // Use this for initialization
    void Start () {
        // for Player Object to have some mass and gravity, not to fall, equivalent to fix angle.
       GetComponent<Rigidbody2D>().freezeRotation = true;
	}
	
    // Update is called once per frame
    // remember: Also used for method that is using transform.
    void Update()
    {
        shootTheArrow();
    }

    // remember: Also used for method that is using physics.
    private void FixedUpdate()
    {
        PlayerWalkKeyBoard();
    }

    public void PlayerShootOnce(bool shootOnce) {
        this.shootOnce = shootOnce;
    }
    public void PlayerShootTwice(bool shootTwice) {
        this.shootTwice = shootTwice;
    }

    public void shootTheArrow() {

        if(Input.GetMouseButtonDown(0)) {

            if(shootOnce) {
               shootOnce = false;
                StartCoroutine(PlayTheShootAnimation());
                // firing arrows based on player's x
                 Instantiate(arrows[0], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            } else if (shootTwice) {
               shootTwice = false;
                StartCoroutine(PlayTheShootAnimation());
                // firing arrows based on player's x
               Instantiate(arrows[1], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            }
        }

    }

    IEnumerator PlayTheShootAnimation() {
        canWalk = false;
        animator.Play("PlayerShoot");
        AudioSource.PlayClipAtPoint(shootclip, transform.position);
        yield return new WaitForSeconds(clip.length);
        animator.SetBool("Shoot" ,false);
        canWalk = true;
        
    }

    void PlayerWalkKeyBoard() {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidBody.velocity.x);

        // determining either we move to left side or right side.
        float h = Input.GetAxis("Horizontal");

        if(canWalk) {
            if (h > 0)
            {
                // moving right
                if (velocity < maxVelocity)
                {
                    force = speed;
                }
                Vector3 scale = transform.localScale;
                scale.x = 1.0f;
                transform.localScale = scale;

                animator.SetBool("Walk", true);

            }
            else if (h < 0)
            {
                // moving left
                if (velocity < maxVelocity)
                {
                    force = -speed;
                }
                Vector3 scale = transform.localScale;
                scale.x = -1.0f;
                transform.localScale = scale;

                animator.SetBool("Walk", true);

            }
            else if (h == 0)
            {
                animator.SetBool("Walk", false);
            }

            myRigidBody.AddForce(new Vector2(force, 0));
        }
    }
}
