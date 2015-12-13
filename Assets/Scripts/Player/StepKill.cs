using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StepKill : MonoBehaviour {

	private float liveTime = 0.5f;

	public void Kill() {
		StartCoroutine("WaitAndDie");
	}

	IEnumerator WaitAndDie() {
		yield return new WaitForSeconds(liveTime);
		gameObject.SetActive(false);
	}
}
