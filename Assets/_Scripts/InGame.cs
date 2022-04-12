using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame : MonoBehaviour {

	public static InGame instance;
	private Menu menu;
	private UIManager uiManager;

	public TextMeshProUGUI timeTxt, killNumTxt;
	public GameObject playerRank;
	public TextMeshProUGUI player1, player2, player3;
	private GameManager gm;
	public List<GroupData> data;
    GameTimer gt;
    void Start(){
        // gm = FindObjectOfType<GameManager>();
        // data = gm.sortedDataForUI;
        // gt = FindObjectOfType<GameTimer>();
    }
	public void SetKillAmount(int amount){
		killNumTxt.SetText(amount.ToString());
	}
	void FirstStep()
	{
        if (!GameManager.isGameStarted) return;
        data = GameManager.Instance.sortedDataForUI;
        if (data.Count == 0) return;

        if (data.Count > 1) {
			player1.text = "" + data [0].LeaderName + "  " + data [0].score;
			player1.color = data [0].groupColor;
		}
		if (data.Count > 1) {
			player2.text = "" + data [1].LeaderName + "  " + data [1].score;
			player2.color = data [1].groupColor;
		}

		if (data.Count > 2) {
			player3.text = "" + data[2].LeaderName + "  " + data[2].score;
			player3.color = data[2].groupColor;
		}
		killNumTxt.text = data[0].KillCount.ToString();
		timeTxt.text = gt.timeText.text;
	}

	void FixedUpdate(){
		FirstStep ();
	}
}
