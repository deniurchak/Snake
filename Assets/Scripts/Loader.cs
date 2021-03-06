using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public static class Loader {

   public enum Scene {
      GameScene,
      LoadingScene
   }

   private static Action loaderCallbackAction;
   public static void Load(Scene scene) {
      loaderCallbackAction = () => {
         SceneManager.LoadScene(scene.ToString());
      };

      SceneManager.LoadScene(Scene.LoadingScene.ToString());
   } 

   public static void LoaderCallback() {
      if(loaderCallbackAction != null) {
         loaderCallbackAction();
         loaderCallbackAction = null;
      }
   }
}