using UnityEngine;
using System.Collections;
using System.Linq;

public class Focus : MonoBehaviour 
{
    public Object FocusSprite;
    bool _active = false;
    GameObject focusGameobject;
    bool downed  =false;
    public GameObject TankSelected;

   public  void OnMouseDown()
    {
        if (_active)
        {
            TankSelected = this.gameObject;
            Managers.TurnManager.tanks.Where(t => t.activeSelf == true && t != this.gameObject).ToList().ForEach(e => e.GetComponent<Focus>().JustDestroy());

            if (!downed)
            {
                GameObject gm = (GameObject)Instantiate(FocusSprite, new Vector3(this.transform.position.x, this.transform.position.y, -5),
                    Quaternion.identity);
                focusGameobject = gm;
                StartCoroutine(Rotation(gm));
                downed = true;
            }
            else
            {
                Color32 c = focusGameobject.GetComponent<SpriteRenderer>().color;
                focusGameobject.GetComponent<SpriteRenderer>().color = new Color32(c.r, c.g, c.b, 255);
            }

        }

    }

   void Update()
   {
       if (_active && downed)
       {
           focusGameobject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -5);
       }
   }


    public void Active()
    {
        downed = false;
        _active = true;
    }
    public void DeActive()
    {
        if (_active)
        {
            TankSelected = null;
            downed = false;
            _active = false;
            StopAllCoroutines();
            Destroy(focusGameobject);
        }
    }
        
    public void JustDestroy()
    {
        if (focusGameobject != null)
        {
            TankSelected = null;
            Color32 c = focusGameobject.GetComponent<SpriteRenderer>().color;
            focusGameobject.GetComponent<SpriteRenderer>().color = new Color32(c.r, c.g, c.b, 0);
        }
    }

    private IEnumerator Rotation(GameObject gm)
    {
        while (true && _active)
        {
            yield return new WaitForFixedUpdate();
            gm.transform.Rotate(0, 0, 0.5f);
        }
    }


}
