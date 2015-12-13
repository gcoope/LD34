using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour {

	void Start () {	
		if(System.DateTime.Today.Month != 12) {
			gameObject.SetActive(false);
		}
	}

}
