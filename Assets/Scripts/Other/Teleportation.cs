using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour 
{


    public Object TeleportationEffect;
    public Object TeleportationStartEffect;
    public GameObject _TeleportationStartEffect;
    bool _allow = false;


    public void AllowTeleportation()
    {
        var position = Managers.TurnManager.PlayerTank.transform.position;
        position.z = -2;
        _TeleportationStartEffect =(GameObject)Instantiate(TeleportationStartEffect, position, Quaternion.identity);

        Managers.ShowSliders(false);
        _allow = true;
    }

    public void DeactiveTeleportation()
    {
        _allow = false;
    }

    void Update()
    {
        if (_allow)
        {
            //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //    {
            //        Vector2 postion = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            //        Managers.TurnManager.PlayerTank.transform.position = new Vector3(
            //            postion.x, CalculateY(postion.x), 0);

            //        Managers.TurnManager.SetTurnToNextTank();
            //        DeactiveTeleportation();
            //        break;
            //    }
            //}

            if (Input.GetMouseButtonDown(0))
            {
                Destroy(_TeleportationStartEffect);
                Vector2 postion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                postion.y = CalculateY(postion.x);

                StartCoroutine(DoTeleportation(postion));
            }

        }
    }

    IEnumerator DoTeleportation(Vector3 postion)
    {

        postion.z = -2;
        Instantiate(TeleportationEffect, postion, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        Managers.TurnManager.PlayerTank.transform.position = new Vector3(
            postion.x,postion.y, 0);




        yield return new WaitForSeconds(1);
        Managers.TurnManager.SetTurnToNextTank();

        DeactiveTeleportation();
    }

    private float CalculateY(float x)
    {
        RaycastHit info;
        Ray ray = new Ray(new Vector3(x, 15, 0), Vector3.down);
        bool hit = Physics.Raycast(ray, out info);

        if (hit)
            return info.point.y + 0.5f;
        else
            return 6;
    }
}
