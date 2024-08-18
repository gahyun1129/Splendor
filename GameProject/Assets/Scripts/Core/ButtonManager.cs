using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform t_PlayerInventory;

    public void OnPlayerInvetoryTogleclick()
    {
        Debug.Log("dd");
        //t_PlayerInventory.position = Vector3.up * 400;
    }
}
