using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class PauseImageButton : MonoBehaviour
{
    [SerializeField] private float delay;
    
    public void OnPauseUIClick()
    {
        StartCoroutine(PublishWithDelay());
    }

    private IEnumerator PublishWithDelay()
    {
        yield return new WaitForSeconds(delay);
        EnvironmentEventBus.OnPauseUIClick.Publish();
    }
}
