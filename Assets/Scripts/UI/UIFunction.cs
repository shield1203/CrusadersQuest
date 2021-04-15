using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunction : MonoBehaviour
{
    public void RemoveFromUIManager()
    {
        UIManager.Instance.RemoveOneUI();
    }
}
