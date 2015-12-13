using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public GameObject aboutPanel;

	void Awake() {
		HideAbout();
	}

	public void StartGame() {		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Exit() {
		Application.Quit();
	}

	public void ShowAbout() {
		aboutPanel.SetActive(true);
	}

	public void HideAbout() {
		aboutPanel.SetActive(false);
	}

}
