using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
  public Camera MainCamera;
  public GameObject EnemyPrefab;
  public GameObject BublePrefab;
  public UnityEngine.UI.Text DepthUpdater;
  public UnityEngine.UI.Text OxygenUpdater;

  private Vector3 _cameraCenter;
  private float _halfOfCameraHeight;
  private float _halfOfCameraWidth;
  private float _enemyWidth;
  private Queue<GameObject> _enemiesPool;
  private List<GameObject> _enemiesOnStage;
  private Queue<GameObject> _bublesPool;
  private List<GameObject> _bublesOnStage;
  private float _lastResetTime;
  private float _timer;
  private float _currentDepth;
  private int _oxygen;
  
	// Use this for initialization
	void Start () {
    _halfOfCameraHeight = MainCamera.orthographicSize;
    _halfOfCameraWidth = _halfOfCameraHeight * MainCamera.aspect;
    _cameraCenter = MainCamera.transform.position;
	  _enemyWidth = EnemyPrefab.GetComponent<Collider2D>().bounds.size.x;
	  SetInitialState();
    GameManager.Instance.EnemyCollide += SetInitialState;
    GameManager.Instance.BubleCollide += OnBubleCollide;
	}

  private void OnBubleCollide(GameObject buble)
  {
    _oxygen += 10;
    OxygenUpdater.text = _oxygen + "bar";
    _bublesOnStage.Remove(buble);
    Destroy(buble);
    var newBuble = Instantiate(BublePrefab);
    _bublesPool.Enqueue(newBuble);
  }

  // Update is called once per frame
	void FixedUpdate()
	{
	  _timer += Time.deltaTime;
	  if (Time.frameCount % 100 == 0)
	  {
	    _oxygen -= 10;
      if(_oxygen <= 0)
        SetInitialState();
      OxygenUpdater.text = _oxygen + "bar";
	  }
	  
    _currentDepth = Time.time - _lastResetTime;
	  DepthUpdater.text = _currentDepth.ToString("F1") +"m";
    if (_timer > Random.Range(0f, 1f) * 8)
	  {
	    SpawnObject();
	    _timer = 0;
	  }

	  foreach (var o in _enemiesOnStage)
	  {
	    if (IsOutOfCamera(o))
	    {
	      _enemiesPool.Enqueue(o);
	    }
	  }
	  _enemiesOnStage.RemoveAll(IsOutOfCamera);

    foreach (var o in _bublesOnStage)
    {
      if (IsOutOfCamera(o))
      {
        _bublesPool.Enqueue(o);
      }
    }
    _bublesOnStage.RemoveAll(IsOutOfCamera);
	}

  private void SetInitialState()
  {
    if (_enemiesOnStage != null)
    {
      foreach (var o in _enemiesOnStage)
      {
        Destroy(o);
      }
    }
    if (_enemiesPool != null)
    {
      foreach (var o in _enemiesPool)
      {
        Destroy(o);
      }
    }

    if (_bublesOnStage != null)
    {
      foreach (var o in _bublesOnStage)
      {
        Destroy(o);
      }
    }
    if (_bublesPool != null)
    {
      foreach (var o in _bublesPool)
      {
        Destroy(o);
      }
    }

    _lastResetTime = Time.time;
    DepthUpdater.text = "0m";
    _oxygen = 200;
    OxygenUpdater.text = _oxygen + "bar";

    _enemiesPool = new Queue<GameObject>();
    for (var i = 0; i < 20; i++)
    {
      var newEnemy = Instantiate(EnemyPrefab);
      _enemiesPool.Enqueue(newEnemy);
    }
    _enemiesOnStage = new List<GameObject>();

    _bublesPool = new Queue<GameObject>();
    for (var i = 0; i < 20; i++)
    {
      var newBuble = Instantiate(BublePrefab);
      _bublesPool.Enqueue(newBuble);
    }
    _bublesOnStage = new List<GameObject>();
  }

  private void SpawnObject()
  {
    var rand = Random.Range(0f, 1f);
    if (rand > 0.5)
    {
      if (_enemiesPool.Count == 0)
        return;

      var newEnemy = _enemiesPool.Dequeue();
      var rangeResult = Random.Range(0f, 1f);
      var xOffsetCoef = rangeResult > 0.5 ? 1 : -1;
      var xOffset = Random.Range(0, _halfOfCameraWidth - _enemyWidth)*xOffsetCoef;
      newEnemy.transform.localPosition = new Vector3(_cameraCenter.x + xOffset,
        _cameraCenter.y - _halfOfCameraHeight - 10);
      newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8);
      _enemiesOnStage.Add(newEnemy);
    }
    else
    {
      if (_bublesPool.Count == 0)
        return;

      var newBuble = _bublesPool.Dequeue();
      var rangeResult = Random.Range(0f, 1f);
      var xOffsetCoef = rangeResult > 0.5 ? 1 : -1;
      var xOffset = Random.Range(0, _halfOfCameraWidth - _enemyWidth) * xOffsetCoef;
      newBuble.transform.localPosition = new Vector3(_cameraCenter.x + xOffset,
        _cameraCenter.y - _halfOfCameraHeight - 10);
      newBuble.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8);
      _bublesOnStage.Add(newBuble);
    }
  }

  private bool IsOutOfCamera(GameObject obj)
  {
    var screenPoint = MainCamera.WorldToViewportPoint(obj.transform.localPosition);
    return screenPoint.y > 1;
  }
}
