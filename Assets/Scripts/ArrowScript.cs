using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    private float arrowSpeed = 2.0f;
    private bool canShootStickyArrow;

    [SerializeField]
    private AudioClip stickyClip;

	// Use this for initialization
	void Start () {
        canShootStickyArrow = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.tag == "FirstStickyArrow"){
            if(canShootStickyArrow){
                ShootArrow();
            }
        } else if(this.gameObject.tag == "SecondStickyArrow"){
            if(canShootStickyArrow){
                ShootArrow();
            }
        } else {
            ShootArrow();
        }
      
	}
    IEnumerator ResetStickyArrow()
    {
        yield return new WaitForSeconds(2.5f);
        if (this.gameObject.tag == "FirstStickyArrow")
        {
            PlayerScripts.instance.PlayerShootOnce(true);
            this.gameObject.SetActive(false);
        }
        else if (this.gameObject.tag == "SecondStickyArrow")
        {
            PlayerScripts.instance.PlayerShootTwice(true);
            this.gameObject.SetActive(false);
        }
    }

    void ShootArrow() {
        Vector3 temp = transform.position;
        temp.y += arrowSpeed * Time.unscaledDeltaTime;
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "LargestBall" || target.tag == "LargeBall" || target.tag == "MediumBall"
           || target.tag == "SmallBall" || target.tag == "SmallestBall") {

            if(gameObject.tag == "FirstStickyArrow" || gameObject.tag == "FirstArrow") {
                PlayerScripts.instance.PlayerShootOnce(true);
            } else if(gameObject.tag == "SecondStickyArrow" || gameObject.tag == "SecondArrow") {
                PlayerScripts.instance.PlayerShootTwice(true);
            }

            gameObject.SetActive(false);
          
        } // if the arrow hits a ball

        if(target.tag == "TopBrick") {

            if(this.gameObject.tag == "FirstArrow"){
                PlayerScripts.instance.PlayerShootOnce(true);
                this.gameObject.SetActive(false);
            } else if(this.gameObject.tag == "SecondArrow") {
                PlayerScripts.instance.PlayerShootTwice(true);
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "FirstStickyArrow")
            {
                canShootStickyArrow = false;
                Vector3 targetPos = target.transform.position;
                Vector3 temp = transform.position;
                targetPos.y -= 0.989f;
                temp.y = targetPos.y;
                transform.position = temp;
                AudioSource.PlayClipAtPoint(stickyClip, transform.position);
                StartCoroutine("ResetStickyArrow");
            }
            else if (this.gameObject.tag == "SecondStickyArrow")
            {
                canShootStickyArrow = false;
                Vector3 targetPos = target.transform.position; // position of the brick
                Vector3 temp = transform.position;
                targetPos.y -= 0.989f;
                temp.y = targetPos.y;
                transform.position = temp;
                AudioSource.PlayClipAtPoint(stickyClip, transform.position);
                StartCoroutine("ResetStickyArrow");
            }

        } // on trigger enter

    }
}
