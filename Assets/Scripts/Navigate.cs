using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goToMainMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void goToShopMenu(){
        SceneManager.LoadScene("ShopMenu");
    }
}
