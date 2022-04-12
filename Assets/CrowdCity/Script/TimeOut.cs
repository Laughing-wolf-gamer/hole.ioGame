using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeOut : MonoBehaviour {

	public static TimeOut instance;
	public Image symbols;
	public TextMeshProUGUI name1, name2, name3, rank1,rank2,rank3, kill1,kill2,kill3;


	 void Awake(){
		instance = this;
	}

	public void TimeOutPanelActivate()
	{
        List<GroupData> finalRanks = GameManager.Instance.getFinalRakings();
		name1.text = finalRanks[0].LeaderName;
		name2.text = finalRanks[1].LeaderName;
		name3.text = finalRanks[2].LeaderName;

		rank1.text = finalRanks[0].score.ToString();
		rank2.text = finalRanks[1].score.ToString();
		rank3.text = finalRanks[2].score.ToString();

		kill1.text = finalRanks[0].KillCount.ToString();
		kill2.text = finalRanks[1].KillCount.ToString();
		kill3.text = finalRanks[2].KillCount.ToString();
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
