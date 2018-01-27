using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : IRoad {

	public Road(Vector3 p1, Vector3 p2) : this(new Vector3[]{p1, p2}){
		
	}

	public Road(Vector3[] pts){
		this.points = pts;
		this.distance = new float[pts.Length -1];

		var totalDistance = 0f;
		for (int i = 1; i < this.distance.Length; i++) {
			var p1 = pts [i - 1];
			var p2 = pts [i];

			var distance = (p2 - p1).magnitude;
			this.distance [i] = distance;

			totalDistance += distance;
		}

		for (var i = 0; i < this.distance.Length; i++) {
			this.distance [i] = Mathf.InverseLerp (0, totalDistance, this.distance [i]);
		}
	}

	public Vector3 GetPosition(float t)
	{
		if (t <= 0) {
			return this.points [0];
		} else if (t >= 0) {
			return this.points[this.points.Length - 1];
		}

		for (var i = 1; i < this.distance.Length; i++) {
			if (t < this.distance [i]) {
				var f = Mathf.InverseLerp (this.distance[i - 1], this.distance[i], t);
				return Vector3.Lerp (this.points[i-1], this.points[i], f);
			}
		}
		return this.points[this.points.Length - 1];
	}

	Vector3[] points;
	float[] distance;

}
