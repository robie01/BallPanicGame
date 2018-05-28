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
    private Button musicBtn;

    [SerializeField]
    private Sprite[] musicBtnSprites;

    [SerializeField]
    private Button fbBtn;

    [SerializeField]
    private Sprite[] fbSprites;


    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private Image infoImage;

    [SerializeField]
    private Sprite[] infoSprite;

    private int infoIndex;


	// Use this for initialization
	void Start () {
        canTouchSettingsButton = true;
        hidden = true;

        if(GameController1.instance.isMusicOn) {
            musicBtn.image.sprite = musicBtnSprites[0];

        }else {
            musicBtn.image.sprite = musicBtnSprites[1];
        }

        // initial image for our info index
        infoIndex = 0;
        infoImage.sprite = infoSprite[infoIndex];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SettingsButton(){
        StartCoroutine(DisableButtonsWhilePlayingAnimation());
    }


    // animation for the buttons 
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

    public void MusicButton()
    {
        if (GameController1.instance.isMusicOn)
        {
            musicBtn.image.sprite = musicBtnSprites[1];

            GameController1.instance.isMusicOn = false;
            GameController1.instance.Save();
        }
        else
        {
            musicBtn.image.sprite = musicBtnSprites[0];

            GameController1.instance.isMusicOn = true;
            GameController1.instance.Save();
        }
    
    }


    public void OpenInfoPanel(){

        infoPanel.SetActive(true);
    }

    public void CloseInfoPanel() {
        infoPanel.SetActive(false);
    }

    public void NextInfo() {
        infoIndex++;

        if (infoIndex == infoSprite.Length) {
            infoIndex = 0;
        }

        infoImage.sprite = infoSprite[infoIndex];
    }
}
