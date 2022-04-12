using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class KilledBy : MonoBehaviour {

	public TextMeshProUGUI rank, Killedname;

	public void KilledByMethod(string getName){
		Killedname.SetText(getName);
		//rank = 
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
