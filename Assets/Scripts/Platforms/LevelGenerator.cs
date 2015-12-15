using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class LevelGenerator : MonoBehaviour {

	public GameObject startPlatform;
	public GameObject platformPrefab;
	public GameObject endPlatformPrefab;
	private int amountToSpawn = 100;
	private float blockHeight = 0.2f;
	private float blockWidth = 0.5f;

	private Vector3 lastPos;

	private List<GameObject> spawnedBlocks;

	void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.RestartGame, Restart);
	}

	void Start() {
		spawnedBlocks = new List<GameObject>(amountToSpawn);
		MakeLevel();
	}

	private void MakeLevel() {
		for(int i = 0; i < amountToSpawn; i++) {

			GameObject g;

			if(i == amountToSpawn - 1) {
				g = Instantiate<GameObject>(endPlatformPrefab);
			} else if(i == 0) {
				g = Instantiate<GameObject>(startPlatform);
			} else {
				g = Instantiate<GameObject>(platformPrefab);
			}

			g.transform.parent = transform;
			spawnedBlocks.Add(g);
			if(i == 0) {
				lastPos = g.transform.position;
				continue;
			} 

			if(i < amountToSpawn) {
				if(g.GetComponent<Platform>() != null && i != amountToSpawn - 1) {

					float chanceOfObstacle = 1f;

					if(i > 20) {
						chanceOfObstacle = 0.75f; // 20%
					} 
					if(i > 40) {
						chanceOfObstacle = 0.6f;
					} 
					if(i > 60) {
						chanceOfObstacle = 0.5f;
					} 
					if(i > 80) {
						chanceOfObstacle = 0.4f;
					}

					float c = UnityEngine.Random.value;
					if(c > chanceOfObstacle) {
						g.GetComponent<Platform>().EnabledAsObstacle();
					}


				}
			}

			int left = UnityEngine.Random.Range(0, 2);
			if(left == 1) {
				g.transform.position = new Vector3(lastPos.x - blockWidth, lastPos.y + blockHeight, lastPos.z);
			} else {
				g.transform.position = new Vector3(lastPos.x, lastPos.y + blockHeight, lastPos.z + blockWidth);
			}

			lastPos = g.transform.position;

			if(i < 25) { // Fancy animation in
				float endPos = g.transform.position.y;
				g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - blockHeight * 3, g.transform.position.z);
				g.transform.DOLocalMoveY(endPos, 0.3f).SetDelay(i * 0.1f);//.SetEase(Ease.InOutExpo);
			}

		}
	}

	private void Restart(EventObject evt) {
		foreach(GameObject g in spawnedBlocks) {
			Destroy(g);
		}
		spawnedBlocks.Clear();
		MakeLevel();
	}
}
