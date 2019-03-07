using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public enum BattleDialogType { Simple, Next };

[System.Serializable]
public class BattleDialogEvent : UnityEvent<BattleDialogType, string> { }

public class BattleDialogManager 
{
    BattleDialogController battleDialogController;

    public BattleDialogManager()
    {
        this.battleDialogController = BattleDialogController.GetInstance();
    }

    public void SendNext(string message)
    {
        DispatchEvent(BattleDialogType.Next, message);
    }

    public void SendSimple(string message)
    {
        DispatchEvent(BattleDialogType.Simple, message);
    }

    public void DispatchEvent(BattleDialogType type, string message)
    {
        BattleDialogEvent e = new BattleDialogEvent();
        e.AddListener(battleDialogController.Process);
    }
}
