﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Laser : Projectile {

	[Header("Laser specific params")]
	public Vector3 startPosition;
	public Vector3 endPosition;
	public Enemy hitTarget;
	public Transform muzzleTarget;
	public float startSize;
	public float endSize;
	public Color startColor;
	public Color endColor;

	LineRenderer lineRenderer;
	bool haveBeenHit = false;

	protected override void Start() {
		base.Start();
		lineRenderer = GetComponent<LineRenderer> ();

		lineRenderer.SetColors (startColor, endColor);
		lineRenderer.SetWidth (startSize, endSize);
		lineRenderer.SetVertexCount (2);
		lineRenderer.SetPosition (0, startPosition);
		lineRenderer.SetPosition (1, endPosition);
	}

	protected override void Update() {
		if (Time.time > killtime) {
			GameObject.Destroy (gameObject);
		}

		if (!haveBeenHit) {
			if (hitTarget) {
				hitTarget.TakeHit (damage, new RaycastHit());
				haveBeenHit = true;
			}
		}

		// Update position
		if(muzzleTarget)
			lineRenderer.SetPosition (0, muzzleTarget.transform.position);
		if(hitTarget)
			lineRenderer.SetPosition (1, hitTarget.transform.position);
	}
}
