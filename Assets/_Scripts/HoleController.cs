using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HoleController : MonoBehaviour{
    
//    [SerializeField] private Transform map;
//    [SerializeField] private Vector2 maxMapSize;
    [SerializeField] private GameObject crown;
    [SerializeField] protected GroupData groupData;
    [SerializeField] private float maxHoleSize;
    [SerializeField] protected float increaseSizeAmount = 2,maxPointToIncreaseSize = 10;
    [SerializeField] protected float holeMoveSpeed;
    [SerializeField] protected int currentScore;
    [SerializeField] protected bool isGameStart;

    #region Events........

    public Action onSizeChange;
    public Action OnObjectFallInHole;
    

    #endregion

    protected virtual void Awake(){
        groupData.indicatorInstance.setUpIndicator(groupData);
    }

    protected virtual void Start(){
        GameHandler.i.onGameEnd += () => {
            SetIsGameStart(false);
            holeMoveSpeed = 0f;
        };
        GameHandler.i.onTimerResume += () =>{
            SetIsGameStart(true);
        };
        GameHandler.i.onTimerUp += () =>{
            SetIsGameStart(false);
        };
        

    }
    [ContextMenu("Die")]
    public void SetDeath(){
        groupData.isDead = true;
        groupData.indicatorInstance.OnDeath();
        GameHandler.i.RemoveHoleFromList(this);

    }
    public void IncreaseScore(int amount){
        currentScore += amount;
        OnObjectFallInHole?.Invoke();
        if((currentScore % maxPointToIncreaseSize) == 0){
            
            StartCoroutine(ScaleHole());
        }
        groupData.score = currentScore;
        groupData.indicatorInstance.UpdateText();
    }
    private IEnumerator ScaleHole(){
        if(transform.localScale.x > maxHoleSize){
            onSizeChange?.Invoke();
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * increaseSizeAmount;
        float t = 0f;
        while(t <= 0.1f){
            t += Time.deltaTime * 0.2f;
            transform.localScale = Vector3.Lerp(startScale,endScale,t);
            
            if(transform.localScale.x > maxHoleSize){
                transform.localScale = new Vector3(maxHoleSize,maxHoleSize,maxHoleSize);
                break;
            }
            yield return null;
        }
        transform.position = new Vector3(transform.position.x,transform.localScale.y - 0.71f,transform.position.z);
        if(transform.localScale.x > maxHoleSize){
            transform.localScale = new Vector3(maxHoleSize,maxHoleSize,maxHoleSize);
        }
        maxPointToIncreaseSize += 10;
        onSizeChange?.Invoke();
        
    }
/*    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(map.position,new Vector3(maxMapSize.x,0f,maxMapSize.y));
    } */
    public void SetIsGameStart(bool value){
        isGameStart = value;
    }
    public bool GetIsDead(){
        return groupData.isDead;
    }
    public void ShowHideCrown(bool value){
        crown.SetActive(value);
    }
    public int GetScore(){
        return groupData.score;
    }
    public GroupData GetHolesGroupData(){
        return groupData;
    }
    public void SetKillCount(){
        groupData.KillCount++;
    }
}
