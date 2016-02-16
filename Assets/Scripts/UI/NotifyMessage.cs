using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotifyMessage : MonoBehaviour
{

    static NotifyMessage Me;

    void Start()
    {
        Me = this;
    }

    public static void ShowMessage(string message, float timeout)
    {
        Me.StartCoroutine(ShowMessageC(message, timeout));
    }

    static IEnumerator ShowMessageC(string message, float timeout)
    {
        Me.GetComponent<CanvasGroup>().interactable = true;
        Me.GetComponent<CanvasGroup>().alpha = 1;

        Me.transform.FindChild("NotifyPanel").FindChild("Text").GetComponent<Text>().text = message;
        yield return new WaitForSeconds(timeout);

        Me.GetComponent<CanvasGroup>().interactable = false;
        Me.GetComponent<CanvasGroup>().alpha = 0;
    }
}
