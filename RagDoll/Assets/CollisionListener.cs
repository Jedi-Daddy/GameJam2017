﻿using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class CollisionListener : MonoBehaviour {

	// Update is called once per frame
  void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Player")
		  GameManager.Instance.OnEnemyCollide();
	}
}