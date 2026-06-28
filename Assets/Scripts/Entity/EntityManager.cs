using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EntityManager : MonoBehaviour
{
    public float seekingTimer = 4f;
    public float seekingTimerElapsed = 0f;
    public UnityEvent onSeekFinished;
    private VisibilityManager visibilityManager;
    public bool gracePeriod = false;
    public bool isSeeking = false;
    [SerializeField] private Animator entityTest;
    public GameObject spottedText;

    private void Start()
    {
        visibilityManager = FindAnyObjectByType<VisibilityManager>();
    }

    private void Update()
    {
        Seek();
        Find();
    }
    private void OnEnable()
    {
        seekingTimerElapsed = 0f;
        StartCoroutine(GracePeriod());

    }

    public void Seek()
    {
        if (gracePeriod == false)
        {
            seekingTimerElapsed += Time.deltaTime;
            if (seekingTimerElapsed >= seekingTimer)
            {
                onSeekFinished?.Invoke();
                isSeeking = false;
                visibilityManager.ResetOnSeenEvent();
            }
        }
        else if (gracePeriod == true)
        {
            return;
        }
    }

    public IEnumerator GracePeriod()
    {
        gracePeriod = true;
        yield return new WaitForSeconds(2f);
        gracePeriod = false;
        isSeeking = true;

    }

    public void Find()
    {
        if (visibilityManager._isVisible == true && isSeeking == true)
        {
            //SceneManager.LoadSceneAsync(0, 1, 2);
            spottedText.SetActive(true);
            
        }
        else
        {
            return;
        }
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
