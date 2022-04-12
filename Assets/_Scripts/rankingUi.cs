using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class rankingUi : MonoBehaviour
{
    public GameObject dummyPlaybutton;
    public TextMeshProUGUI firstText;
    public TextMeshProUGUI secondText;
    public TextMeshProUGUI thirdText;
    public List<GroupData> data;
    // Use this for initialization
    void Start()
    {
        firstText.text = "";
        secondText.text = "";
        thirdText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameStarted)
        {
            dummyPlaybutton.SetActive(false);
            data = GameManager.Instance.sortedDataForUI;
            if (data.Count > 1)
            {
                firstText.text = "" + data[0].LeaderName + "  " + data[0].score;
                firstText.color = data[0].groupColor;
            }
            if (data.Count > 1)
            {
                secondText.text = "" + data[1].LeaderName + "  " + data[1].score;
                secondText.color = data[1].groupColor;
            }
            if (data.Count > 2)
            {

                thirdText.text = "" + data[2].LeaderName + "  " + data[2].score;
                thirdText.color = data[2].groupColor;
            }
        }
    }
}
