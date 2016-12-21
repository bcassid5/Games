using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Basket : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
		Vector3 mousePos2D = Input.mousePosition;

		mousePos2D.z = -Camera.main.transform.position.z;

		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);
		Vector3 pos = this.transform.position;
		pos.x = mousePos3D.x;

		this.transform.position = pos;
	}
		
	void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;
		if(collidedWith.tag == "Apple"){
			Destroy (collidedWith);
		}
			
		Scoring.scr += 100;

		//track the highscore
		if (Scoring.scr > HighScore_AP.score) {
			HighScore_AP.score = Scoring.scr;
		}	
	}
}
