using Assets;
using UnityEngine;

public class CollisionListener : MonoBehaviour {

	// Update is called once per frame
  void OnTriggerEnter2D(Collider2D other) 
  {
    if (other.gameObject.tag == "Player")
    {
      if(tag == "enemy")
        GameManager.Instance.OnEnemyCollide();
      else
      {
        GameManager.Instance.OnBubleCollide(gameObject);
      }
    }
  }
}
