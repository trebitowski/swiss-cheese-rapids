using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

  /// <summary>
  /// UnityEngine.PlayerPrefs wrapper for WebGL LocalStorage
  /// https://gist.github.com/robertwahler/b3110b3077b72b4c56199668f74978a0
  /// </summary>
  public static class WebPlayerPrefs {

    [DllImport("__Internal")]
      private static extern void SaveToLocalStorage(string key, string value);

      [DllImport("__Internal")]
      private static extern string LoadFromLocalStorage(string key);

      [DllImport("__Internal")]
      private static extern void RemoveFromLocalStorage(string key);

      [DllImport("__Internal")]
      private static extern int HasKeyInLocalStorage(string key);

    public static void DeleteKey(string key) {
      Debug.Log(string.Format("SCR.PlayerPrefs.DeleteKey(key: {0})", key));

      #if UNITY_EDITOR
        UnityEngine.PlayerPrefs.DeleteKey(key: key);
      #elif UNITY_WEBGL
        RemoveFromLocalStorage(key: key);
      #else
        UnityEngine.PlayerPrefs.DeleteKey(key: key);
      #endif
    }

    public static bool HasKey(string key) {
      Debug.Log(string.Format("SCR.PlayerPrefs.HasKey(key: {0})", key));

      #if UNITY_EDITOR
        return (UnityEngine.PlayerPrefs.HasKey(key: key));
      #elif UNITY_WEBGL
        return (HasKeyInLocalStorage(key) == 1);
      #else
        return (UnityEngine.PlayerPrefs.HasKey(key: key));
      #endif
    }

    public static string GetString(string key) {
      Debug.Log(string.Format("SCR.PlayerPrefs.GetString(key: {0})", key));

      #if UNITY_EDITOR
        return (UnityEngine.PlayerPrefs.GetString(key: key));
      #elif UNITY_WEBGL
        return LoadFromLocalStorage(key: key);
      #else
        return (UnityEngine.PlayerPrefs.GetString(key: key));
      #endif
    }

    public static void SetString(string key, string value) {
      Debug.Log(string.Format("SCR.PlayerPrefs.SetString(key: {0}, value: {1})", key, value));

      #if UNITY_EDITOR
        UnityEngine.PlayerPrefs.SetString(key: key, value: value);
      #elif UNITY_WEBGL
        SaveToLocalStorage(key: key, value: value);
      #else
        UnityEngine.PlayerPrefs.SetString(key: key, value: value);
      #endif

    }

    public static void Save() {
      Debug.Log(string.Format("SCR.PlayerPrefs.Save()"));

      #if UNITY_EDITOR
        UnityEngine.PlayerPrefs.Save();
      #elif !UNITY_WEBGL
        UnityEngine.PlayerPrefs.Save();
      #endif
    }

      
  }