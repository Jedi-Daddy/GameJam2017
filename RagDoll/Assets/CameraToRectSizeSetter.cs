using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToRectSizeSetter : MonoBehaviour {

  public Camera Camera;
  public RectTransform CropRect;

  private void Awake()
  {
    var rectPos = new Vector2(CropRect.anchoredPosition.x / 1920f, CropRect.anchoredPosition.y / 1080f);
    var rectSize = new Vector2(CropRect.sizeDelta.x / 1920f, CropRect.sizeDelta.y / 1080f);
    Camera.rect = new Rect(rectPos, rectSize);
  }
}
