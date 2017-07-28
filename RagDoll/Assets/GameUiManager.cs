using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
  public Camera MainCamera;
  public GameObject EnemyPrefab;

  private Vector3 _cameraCenter;
  private float _halfOfCameraHeight;
  private float _halfOfCameraWidth;
  private float _enemyWidth;
  private Queue<GameObject> _objectsPool;
  private List<GameObject> _objectsOnStage;
  private float _timer;
  
	// Use this for initialization
	void Start () {
    _halfOfCameraHeight = MainCamera.orthographicSize;
    _halfOfCameraWidth = _halfOfCameraHeight * MainCamera.aspect;
    _cameraCenter = MainCamera.transform.position;
	  _enemyWidth = EnemyPrefab.GetComponent<Collider2D>().bounds.size.x;
	  SetInitialState();
	  GameManager.Instance.EnemyCollide += SetInitialState;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
	  _timer += Time.deltaTime;
    if (_timer > Random.Range(0f, 1f) * 6)
	  {
	    SpawnEnemy();
	    _timer = 0;
	  }
	  foreach (var o in _objectsOnStage)
	  {
	    if (IsOutOfCamera(o))
	    {
	      _objectsPool.Enqueue(o);
	    }
	  }
	  _objectsOnStage.RemoveAll(IsOutOfCamera);
	}

  private void SetInitialState()
  {
    if (_objectsOnStage != null)
    {
      foreach (var o in _objectsOnStage)
      {
        Destroy(o);
      }
    }
    if (_objectsPool != null)
    {
      foreach (var o in _objectsPool)
      {
        Destroy(o);
      }
    }

    _objectsPool = new Queue<GameObject>();
    for (var i = 0; i < 20; i++)
    {
      var newEnemy = Instantiate(EnemyPrefab);
      _objectsPool.Enqueue(newEnemy);
    }
    _objectsOnStage = new List<GameObject>();
  }

  private void SpawnEnemy()
  {
    if (_objectsPool.Count == 0)
      return;

    var newEnemy = _objectsPool.Dequeue();
    var rangeResult = Random.Range(0f, 1f);
    var xOffsetCoef = rangeResult > 0.5 ? 1 : -1;
    var xOffset = Random.Range(0, _halfOfCameraWidth - _enemyWidth) * xOffsetCoef;
    newEnemy.transform.localPosition = new Vector3(_cameraCenter.x + xOffset, _cameraCenter.y - _halfOfCameraHeight - 10);
    newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
    _objectsOnStage.Add(newEnemy);
  }

  private bool IsOutOfCamera(GameObject obj)
  {
    var screenPoint = MainCamera.WorldToViewportPoint(obj.transform.localPosition);
    return screenPoint.y > 1;
  }
}
