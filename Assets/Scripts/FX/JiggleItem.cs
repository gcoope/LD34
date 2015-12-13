using UnityEngine;
using System.Collections;
using DG.Tweening;

public class JiggleItem : MonoBehaviour {


	void Start() {
		StartCoroutine("JiggleMe");
	}

	IEnumerator JiggleMe() {
		while(true) {
			yield return new WaitForSeconds(Random.Range(1f, 4f));
			transform.DOPunchScale(new Vector3(0.75f,0.75f,0.75f), 0.3f, 6);
		}
	}
}
