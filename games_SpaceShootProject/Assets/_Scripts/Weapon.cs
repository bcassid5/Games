using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
//NI means it has not been implemented, though an attempt was made
public enum WeaponType{
	none,
	blaster, 
	spread,
	phaser, //attempted implementation.. NI
	missile,//NI
	laser,
	bomb,
	shield
}

[System.Serializable]
public class WeaponDefinition{
	public WeaponType type = WeaponType.none;
	public string letter;
	public Color color = Color.white;
	public GameObject projectilePrefab;
	public Color projectileColor = Color.white;
	public float damageOnHit = 0;
	public float continuousDamage = 0;
	public float delayBetweenShots = 0;
	public float velocity = 20;
}

public class Weapon : MonoBehaviour {

	static public Transform PROJECTILE_ANCHOR;


	public bool ____________;

	[SerializeField]
	private WeaponType _type = WeaponType.none;
	public WeaponDefinition def;
	public GameObject collar;
	public float lastShot;

	private AudioSource shootingSound;
	public AudioSource laser;
	public AudioSource gun;

	void Awake(){
		collar = transform.Find ("Collar").gameObject;
		if (PlayerPrefs.HasKey ("SpaceShootShootMusic")) {
			if (PlayerPrefs.GetString ("SpaceShootShootMusic") == "Laser") {
				shootingSound = laser;
			} else {
				shootingSound = gun;
			}
		} else {
			shootingSound = laser;
		}

	}

	// Use this for initialization
	void Start () {
		
		SetType (_type);

		if (PROJECTILE_ANCHOR == null) {
			GameObject go = new GameObject ("_Projectile_Anchor");
			PROJECTILE_ANCHOR = go.transform;
		}

		GameObject parentGO = transform.parent.gameObject;

		if (parentGO.tag == "Player") {
			Player.S.fireDelegate += Fire;
		}

		if (PlayerPrefs.HasKey ("SpaceShootShootVolume")) {
			shootingSound.volume = PlayerPrefs.GetFloat ("SpaceShootShootVolume");
		} else {
			shootingSound.volume = 0.5f;
		}
	}

	public WeaponType type{
		get{ return (_type); }
		set{ SetType (value); }
	}
	
	public void SetType(WeaponType wt){
		_type = wt;
		if (type == WeaponType.none) {
			this.gameObject.SetActive (false);
			return;
		} else {
			this.gameObject.SetActive (true);
		}

		def = Main.GetWeaponDefinition (_type);
		collar.GetComponent<Renderer> ().material.color = def.color;
		lastShot = 0;


	}

	public void Fire(){
		if (!gameObject.activeInHierarchy)
			return;

		if (Time.time - lastShot < def.delayBetweenShots)
			return;

		Projectile p;

		switch (type) {
		case WeaponType.blaster:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = Vector3.up * def.velocity;
			break;

		case WeaponType.spread:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = Vector3.up * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = new Vector3 (-0.2f, 0.9f, 0) * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = new Vector3 (0.2f, 0.9f, 0) * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = new Vector3 (-0.6f, 0.5f, 0) * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = new Vector3 (0.6f, 0.5f, 0) * def.velocity;
			break;
		/*
		case WeaponType.phaser:
			p = MakeProjectile ();
			Vector3 tempPos = collar.transform.position;
			float age = Time.time;
			float theta = Mathf.PI * 2 * age;
			float sin = Mathf.Sin (theta);
			p.GetComponent<Rigidbody> ().velocity = new Vector3 (sin,1,0) * def.velocity;
		
			break;*/
		/*
		case WeaponType.missile:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = Vector3.up * def.velocity;
			break;
		*/
		case WeaponType.laser:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = Vector3.up * def.velocity;
			break;

		case WeaponType.bomb:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody> ().velocity = Vector3.up * def.velocity;
			break;
		}
	}

	public Projectile MakeProjectile(){
		GameObject go = Instantiate (def.projectilePrefab) as GameObject;

		if (transform.parent.gameObject.tag == "Player") {
			go.tag = "ProjectilePlayer";
			go.layer = LayerMask.NameToLayer ("ProjectilePlayer");
		} else {
			go.tag = "ProjectileEnemy";
			go.layer = LayerMask.NameToLayer ("ProjectileEnemy");
		}
		go.transform.position = collar.transform.position;
		go.transform.parent = PROJECTILE_ANCHOR;

		Projectile p = go.GetComponent<Projectile> ();
		p.type = type;

		shootingSound.Play ();

		lastShot = Time.time;
		return (p);
	}
}
