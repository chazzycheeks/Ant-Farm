using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
/*    [Header("Area")]
    [SerializeField] private AmbienceAreas area;*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.SwitchAmbienceBig();
        }
    }
}
