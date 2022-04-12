using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
	[SerializeField] private KilledBy killedBy;
	[SerializeField] private InGame ingameUI;
	[SerializeField] private TextMeshProUGUI bestScore;
	[SerializeField] private TextMeshProUGUI currentTimerText;
	[SerializeField] private TextMeshProUGUI[] firstRank,secondRank,thirdRank;
	[SerializeField] private TextMeshProUGUI[] firstRankScore,secondRankScore,thirdRankScore;
	public GameObject menuPanel, ingamePanel, timeOutPanel, killedByPanel,extraTimePanal;

	public static UIManager i{get;private set;}
	private void Awake(){
		i = this;
	}
	public void GameOver() {
		menuPanel.SetActive (false);
		ingamePanel.SetActive (false);
		timeOutPanel.SetActive (true);
		killedByPanel.SetActive (false);
		extraTimePanal.SetActive(false);
	}
	public void TimeOut(){
		// On first Timer End ....... Calls this Function...........
		menuPanel.SetActive (false);
		ingamePanel.SetActive (false);
		timeOutPanel.SetActive (false);
		extraTimePanal.SetActive(true);
	}

	public void KilledBy(string KilledByName){
		menuPanel.SetActive (false);
		ingamePanel.SetActive (false);
		timeOutPanel.SetActive (false);
		killedByPanel.SetActive (true);
		extraTimePanal.SetActive(false);
		
		killedBy.KilledByMethod(KilledByName);

	}
	public void GameResume(){
		// On Game Restart With Extra Time ....... Calls this Function...........
		menuPanel.SetActive (false);
		ingamePanel.SetActive (true);
		timeOutPanel.SetActive (false);
		extraTimePanal.SetActive(false);
	}
	public void SetCurrentTimer(float minit,float seconds){
		currentTimerText.SetText(string.Format("{0:00}:{1:00}",minit,seconds));
	}
	public void SetFirstRank(string first,int score){
		for (int i = 0; i < firstRank.Length; i++){
			if(score > 0){
				firstRank[i].transform.parent.gameObject.SetActive(true);	
			}else{
				firstRank[i].transform.parent.gameObject.SetActive(false);	
			}
			firstRank[i].SetText(first);
			
		}
		if(firstRankScore.Length > 0){
			for (int i = 0; i < firstRankScore.Length; i++){
				firstRankScore[i].SetText(score.ToString());
			}
		}
		SetBestScore(score);
	}
	public void SetSecondRank(string second,int score){
		for (int i = 0; i < secondRank.Length; i++){
			if(score > 0){
				secondRank[i].transform.parent.gameObject.SetActive(true);	
			}else{
				secondRank[i].transform.parent.gameObject.SetActive(false);	
			}
			secondRank[i].SetText(second);
		}
		if(secondRankScore.Length > 0){
			for (int i = 0; i < secondRankScore.Length; i++){
				secondRankScore[i].SetText(score.ToString());
			}
		}
		
	}
	public void SetThirRank(string Third,int score){
		for (int i = 0; i < thirdRank.Length; i++){
			if(score > 0){
				thirdRank[i].transform.parent.gameObject.SetActive(true);	
			}else{
				thirdRank[i].transform.parent.gameObject.SetActive(false);	
			}
			thirdRank[i].SetText(Third);
			
		}
		if(thirdRankScore.Length > 0){
			for (int i = 0; i < thirdRankScore.Length; i++){
				thirdRankScore[i].SetText(score.ToString());
			}
		}
		
	}
	public void SetBestScore(int getBestScore){
		bestScore.SetText(getBestScore.ToString());
	}
	public void SetKillAmountText(int amount){
		ingameUI.SetKillAmount(amount);
	}
}
