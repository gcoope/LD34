using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

	private Text timeText;

	private float milliseconds;
	private float seconds;
	private float minutes;

	private bool playing = false;

	void Awake() {		
		gameObject.AddGlobalEventListener(GameEvent.StartGame, StartGame);
		gameObject.AddGlobalEventListener(GameEvent.RestartGame, ResetTimer);
		gameObject.AddGlobalEventListener(GameEvent.EndGame, EndGame);
	}

	void Start () {
		timeText = gameObject.GetComponent<Text>();
		SetTimeText();
	}

	void FixedUpdate() {
		if(!playing) return;
		if(milliseconds > 99) {
			seconds++;
			milliseconds = 0;
		}

		if(seconds > 59) {
			minutes++;
			seconds = 0;
		}

		milliseconds += Time.deltaTime * 100;

		SetTimeText();
	}

	private void SetTimeText() {
		if(timeText != null) {
			if(minutes >= 1) {
				timeText.text = minutes.ToString("00") + ":"  + seconds.ToString("00") + ":" + milliseconds.ToString("00");
			} else {
				timeText.text = "   " + seconds.ToString("00") + ":" + milliseconds.ToString("00");
			}
		}
	}

	private void EndGame(EventObject evt) {
		playing = false;
	}

	private void StartGame(EventObject evt) {
		playing = true;
	}

	private void ResetTimer(EventObject evt) {
		milliseconds = 0;
		seconds = 0;
		minutes = 0;
		SetTimeText();
	}
}
