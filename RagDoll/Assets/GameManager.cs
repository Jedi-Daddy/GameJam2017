using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
  public class GameManager
  {
    public static readonly GameManager Instance = new GameManager();

    private GameManager()
    {
    }

    public event Action EnemyCollide;

    public void OnEnemyCollide()
    {
      if (EnemyCollide != null)
        EnemyCollide();
    }
  }
}
