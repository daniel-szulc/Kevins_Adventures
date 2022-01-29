using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
 public GameObject _obj;
 private void OnTriggerEnter2D(Collider2D other)
 {
  if(other.CompareTag("Player"))
  _obj.SetActive(true);
 }
}
