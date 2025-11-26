using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningState_Temp : MonoBehaviour
{
    public GameObject winText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Win!");
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            winText.SetActive(true);
        }
    }
}
