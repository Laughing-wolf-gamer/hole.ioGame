using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialChanger : MonoBehaviour {

    public GameManager gameMangerInstance;
   
    List<GroupData> data;
	void Start () {
        data = gameMangerInstance.data;
        currentIndex = PlayerPrefs.GetInt("PlayerIndexMat", 0);//grabbing stored val with default 0

    }

    // Update is called once per frame
    public int currentIndex = 0;
    int lastIndex;
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftButton();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rightButton();
        }
        if (currentIndex > data.Count - 1) currentIndex = 1;
        if (currentIndex < 1) currentIndex = data.Count - 1;
        if ( lastIndex != currentIndex)
        {
			Material mat = data[currentIndex].leaderMaterial;
            ChangeMaterial(mat);
            lastIndex = currentIndex;
            data[0].leaderMaterial = mat;
            data[0].groupColor = mat.GetColor("_Color");
            PlayerPrefs.SetInt("PlayerIndexMat", currentIndex);//storing index
        }
        
    }
    public void leftButton()
    {
        currentIndex--;
    }
    public void rightButton()
    {
        currentIndex++;
    }
    public void ChangeMaterial(Material mat)
    {
        foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = mat;
        }
    }
}
