using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private List<HoleController> holeControllerList;
    [SerializeField] private bool isGamePlaying,isGameEnd;
    [SerializeField] private bool isKilled;
    [SerializeField] private float maxTimer = 90,extraTime = 30f;
    
    [SerializeField] private bool isTimeout;
    [SerializeField] private string killedByName;
    public Action onGameEnd;
    public Action onTimerResume,onTimerUp;

    public bool canAskForExtraTime;
    public bool askedForExtraTime;

    #region Singelton.........
    public static GameHandler i;

    private void Awake(){
        i = this;
    }

    #endregion

    private void Start(){
        isTimeout = false;
        StartCoroutine(StartGameRoutine());
        
    }
    private void Update(){
        if(isGamePlaying){
            if(maxTimer > 0){
                maxTimer -= Time.deltaTime;
            }else{
                maxTimer = 0;
                isTimeout = true;
            }
        }
        DispalyTimer(maxTimer);
    }
    private void DispalyTimer(float timeToDisplay){
        if(timeToDisplay < 0){
            timeToDisplay = 0;
        }
        float minit = Mathf.FloorToInt(timeToDisplay / 60f);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        UIManager.i.SetCurrentTimer(minit,seconds);
    }

    private IEnumerator StartGameRoutine(){

        while(!isGamePlaying){
            yield return null;
        }
        yield return StartCoroutine(nameof(GamePlayingRoutine));
    }
    private IEnumerator GamePlayingRoutine(){
        while(!isGameEnd){
            if(holeControllerList.Count == 1 && isTimeout){
                // isTimeout = true;
                EndGame(false);
                break;
            }
            if(!isTimeout){
                if(holeControllerList.Count > 3){

                    int max1 = holeControllerList[0].GetScore();
                    int max2 = holeControllerList[1].GetScore();
                    int max3 =holeControllerList[2].GetScore();
                    // enemyController.TryRemoveColliders();
                    for (int i = 0; i < holeControllerList.Count; i++){
                        if(holeControllerList[i].GetIsDead()){
                            holeControllerList.Remove(holeControllerList[i]);
                        }else{
                            // Search for Highest Rank....
                            if(holeControllerList[i].GetScore() > max1 ){
                                max2 = max1;
                                max1 = holeControllerList[i].GetScore();
                            }
                            if(holeControllerList[i].GetScore() < max1 && holeControllerList[i].GetScore() > max2){
                                max3 = max2;
                                max2 = holeControllerList[i].GetScore();
                            }
                            if(holeControllerList[i].GetScore() < max2 && holeControllerList[i].GetScore() > max3){
                                max3 = holeControllerList[i].GetScore();
                            }
                        }
                    }
                    if(max1 > 0){
                        Debug.Log("Rank 1 " + max1);
                        Debug.Log("Rank 2 " + max2);
                        for (int h = 0; h < holeControllerList.Count; h++){
                            
                            if(holeControllerList[h].GetScore() == max1){
                                holeControllerList[h].ShowHideCrown(true);
                                UIManager.i.SetFirstRank(holeControllerList[h].GetHolesGroupData().LeaderName,holeControllerList[h].GetScore());
                                // UIManager.i.SetBestScore(holeControllerList[h].GetScore());
                            }else{
                                holeControllerList[h].ShowHideCrown(false);
                            }
                            if(holeControllerList[h].GetScore() == max2 && max2 != max1){
                                
                                UIManager.i.SetSecondRank(holeControllerList[h].GetHolesGroupData().LeaderName,holeControllerList[h].GetScore());
                            }
                            if(holeControllerList[h].GetScore() == max3 && max2 != max3){
                                UIManager.i.SetThirRank(holeControllerList[h].GetHolesGroupData().LeaderName,holeControllerList[h].GetScore());
                            }
                            
                        }
                    }
                }
                if(holeControllerList.Count == 1){
                    isTimeout = true;
                    EndGame(false);
                }
            }else{
                if(canAskForExtraTime){
                    onTimerUp?.Invoke();
                    yield return StartCoroutine(AskingForExtraTime());
                    yield break;
                }else{
                    isTimeout = true;
                    EndGame(false);
                }
            }
            
            yield return null;
        }
        

    }
    private IEnumerator AskingForExtraTime(){
        UIManager.i.TimeOut();
        for (int i = 0; i < holeControllerList.Count; i++){
            holeControllerList[i].SetIsGameStart(false);
        }
        while(canAskForExtraTime){
            yield return null;
        }
        canAskForExtraTime = false;
        if(askedForExtraTime){
            for (int i = 0; i < holeControllerList.Count; i++){
                holeControllerList[i].SetIsGameStart(true);
            }
            UIManager.i.GameResume();
            isTimeout = false;
            isGamePlaying = true;
            maxTimer += extraTime;
            onTimerResume?.Invoke();
        }else{
            maxTimer = 0f;
            for (int i = 0; i < holeControllerList.Count; i++){
                holeControllerList[i].SetIsGameStart(false);
            }
            if(holeControllerList.Count == 1){
                isTimeout = true;
                EndGame(false);
            }
        }
        yield return StartCoroutine(GamePlayingRoutine());
    }

    public void StartGame(){
        isGamePlaying = true;
    }
    
    public void EndGame(bool isKilled){
        
        canAskForExtraTime = false;
        onGameEnd?.Invoke();
        if(isKilled){
            UIManager.i.KilledBy(killedByName);
        }else{
            UIManager.i.GameOver();
        }
        isGamePlaying = false;
        isGameEnd = true;
    }
    public void SetKilledByName(string killedName){
        killedByName = killedName;
    }

    
    public bool GetisGamePlaying(){
        return isGamePlaying;
    }
    public void NeedExtraTime(bool value){
        // Can Ads from this call Ads from this Function....
        if(canAskForExtraTime){
            // For Testing I am Calling this here.
            AfterWatchingAds(value);
        }
    }
    private void AfterWatchingAds(bool value){
        // Call After Wathing ad to get Extra 30 secs....
        askedForExtraTime = value;
        canAskForExtraTime = false;
    }
    public void RemoveHoleFromList(HoleController hole){
        if(holeControllerList.Contains(hole)){
            holeControllerList.Remove(hole);
        }
    }
    
    
}
