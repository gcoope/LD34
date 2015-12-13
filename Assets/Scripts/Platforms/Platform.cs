using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Platform : MonoBehaviour {

	private Renderer myRenderer;
	private Collider myCollider;
	private Color startColour;
	[SerializeField] private Material playerMat;
	public ParticleSystem particles;

	void Awake() {
		gameObject.AddGlobalEventListener(GameEvent.EndGame, StopStuff);
		myRenderer = gameObject.GetComponent<Renderer>();
		myCollider = gameObject.GetComponent<Collider>();
		startColour = myRenderer.material.color;
	}

	public void EnabledAsObstacle() {
		// Choose randomly to be a thing - choose percentage chance for each type
		int chance = Random.Range(0,100);

		if(chance > 70) {
			int type = Random.Range(0, 2);

			if(type == 0) {
				StartBlinkingObstacle();
			} else if(type == 1) {
				AddSpikesAndBeginSpiking();
			}
		}
	}

	public void PlayParticles() {
		particles.Play();
		myRenderer.material.color = playerMat.color;
		startColour = playerMat.color;
	}

	private void StopStuff(EventObject evt) {
		StopCoroutine("BeInactive");
		StopCoroutine("BeActive");
		DOTween.Kill(myRenderer.material);
	}

	// Blinking obstacle --------------------

	private void StartBlinkingObstacle() {
		startColour = myRenderer.material.color;
		myRenderer.material.DOColor(Color.black, 2f).OnComplete(()=>{
			myRenderer.enabled = false;
			myCollider.enabled = false;
			StartCoroutine("BeInactive");
		}).SetDelay(Random.Range(1,4f));
	}

	IEnumerator BeInactive() {
		yield return new WaitForSeconds(1);
		myRenderer.enabled = true;
		myRenderer.material.DOColor(startColour, 0.5f).OnComplete(()=>{ StartCoroutine("BeActive"); });
	}

	IEnumerator BeActive() {
		myCollider.enabled = true;
		yield return new WaitForSeconds(3);
		StartBlinkingObstacle();
	}

	// Spike obstacle --------------------------

	private void AddSpikesAndBeginSpiking() {
		GameObject spike = Instantiate<GameObject>(Resources.Load<GameObject>("Spikes"));
		spike.transform.SetParent(transform);
		spike.transform.DOLocalMoveY(-1.75f, 1.5f).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(0,4));
	}

}
