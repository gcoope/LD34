using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


	public GameObject uiPanel;
	public Text gameOverText;
	private CanvasGroup cGroup;

	void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.StartGame, Hide);
		gameObject.AddGlobalEventListener(GameEvent.EndGame, Show);
		gameObject.AddGlobalEventListener(GameEvent.RestartGame, Hide);
	}

	void Start () {
		cGroup = uiPanel.GetComponent<CanvasGroup>();
		uiPanel.SetActive(false);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
//			Application.LoadLevel(0);
			SceneManager.LoadScene(0);
		}
	}

	public void GoToMenu() {
//		Application.LoadLevel(0);
		SceneManager.LoadScene(0);
	}
	
	private void Show(EventObject evt) {

		// check if good or bad ending
		if(evt.Params != null) {
			if(evt.Params[0] != null) {
				if(evt.Params[0].ToString() == GameEvent.WonMessage) {
					gameOverText.text = "YOU WON!";
				} else {
					gameOverText.text = "YOU LOST!";
				}
			} else {
				gameOverText.text = "YOU LOST!";
			}
		} else {
			gameOverText.text = "YOU LOST!";
		}

		uiPanel.SetActive(true);
		float a = cGroup.alpha;
		DOTween.To(()=> a, x => a = x, 1, 0.75f).OnUpdate(()=> UpdateAlpha(a));
	}

	private void Hide(EventObject evt) {
		cGroup.alpha = 0;
		uiPanel.SetActive(false);
	}

	private void UpdateAlpha(float a) {
		cGroup.alpha = a;
	}
}
