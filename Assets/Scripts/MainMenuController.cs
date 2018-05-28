using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour {


    // for settings button panel that animates the buttons.
    [SerializeField]
    private Animator settingsButtonAnim;

    private bool hidden;
    private bool canTouchSettingsButton;

    [SerializeField]
    private Sprite[] musicBtnSprites;

    [SerializeField]
    private Button fbBtn;

    [SerializeField]
    private Sprite[] fbSprites;

	// Use this for initialization
	void Start () {
        canTouchSettingsButton = true;
        hidden = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SettingsButton(){
        StartCoroutine(DisableButtonsWhilePlayingAnimation());
    }


    // animator for the buttons 
    IEnumerator DisableButtonsWhilePlayingAnimation() {

       if(canTouchSettingsButton) {
            if(hidden) {
                canTouchSettingsButton = false;
                settingsButtonAnim.Play("SlideIn");
                hidden = false;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingsButton = true;
            } else {
                canTouchSettingsButton = true;
                settingsButtonAnim.Play("SlideOut");
                hidden = true;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingsButton = true; 
            }
        }
    }
}
