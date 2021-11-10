using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject _follow;

	[SerializeField]
	private Vector2 _minCamPos, _maxCamPos;

	[SerializeField]
	private float _smoothTime;
	
	private Vector2 _velocity;

	void FixedUpdate () {
		float posX = Mathf.SmoothDamp(transform.position.x, _follow.transform.position.x, ref _velocity.x, _smoothTime);
		float posY = Mathf.SmoothDamp(transform.position.y,	_follow.transform.position.y, ref _velocity.y, _smoothTime);

		transform.position = new Vector3 (
			Mathf.Clamp(posX, _minCamPos.x, _maxCamPos.x), 
			Mathf.Clamp(posY, _minCamPos.y, _maxCamPos.y), 
			transform.position.z);
	}
}
