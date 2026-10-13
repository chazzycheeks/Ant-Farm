using System.Collections;
using UnityEngine;

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

    public void StartDroppingFloor()
    {
        StartCoroutine(DropFloor());
    }
    public IEnumerator DropFloor()
    {
        yield return new WaitForSeconds(2.5f);
        floorCollider.SetActive(false);
        floorPiece0.SetTrigger("drop");
        floorPiece1.SetTrigger("drop");
        floorPiece2.SetTrigger("drop");
        floorPiece3.SetTrigger("drop");
        floorPiece4.SetTrigger("drop");
        floorPiece5.SetTrigger("drop");
        floorPiece6.SetTrigger("drop");
    }
}
