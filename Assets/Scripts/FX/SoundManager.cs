using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SoundManager : MonoBehaviour {

	public AudioClip[] soundClips;
	private AudioSource audioSource;

	// Steps
	private float stepPitch = 1;

	void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.RestartGame, Reset);
	}

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();
	}


	public void PlayGameLoseSound() {
		audioSource.clip = soundClips[0];
		audioSource.pitch = 1;
		audioSource.Play();
	}

	public void PlayGameWinSound() {
		audioSource.clip = soundClips[1];
		audioSource.pitch = 1;
		audioSource.Play();
	}

	public void PlaySelectSound() {
		audioSource.clip = soundClips[2];
		audioSource.pitch = 1;
		audioSource.Play();
	}

	public void PlayStepSound() {
		audioSource.clip = soundClips[3];
		audioSource.Play();
		stepPitch += 0.018f;
		audioSource.pitch = stepPitch;

	}

	private void Reset(EventObject evt) {
		stepPitch = 1;
		audioSource.pitch = stepPitch;
	}

}
