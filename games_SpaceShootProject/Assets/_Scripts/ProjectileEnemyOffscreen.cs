using UnityEngine;
using System.Collections;

public class ProjectileEnemyOffscreen : MonoBehaviour {

	void Awake () {
		InvokeRepeating ("CheckOffscreen",2f,2f);
	}

	void CheckOffscreen(){
		if (Utils.ScreenBoundsCheck (GetComponent<Collider> ().bounds, BoundsTest.offScreen) != Vector3.zero) {
			Destroy (this.gameObject);
		}
	}
}
