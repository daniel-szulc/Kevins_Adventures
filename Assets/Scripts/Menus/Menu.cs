using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
 public GameObject loading;
 public GameObject taptoplay;
 public TextMeshProUGUI loadingtxt;
 public GameObject mainMenu;
 public GameObject langMenu;
 public GameObject settingsButton;
 private AsyncOperation async;
 public GameObject dontDestroy;
 
 public void PlayGame()
 {
  taptoplay.SetActive(false);
  StartCoroutine(LoadScene());
 // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 }

 private void Start()
 {
 }
 
 IEnumerator LoadScene()
 {
  StartCoroutine(Loading());
  if(story_script.StoryDone)
  async = SceneManager.LoadSceneAsync("map");
 else
 {
  async = SceneManager.LoadSceneAsync("movie_start");
 }

  async.allowSceneActivation = false;
  yield return new WaitForSeconds(2);
  async.allowSceneActivation = true;
   yield return async;
 }

 private IEnumerator Loading()
 {
  loading.SetActive(true);

  if(languageMENU.lang==languageMENU.Language.en)
  { 
   loadingtxt.text ="     LOADING";
   yield return new WaitForSeconds(0.2f);
   
   while (true)
     {

      loadingtxt.text ="     LOADING.";
      yield return new WaitForSeconds(0.2f);
      loadingtxt.text = "     LOADING..";
      yield return new WaitForSeconds(0.2f);
      loadingtxt.text = "     LOADING...";
      yield return new WaitForSeconds(0.2f);
     }}
  else if(languageMENU.lang==languageMENU.Language.pl)
  { 
   loadingtxt.text = "   ŁADOWANIE";
   yield return new WaitForSeconds(0.2f);
   while (true)
   {
    loadingtxt.text = "   ŁADOWANIE.";
    yield return new WaitForSeconds(0.2f);
    loadingtxt.text = "   ŁADOWANIE..";
    yield return new WaitForSeconds(0.2f);
    loadingtxt.text = "   ŁADOWANIE...";
    yield return new WaitForSeconds(0.2f);
   }}
  else if(languageMENU.lang==languageMENU.Language.de)
  {
   loadingtxt.text = "WIRD GELADEN";
   yield return new WaitForSeconds(0.2f);
   while (true)
   {
    loadingtxt.text = "WIRD GELADEN.";
    yield return new WaitForSeconds(0.2f);
    loadingtxt.text = "WIRD GELADEN..";
    yield return new WaitForSeconds(0.2f);
    loadingtxt.text = "WIRD GELADEN...";
    yield return new WaitForSeconds(0.2f);
   }}
  
 }

 public void Settings()
 {
  langMenu.SetActive(true);
 // mainMenu.SetActive(false);
  settingsButton.SetActive(false);
 }
 public void QuitGame()
 {
  Debug.Log("Exit");
  Application.Quit();
 }
}
