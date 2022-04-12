using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {


	public static Menu instance;
	[SerializeField] private UIManager uiManager;
	[SerializeField] private HoleController[] holeController;
	// private GameManager gameManager;
	private GroupData groupData;
	public AudioSource audioSource;

	public TextMeshProUGUI bestscoreTxt, rankTxt, nameTxt;
	public Image city, play, sound;
	public Sprite  soundOn,soundOff;
	//public Color[] colors;
	public Material playerMat;

	void Awake(){
		instance = this;
	}

	void Start(){
		// uiManager = FindObjectOfType<UIManager> ();
        // gameManager = FindObjectOfType<GameManager> ();
		FirstFill ();
	}

	public void FirstFill()
	{
		// bestscoreTxt.text = gameManager.data[0].KillCount.ToString();
		nameTxt.text = "You";
	}

	public void PlayBtnPress()
	{
		uiManager.menuPanel.SetActive (false);
		uiManager.timeOutPanel.SetActive (false);
		uiManager.killedByPanel.SetActive (false);
		uiManager.ingamePanel.SetActive (true);
		// gameManager.StartGame ();
		GameHandler.i.StartGame();
		for (int i = 0; i < holeController.Length; i++){
			holeController[i].SetIsGameStart(true);
			
		}
	}

	public void SoundBtnPress()
	{
		if (audioSource.volume == 0) {
			audioSource.volume = 1;
			sound.sprite = soundOn;
		} else {
			audioSource.volume = 0;
			sound.sprite = soundOff;
		}
	}

	public void Left()
	{
		FindObjectOfType<PlayerMaterialChanger> ().leftButton ();

		city.color = playerMat.color;
		play.color = playerMat.color;	
	}

	public void Right()
	{
		FindObjectOfType<PlayerMaterialChanger> ().rightButton();

		city.color = playerMat.color;
		play.color = playerMat.color;
	}


	public void PrivacyPolicy(){
		Application.OpenURL ("http://lionprovpn.xyz/crowd-zombie-privacy-policy.html");

    }
}
