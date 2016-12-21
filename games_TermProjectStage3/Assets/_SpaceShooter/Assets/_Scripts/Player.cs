using UnityEngine;
using System.Collections;



public class Player : MonoBehaviour {

	[SerializeField]
	private float _shieldLevel = 1;

	public float gameRestartDelay = 2f;

	static public Player S;

	//ship movement
	public float speed = 30;
	public float rollMult = -45;
	public float pitchMult = 30;

	//weapons
	public Weapon[] weapons;

	//ship status
	public bool __________;

	public Bounds bounds;

	public delegate void WeaponFireDelegate ();
	public WeaponFireDelegate fireDelegate;

	void Awake(){
		S = this; //sets singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);

	}

	void Start(){
		ClearWeapons ();
		weapons [0].SetType (WeaponType.blaster);
		S = this; //sets singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}

	
	// Update is called once per frame
	void Update () {
		//get info
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");

		//change position based on axis
		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;

		bounds.center = transform.position;

		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.onScreen);
		if (off != Vector3.zero) {
			pos -= off;
			transform.position = pos;
		}

		//rotate ship
		transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

		if (Input.GetAxis ("Jump") == 1 && fireDelegate != null) {
			fireDelegate ();
		}
	}

	public GameObject lastTriggerGo = null;

	void OnTriggerEnter(Collider other){

		GameObject go = Utils.FindTaggedParent (other.gameObject);

		if (go != null) {
			if (go == lastTriggerGo) {
				return;
			}
			lastTriggerGo = go;

			if (go.tag == "Enemy") {
				shieldLevel--;
				Destroy (go);
				Main.enemiesOnScreen--;
				Debug.Log (Main.enemiesOnScreen);
			} else if (go.tag == "PowerUp"){
				AbsorbPowerUp (go);
			} else if (go.tag == "ProjectileEnemy"){ //adding this code for enemy projectiles
				shieldLevel--;
				Destroy (go);
			}
			else {
				print ("Triggered: " + go.name);
			}
		} else {
			print ("Triggered: " + other.gameObject.name);
		}
	}

	public void AbsorbPowerUp (GameObject go){
		PowerUp pu = go.GetComponent<PowerUp> (); //getcomponent<powerup> - problem here
		switch (pu.type) {
		case WeaponType.shield:
			shieldLevel++;
			break;
		
		default:
			if (pu.type == weapons [0].type) {
				Weapon w = GetEmptyWeaponSlot ();

				if (w != null) {
					w.SetType (pu.type);
				}
			} else {
				ClearWeapons ();
				weapons [0].SetType (pu.type);
			}
			break;
		}
		pu.AbsorbedBy (this.gameObject);
	}

	Weapon GetEmptyWeaponSlot(){
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].type == WeaponType.none){
				return (weapons[i]);
			}
		}
		return null;
	}

	void ClearWeapons(){
		foreach (Weapon w in weapons) {
			w.SetType (WeaponType.none);
		}
	}

	//shieldLevel function

	public float shieldLevel {
		get{ 
			return(_shieldLevel);
		}
		set{
			_shieldLevel = Mathf.Min (value, 4);
			if (value < 0) {
				Destroy (this.gameObject);
				Main.S.DelayedRestart (gameRestartDelay);
			}
		}
	}
}
