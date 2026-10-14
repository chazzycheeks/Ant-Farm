using System.Collections;
using UnityEngine;
using MoreMountains.CorgiEngine;

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
        player.GetComponentInChildren<Animator>().SetBool("Ending", true);
        yield return new WaitForSeconds(2.5f);
        player.GetComponentInChildren<Animator>().SetBool("Ending", false);
        player.GetComponent<Character>().UnFreeze();
        floorCollider.SetActive(false);
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
        player.GetComponent<Character>().UnFreeze();
    }

}
