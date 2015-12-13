using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour {
	
    public Transform target;
	private float damping = 2;
	private float height = 8;

	private bool followTarget = true;
	private Vector3 startPos;

    void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.EndGame, EndGame);
		gameObject.AddGlobalEventListener(GameEvent.RestartGame, RestartGame);
		startPos = transform.position;
    }

    private void Update() {
		if(target == null || !followTarget) return;
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + 10, target.transform.position.y + height, target.position.z - 10), Time.deltaTime * damping);
    }

	private void EndGame(EventObject evt) {
		followTarget = false;
	}

	private void RestartGame(EventObject evt) {
		followTarget = true;
		transform.position = startPos;
	}

}
