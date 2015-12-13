using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StepCounter : MonoBehaviour {
	
	private int steps = 0;

	public GameObject stepCountMeshPrefab;
	private GameObject[] stepMeshes;

	void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.TookStep, PopupStep);
		gameObject.AddGlobalEventListener(GameEvent.EndGame, GetSteps);
		gameObject.AddGlobalEventListener(GameEvent.RestartGame, ResetSteps);
	}

	void Start () {
		stepMeshes = new GameObject[100];
		for(int i = 0; i < 100; i++) {
			GameObject s = Instantiate<GameObject>(stepCountMeshPrefab);
			s.transform.SetParent(transform);
			s.transform.localScale = new Vector3(0.125f, 0.15f, 0.15f);
			s.transform.localPosition = Vector3.zero;
			s.GetComponent<TextMesh>().text = (i + 1).ToString();
			stepMeshes[i] = s;
			s.SetActive(false);
		}
	}

	private void ResetSteps(EventObject evt) {
		steps = 0;
		for(int i = 0; i < stepMeshes.Length; i++) {
			stepMeshes[i].transform.localPosition = Vector3.zero;
		}
	}
	
	private void PopupStep(EventObject evt) {
		GameObject newStep = stepMeshes[steps];
		newStep.SetActive(true);
		newStep.GetComponent<StepKill>().Kill();
		newStep.transform.localPosition = Vector3.zero;// new Vector3(transform.position.x, transform.position.y, transform.position.z);
		newStep.transform.DOMoveY(transform.position.y + 1.5f, 1f).SetEase(Ease.OutExpo);
		steps++;
	}

	private void GetSteps(EventObject evt) {
		gameObject.DispatchGlobalEvent(GameEvent.SendLastScore, new object[]{steps});
	}
}
