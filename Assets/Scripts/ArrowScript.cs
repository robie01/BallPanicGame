﻿using System.Collections;
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
        if (this.gameObject.tag == "SingleStickyArrow")
        {
            PlayerScripts.instance.PlayerShootOnce(true);
            this.gameObject.SetActive(false);
        }
        else if (this.gameObject.tag == "DoubleStickyArrow")
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

            if(gameObject.tag == "SingleStickyArrow" || gameObject.tag == "FirstArrow") {
                PlayerScripts.instance.PlayerShootOnce(true);
            } else if(gameObject.tag == "DoubleStickyArrow" || gameObject.tag == "SecondArrow") {
                PlayerScripts.instance.PlayerShootTwice(true);
            }

            gameObject.SetActive(false);
          
        } // if the arrow hits a ball

        if(target.tag == "TopBrick" || target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom"
           || target.tag == "UnbreakableBrickLeft" || target.tag == "UnbreakableBrickRight"
           || target.tag == "UnbreakableBrickBottomVertical") {

            if(this.gameObject.tag == "FirstArrow"){
                PlayerScripts.instance.PlayerShootOnce(true);
                this.gameObject.SetActive(false);
            } else if(this.gameObject.tag == "SecondArrow") {
                PlayerScripts.instance.PlayerShootTwice(true);
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.tag == "SingleStickyArrow")
            {
                canShootStickyArrow = false;
                Vector3 targetPos = target.transform.position;
                Vector3 temp = transform.position;

                if(target.tag == "TopBrick"){
                    targetPos.y -= 0.989f;
                } else if(target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom"
                          || target.tag == "UnbreakableBrickLeft" || target.tag == "UnbreakableBrickRight") {
                    targetPos.y -= 0.75f;
                } else if(target.tag == "UnbreakableBrickBottomvertical") {
                    targetPos.y -= 0.97f;
                }

                temp.y = targetPos.y;
                transform.position = temp;
                AudioSource.PlayClipAtPoint(stickyClip, transform.position);
                StartCoroutine("ResetStickyArrow");
            }
            else if (this.gameObject.tag == "DoubleStickyArrow")
            {
                canShootStickyArrow = false;
                Vector3 targetPos = target.transform.position; // position of the brick
                Vector3 temp = transform.position;

                if (target.tag == "TopBrick")
                {
                    targetPos.y -= 0.989f;
                }
                else if (target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom"
                        || target.tag == "UnbreakableBrickLeft" || target.tag == "UnbreakableBrickRight")
                {
                    targetPos.y -= 0.75f;
                }
                else if (target.tag == "UnbreakableBrickBottomvertical")
                {
                    targetPos.y -= 0.97f;
                }
              
                temp.y = targetPos.y;
                transform.position = temp;
                AudioSource.PlayClipAtPoint(stickyClip, transform.position);
                StartCoroutine("ResetStickyArrow");
            }

        }  // if the arrow hits unbreakable brick

        if(target.tag == "BrokenBrickTop" || target.tag == "BrokenBrickBottom" 
           || target.tag == "BrokenBrickLeft" || target.tag == "BrokenBrickRight") {

            // break the broken brick
            BrickScript brick = target.gameObject.GetComponentInParent<BrickScript>();
            brick.StartCoroutine(brick.BreakTheBrick());

            if(gameObject.tag == "FirstArrow" || gameObject.tag == "SingleStickyArrow"){
                PlayerScripts.instance.PlayerShootOnce(true);
            } else if(gameObject.tag == "SecondArrow" || gameObject.tag == "DoubleStickyArrow") {
                PlayerScripts.instance.PlayerShootTwice(true);
            }

            gameObject.SetActive(false);
        } // if the arrow hits breakable brick

    }// on trigger enter
}
