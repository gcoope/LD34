using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody rBody;
	private Vector3 startPos;
	private bool canJump = true;
	private float jumpTime = 0.15f;
	private bool gameOver = false;
	private float halfScreenWidth;
	private float bottomSideOfScreen;
	private bool gameStarted = false;

	public SoundManager soundManager;

	void Start() {
		DOTween.Init();
		rBody = gameObject.GetComponent<Rigidbody>();
		startPos = transform.position;
		halfScreenWidth = Screen.width * 0.5f;
		bottomSideOfScreen = Screen.height * 0.75f;
	}

	void Update() {
		RecieveInput();

		if(rBody.velocity.magnitude > 8) { // falling
			if(!gameOver) {
				soundManager.PlayGameLoseSound();
				gameObject.DispatchGlobalEvent(GameEvent.EndGame);
				gameOver = true;
			}
		}
	}

	private void RecieveInput() {	
		if(Input.GetKeyDown(KeyCode.LeftArrow)) Left();
		else if(Input.GetKeyDown(KeyCode.RightArrow)) Right();
		if(Input.GetKeyDown(KeyCode.R)) Restart();

		if(Input.GetMouseButtonDown(0)) {
			if(Input.mousePosition.y < bottomSideOfScreen) {
				if(Input.mousePosition.x < halfScreenWidth)	Left(); 
				else Right();
			}
		}
	}

	private void Left() {
		if(!gameStarted) FirstStart();
		if(!gameOver) LeftJump();
	}

	private void Right() {
		if(!gameStarted) FirstStart();
		if(!gameOver) RightJump();
	}

	private void FirstStart() {
		gameStarted = true;
		gameOver.DispatchGlobalEvent(GameEvent.StartGame);
	}

	public void Restart() {
		gameStarted = false;
		canJump = true;
		gameOver = false;
		rBody.velocity = Vector3.zero;
		transform.position = startPos;
		gameObject.DispatchGlobalEvent(GameEvent.RestartGame);
	}

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.CompareTag("EndPlatform")) {
			if(!gameOver) {
				soundManager.PlayGameWinSound();
				gameObject.DispatchGlobalEvent(GameEvent.TookStep);
				gameObject.DispatchGlobalEvent(GameEvent.EndGame, new object[]{ GameEvent.WonMessage });
				gameOver = true;
				col.gameObject.GetComponent<Platform>().PlayParticles();
			}
		} 
		else if(col.gameObject.CompareTag("Spike")) {
			if(!gameOver) {
				soundManager.PlayGameLoseSound();
				gameObject.DispatchGlobalEvent(GameEvent.EndGame);		
				canJump = false;
				gameOver = true;
			}
		}
		else if(col.gameObject.CompareTag("Platform")) {
			if(gameOver) return;
			soundManager.PlayStepSound();
			gameObject.DispatchGlobalEvent(GameEvent.TookStep);
			col.gameObject.GetComponent<Platform>().PlayParticles();
			canJump = true;
		}
	}

	private void LeftJump() {
		if(!canJump) return;
		canJump = false;
		rBody.DOJump(new Vector3(transform.position.x - 0.5f, transform.position.y + 0.15f, transform.position.z), 0.5f, 1, jumpTime);
//		transform.eulerAngles = Vector3.zero;
	}

	private void RightJump() {
		if(!canJump) return;
		canJump = false;
		rBody.DOJump(new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z + 0.5f), 0.5f, 1, jumpTime);
//		transform.eulerAngles = new Vector3(0,90,0);
	}
}
