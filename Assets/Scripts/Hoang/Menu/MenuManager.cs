using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] title;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject info;
    public void OnStartGame()
    {

    }
    public void OnContinueGame()
    {
    }
    public void OnExitGame()
    {
        Application.Quit();
    }
    IEnumerator MoveObject(GameObject[] objs, float horizontal, float vertical, float duration)
    {
        float time = 0;
        foreach (GameObject obj in objs)
        {
            while (time < duration)
            {
                obj.transform.position += new Vector3(horizontal / duration * Time.deltaTime, vertical / duration * Time.deltaTime, 0);
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
        }
    }
    IEnumerator AppearObject(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(true);
    }
    public void OnTitleUp()
    {
        StartCoroutine(MoveObject(title, 0, 300, 1));
    }
    public void OnTitleDown()
    {
        StartCoroutine(MoveObject(title, 0, -300, 1));
    }
    public void OnButtonLeft()
    {
        StartCoroutine(MoveObject(buttons, -1000, 0, 0.2f));
    }
    public void OnButtonRight()
    {
        StartCoroutine(MoveObject(buttons, 1000, 0, 0.2f));
    }
    public void OnAppear(GameObject obj)
    {
        StartCoroutine(AppearObject(obj, 0.5f));
    }
}
