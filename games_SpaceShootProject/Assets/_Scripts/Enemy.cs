﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed = 10f;
	public float health = 10;
	public float fireRate = 0.3f;
	static public int score = 100;


	public int showDamageForFrames = 2;

	public float powerUpDropChance = 1f;

	public bool __________;

	public Color[] originalColors;
	public Material[] materials;
	public int remainingDamageFrames = 0;

	public Bounds bounds;
	public Vector3 boundsCenterOffest;

	void Awake(){
		materials = Utils.GetAllMaterials (gameObject);

		originalColors = new Color[materials.Length];
		for (int i = 0; i < materials.Length; i++) {
			originalColors [i] = materials [i].color;
		}
		//enemyWeapon.SetType (WeaponType.blaster);

		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	// Update is called once per frame
	void Update () {
		Move ();
		if (remainingDamageFrames>0){
			remainingDamageFrames--;
			if (remainingDamageFrames == 0) {
				UnShowDamage ();
			}
		}
	}

	public virtual void Move(){
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;


	}

	public Vector3 pos {
		get{ 
			return (this.transform.position);
		}
		set{
			this.transform.position = value;
		}
	}

	void CheckOffscreen(){
		if (bounds.size == Vector3.zero) {
			bounds = Utils.CombineBoundsOfChildren (this.gameObject);
			boundsCenterOffest = bounds.center - transform.position;
		}

		bounds.center = transform.position + boundsCenterOffest;

		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);

		if (off != Vector3.zero) {
			if (off.y < 0) {
				Destroy (this.gameObject);
				Main.enemiesOnScreen--;
				//Debug.Log (Main.enemiesOnScreen);
			}
		}
	}

	//================ Collision Function ==================\\

	void OnCollisionEnter(Collision coll){
		GameObject other = coll.gameObject;

		switch (other.tag) {
		case "ProjectilePlayer":
			Projectile p = other.GetComponent<Projectile> ();
			bounds.center = transform.position + boundsCenterOffest;
			if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen) != Vector3.zero) {
				Destroy (other);
				break;
			}
			//hurt this enemy
			ShowDamage ();
			//get damage amount from type...
			health -= Main.W_DEFS [p.type].damageOnHit;

			if (health == 0 || health < -2) {
				Main.S.ShipDestroyed (this);
				Destroy (this.gameObject);
			}
			Destroy (other);
			break;
		}
	}

	void ShowDamage(){
		foreach (Material m in materials){
			m.color = Color.red;
		}
		remainingDamageFrames = showDamageForFrames;
	}

	void UnShowDamage(){
		for (int i = 0; i < materials.Length; i++) {
			materials [i].color = originalColors [i];
		}
	}

}
