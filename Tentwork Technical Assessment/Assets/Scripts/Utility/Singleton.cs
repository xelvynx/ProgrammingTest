﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }
    public static bool IsInitialize
    {
        get { return instance != null; }
    }
    #region Unity Methods
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("[Singleton] tring to instantiate a second instance of a singleton class.");
        }
        else
        {
            instance = (T)this;
        }
    }
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            instance = null;
        }
    }
    #endregion
}
