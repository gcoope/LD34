using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private int savedHighscore;
	private int currentScore;

	public Text youScoredText;
	public Text highScoreText;
	public Text youGotHighscoreText;

	void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.SendLastScore, HandleSentScore);
	}

	void Start () {
		if(PlayerPrefs.HasKey("Highscore")) {
			savedHighscore = PlayerPrefs.GetInt("Highscore");
		}
		youGotHighscoreText.text = "";
	}

	void Update() {
		if(Input.GetKey(KeyCode.F1)) {
			PlayerPrefs.SetInt("Highscore", 0);
			savedHighscore = 0;
		}
	}

	private void HandleSentScore(EventObject evt) {
		if(evt.Params != null) {
			currentScore = (int)evt.Params[0];
		}
		SetScoreValues();
	}

	public void SetScoreValues() {
		youScoredText.text = "You scored: " +currentScore.ToString();
		highScoreText.text = "Best: " +  (currentScore < savedHighscore ? savedHighscore.ToString() : currentScore.ToString());
		if(currentScore >= savedHighscore) {
			PlayerPrefs.SetInt("Highscore", currentScore);
			savedHighscore = currentScore;
			youGotHighscoreText.text = "New highscore!";
		} else {
			youGotHighscoreText.text = "";
		}
	}
}
