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

    public event Action EnemyCollide;
    public event Action<GameObject> BubleCollide;

    public void OnEnemyCollide()
    {
      if (EnemyCollide != null)
        EnemyCollide();
    }

    public void OnBubleCollide(GameObject buble)
    {
      if (BubleCollide != null)
        BubleCollide(buble);
    }
  }
}
