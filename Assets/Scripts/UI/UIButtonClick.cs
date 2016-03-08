using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIButtonClick : MonoBehaviour 
{
    public void InitilizeButtons()
    {
        if (ModesClass.ModesQuantities[ModesClass.Modes.HealthPlus] == 0)
            transform.FindChild("Health+").GetComponent<Button>().interactable = false;
        if (ModesClass.ModesQuantities[ModesClass.Modes.StrengthPlus] == 0)
            transform.FindChild("Strength+").GetComponent<Button>().interactable = false;
        if (ModesClass.ModesQuantities[ModesClass.Modes.OilPlus] == 0)
            transform.FindChild("Oil+").GetComponent<Button>().interactable = false;
        if (ModesClass.ModesQuantities[ModesClass.Modes.AntiStrike] == 0)
            transform.FindChild("Antistrike").GetComponent<Button>().interactable = false;
    }

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
            ModesClass.SubtractModeQuantitie(ModesClass.Modes.HealthPlus);
        }
        if (ModesClass.ModesQuantities[ModesClass.Modes.HealthPlus] == 0)
            button.GetComponent<Button>().interactable = false;
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
            ModesClass.SubtractModeQuantitie(ModesClass.Modes.StrengthPlus);
        }
        if (ModesClass.ModesQuantities[ModesClass.Modes.StrengthPlus] == 0)
            button.GetComponent<Button>().interactable = false;
    }
    public void OnOilPlusClicked(GameObject button)
    {
        Managers.TurnManager.PlayerTank.GetComponent<Tank>().Oil += 500;
        GameObject.Find("RightMovement").GetComponent<Button>().interactable = true;
        GameObject.Find("LeftMovement").GetComponent<Button>().interactable = true;
        Managers.TurnManager.PlayerTank.GetComponent<Tank_Movement>().enabled = true;
        NotifyMessage.ShowMessage("Oil Increased", 3);
        ModesClass.SubtractModeQuantitie(ModesClass.Modes.OilPlus);

        if (ModesClass.ModesQuantities[ModesClass.Modes.OilPlus] == 0)
            button.GetComponent<Button>().interactable = false;
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

    public void OnAntiStrikeLoadoutClicked(GameObject button)
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
        SetAlphaToButton(GameObject.Find("Antistrike"));

        button.GetComponent<Button>().interactable = false;
    }

    public void OnSucicdeClicked(GameObject button)
    {
        Managers.TurnManager.SetTurnToNextTank();
        Managers.WeaponManager.WeaponConfigWhenComboOpened();

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
