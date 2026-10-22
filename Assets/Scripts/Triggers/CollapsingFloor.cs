using System.Collections;
using UnityEngine;
using MoreMountains.CorgiEngine;
using FMODUnity;

public class CollapsingFloor : MonoBehaviour
{
    public Animator floorPiece0;
    public Animator floorPiece1;
    public Animator floorPiece2;
    public Animator floorPiece3;
    public Animator floorPiece4;
    public Animator floorPiece5;
    public Animator floorPiece6;
    public GameObject floorCollider;
    public Animator cameraZoom;

    [SerializeField] private EventReference entitySound;
    [SerializeField] private EventReference floorSound;
    [SerializeField] private EventReference windSound;


    public void StartDroppingFloor()
    {
        StartCoroutine(DropFloorEvent());
    }
    public IEnumerator DropFloorEvent()
    {
        cameraZoom.SetTrigger("zoomingIn");
        /*CameraUIAnimationController.Instance.TriggerBlackBarDownIn();
        CameraUIAnimationController.Instance.TriggerBlackBarUpIn();*/
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Character>().Freeze();
        AudioManager.instance.PlayOneShotSound(entitySound, this.transform.position);
        player.GetComponentInChildren<Animator>().SetTrigger("StartCutscene");
        player.GetComponentInChildren<Animator>().SetBool("Ending", true);
        yield return new WaitForSeconds(2.5f);
        player.GetComponentInChildren<Animator>().SetBool("Ending", false);
        player.GetComponent<Character>().UnFreeze();
        floorCollider.SetActive(false);
        AudioManager.instance.PlayOneShotSound(floorSound, this.transform.position);
        floorPiece0.SetTrigger("drop");
        floorPiece1.SetTrigger("drop");
        floorPiece2.SetTrigger("drop");
        floorPiece3.SetTrigger("drop");
        floorPiece4.SetTrigger("drop");
        floorPiece5.SetTrigger("drop");
        floorPiece6.SetTrigger("drop");
        cameraZoom.SetTrigger("zoomingOut");
        /*CameraUIAnimationController.Instance.TriggerBlackBarDownOut();
        CameraUIAnimationController.Instance.TriggerBlackBarUpOut();*/
    }

    public void StartRoom14Couroutine()
    {
        StartCoroutine(Room14EntryFreeze());
    }

    private IEnumerator Room14EntryFreeze()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Character>().Freeze();
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.PlayOneShotSound(windSound, this.transform.position);
        player.GetComponent<Character>().UnFreeze();
    }

}
