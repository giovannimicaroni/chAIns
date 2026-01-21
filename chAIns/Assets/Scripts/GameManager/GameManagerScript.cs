using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance {  get; private set; }
    public int Gold { get; private set; } = 0;

    public Text goldText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int gold)
    {
        Gold += gold;
        UpdateGoldUI();
        Console.Write("Aumentou gold");
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = Gold.ToString();
        }
    }
}
