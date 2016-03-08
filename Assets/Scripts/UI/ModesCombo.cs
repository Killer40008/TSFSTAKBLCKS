using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class ModesCombo : MonoBehaviour
{
    public Object ArmorPrefap;
    public GameObject[] ArmorButtons;


    ///

    public void InitilizeButtons()
    {
        Transform border = GameObject.Find("ModesCombo").transform.FindChild("Border").transform;
        for (int i = 0; i < border.childCount; i++)
        {
            Transform parent = border.GetChild(i);
            for (int j = 0; j < parent.childCount; j++)
            {
                Button button = parent.GetChild(j).GetComponent<Button>();
                string name = parent.GetChild(j).name;

                if (name != "None")
                {
                    ModesClass.Modes mode = (ModesClass.Modes)System.Enum.Parse(typeof(ModesClass.Modes), name);
                    if (ModesClass.ModesQuantities[mode] == 0)
                    {
                        button.interactable = false;
                        parent.GetChild(j).GetComponent<CanvasGroup>().alpha = 0.5f;
                    }
                }
            }
        }
    }

    ///
    public void OnModesComboOpen()
    {
        if (!GameObject.Find("ModesCombo").transform.FindChild("Border").gameObject.activeSelf)
        {
            InitilizeButtons();
            GameObject.Find("ModesCombo").transform.FindChild("Border").gameObject.SetActive(true);
        }
        else
            CloseCombo();
    }

    public void CloseCombo()
    {
        GameObject.Find("ModesCombo").transform.FindChild("Border").gameObject.SetActive(false);
        if (GameObject.Find("Loadout") != null)
        {
            GameObject.Find("HUD").transform.FindChild("Panel").transform.FindChild("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text =
                GameObject.Find("Loadout").transform.FindChild("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text;
        }
    }


    public void OnNoneSelected()
    {
        GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = "None";

        //reset double damage
        Managers.DamageManager.DamageMultiply = 1;
        //reset double Burrell
        if (Managers.TurnManager.PlayerTank.GetComponentsInChildren<Tank_Fire>().Length > 1)
            Destroy(Managers.TurnManager.PlayerTank.GetComponentsInChildren<Tank_Fire>().Where(t => t.gameObject.tag == "SecondBurrell").FirstOrDefault().gameObject);

        //reset armors
        ClearArmor();

        CloseCombo();

    }
    public void OnDoubleDamageSelected(GameObject button)
    {
        if (ModesClass.ModesQuantities[ModesClass.Modes.Double_Damage] > 0)
        {
            OnNoneSelected();
            GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = ModesClass.Modes.Double_Damage.ToString().Replace("_", " ");

            Managers.DamageManager.DamageMultiply = 2;
            NotifyMessage.ShowMessage("Double Damage Activated!", 3);
            ModesClass.SubtractModeQuantitie(ModesClass.Modes.Double_Damage);
            CloseCombo();
        }
        if (ModesClass.ModesQuantities[ModesClass.Modes.Double_Damage] == 0)
            button.GetComponent<Button>().interactable = false;
    }
    public void OnDoubleBurrellSelected(GameObject button)
    {
        if (ModesClass.ModesQuantities[ModesClass.Modes.Double_Burrell] > 0)
        {
            OnNoneSelected();
            GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = ModesClass.Modes.Double_Burrell.ToString().Replace("_", " ");


            GameObject burrell = Managers.TurnManager.PlayerTank.transform.FindChild("Burrell").gameObject;
            GameObject secondBurrell = (GameObject)Instantiate(burrell, burrell.transform.position, Quaternion.identity);
            secondBurrell.tag = "SecondBurrell";
            secondBurrell.name = "Burrell2";
            secondBurrell.GetComponent<Burrell_Movement>().enabled = false;
            secondBurrell.transform.SetParent(burrell.transform.parent, true);
            secondBurrell.transform.localEulerAngles = new Vector3(0, 0, 180 - burrell.transform.eulerAngles.z);
            secondBurrell.transform.localScale = burrell.transform.localScale;
            ModesClass.SubtractModeQuantitie(ModesClass.Modes.Double_Burrell);
            CloseCombo();
        }
        if (ModesClass.ModesQuantities[ModesClass.Modes.Double_Burrell] == 0)
            button.GetComponent<Button>().interactable = false;
    }
    public void OnMiniArmorSelected(GameObject button)
    {
        OnNoneSelected();
        GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = ModesClass.Modes.Mini_Armor.ToString().Replace("_", " ");


        CreateArmor(new Color32(244, 227, 0, 180), 100);
        CloseCombo();
        DisableArmorButtons();

    }
    public void OnNormalArmorSelected(GameObject button)
    {
        OnNoneSelected();
        GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = ModesClass.Modes.Normal_Armor.ToString().Replace("_", " ");


        CreateArmor(new Color32(0, 244, 12, 180), 200);
        CloseCombo();
        DisableArmorButtons();

    }
    public void OnStrongArmorSelected(GameObject button)
    {
        OnNoneSelected();
        GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = ModesClass.Modes.Strong_Armor.ToString().Replace("_", " ");

        CreateArmor(new Color32(6, 0, 190, 180), 300);
        CloseCombo();
        DisableArmorButtons();

    }

    public void OnSuperArmorSelected(GameObject button)
    {
        OnNoneSelected();
        GameObject.Find("ModesCombo").transform.FindChild("Label").GetComponent<Text>().text = ModesClass.Modes.Super_Armor.ToString().Replace("_", " ");

        CreateArmor(new Color32(195, 0, 0, 180), 400);
        CloseCombo();
        DisableArmorButtons();
    }

    void CreateArmor(Color32 color, float strength)
    {
        if (!Managers.TurnManager.PlayerTank.GetComponent<Tank>().ArmorActivate)
        {
            Vector3 pos = Managers.TurnManager.PlayerTank.transform.position;
            pos.y -= 0.1f;
            pos.x -= 0.1f;
            pos.z = 0.05f;
            GameObject gm = Instantiate(ArmorPrefap, pos, Quaternion.identity) as GameObject;
            gm.transform.SetParent(Managers.TurnManager.PlayerTank.transform);
            gm.GetComponent<SpriteRenderer>().color = color;
            Managers.TurnManager.PlayerTank.GetComponent<Tank>().ArmorActivate = true;
            Armor arm = gm.GetComponent<Armor>();
            arm.SetStrength(strength);
            arm.Tank = Managers.TurnManager.PlayerTank;
        }
    }
    void ClearArmor()
    {
        for (int i = 0; i < Managers.TurnManager.PlayerTank.transform.childCount; i++)
        {
            if (Managers.TurnManager.PlayerTank.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Armor"))
            {
                Managers.TurnManager.PlayerTank.GetComponent<Tank>().ArmorActivate = false;
                Destroy(Managers.TurnManager.PlayerTank.transform.GetChild(i).gameObject);
            }
        }
    }

    void DisableArmorButtons()
    {
        foreach (GameObject gm in ArmorButtons)
            gm.GetComponent<Button>().interactable = false;
    }
}
