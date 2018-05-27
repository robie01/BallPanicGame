using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goToMainMenu() {

        Application.LoadLevel("MenuScene");
    }

    public void goToShopMenu(){
        Application.LoadLevel("ShopMenu");
    }
}
