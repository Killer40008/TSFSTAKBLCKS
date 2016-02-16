using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIButtonClick : MonoBehaviour 
{

    public void OnHealthPlusClicked(GameObject button)
    {
        if (Managers.DamageManager.GetHealth(Managers.TurnManager.PlayerTank) < 100)
        {
            if (Managers.DamageManager.GetHealth(Managers.TurnManager.PlayerTank) + 20 <= 100)
                Managers.DamageManager.AddHealth(Managers.TurnManager.PlayerTank, 20);
            else
                Managers.DamageManager.SetHealth(Managers.TurnManager.PlayerTank, 100);

            Managers.DamageManager.CalculatePlayerHealthInUI();
            NotifyMessage.ShowMessage("Health Increased", 3);
            ModesClass.ModesQuantities[ModesClass.Modes.HealthPlus]--;
        }
    }
    public void OnStrengthPlusClicked(GameObject button)
    {
        if (Managers.DamageManager.GetStrength(Managers.TurnManager.PlayerTank) < 100)
        {
            if (Managers.DamageManager.GetStrength(Managers.TurnManager.PlayerTank) + 20 <= 100)
                Managers.DamageManager.AddStrength(Managers.TurnManager.PlayerTank, 20);
            else
                Managers.DamageManager.SetStrength(Managers.TurnManager.PlayerTank, 100);

            Managers.DamageManager.CalculatePlayerStrenghInUI();
            NotifyMessage.ShowMessage("Strength Increased", 3);
            ModesClass.ModesQuantities[ModesClass.Modes.StrengthPlus]--;

        }
    }
    public void OnOilPlusClicked(GameObject button)
    {
        Managers.TurnManager.PlayerTank.GetComponent<Tank>().Oil += 500;
        GameObject.Find("RightMovement").GetComponent<Button>().interactable = true;
        GameObject.Find("LeftMovement").GetComponent<Button>().interactable = true;
        Managers.TurnManager.PlayerTank.GetComponent<Tank_Movement>().enabled = true;
        NotifyMessage.ShowMessage("Oil Increased", 3);
        ModesClass.ModesQuantities[ModesClass.Modes.OilPlus]--;

    }
    //
    public void OnTeleportionClicked(GameObject button)
    {
        GameObject.Find("BackGround").GetComponent<Teleportation>().AllowTeleportation();
        NotifyMessage.ShowMessage("Touch On Any Position... ", 3);

    }
    public void OnAntiStrikeClicked(GameObject button)
    {
        if (GetAlphaToButton(button) > 0.3f)
        {
            GameObject sphareTrigger = new GameObject();
            sphareTrigger.transform.position = Managers.TurnManager.PlayerTank.transform.position;
            sphareTrigger.transform.SetParent(Managers.TurnManager.PlayerTank.transform);
            SphereCollider collider = sphareTrigger.AddComponent<SphereCollider>();
            collider.radius = 4;
            SphareAntistrike st = sphareTrigger.AddComponent<SphareAntistrike>();
            st.MyTank = Managers.TurnManager.PlayerTank;
            collider.isTrigger = true;
            NotifyMessage.ShowMessage("Anti Strike Activated!", 3);
            SetAlphaToButton(button);
        }
    }
    public void OnSucicdeClicked(GameObject button)
    {
        Managers.TurnManager.SetTurnToNextTank();
    }


    void SetAlphaToButton(GameObject button)
    {
        Color color = button.GetComponent<Image>().color;
        color.a = 0.3f;
        button.GetComponent<Image>().color = color;
    }

    float GetAlphaToButton(GameObject button)
    {
        Color color = button.GetComponent<Image>().color;
        return color.a;
    }
}
