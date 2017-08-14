using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
  public class GameManager
  {
    public static readonly GameManager Instance = new GameManager();

    private GameManager()
    {
    }

		public event Action<GameObject> EnemyCollide;
    	public event Action<GameObject> BubleCollide;

		public void OnEnemyCollide(GameObject enemy)
		{
			if (EnemyCollide != null)
				EnemyCollide (enemy);
		}

    public void OnBubleCollide(GameObject buble)
		{
			if (BubleCollide != null)
				BubleCollide (buble);
		}
	}
}
