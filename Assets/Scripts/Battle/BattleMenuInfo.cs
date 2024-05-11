using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleMenuInfo : MonoBehaviour
{
    public GameObject Pointer1;
    public GameObject Pointer2;
    public GameObject Pointer3;
    public GameObject Pointer4;                                                                         

    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI DamageValue;
    public TextMeshProUGUI TPCosts;
    public TextMeshProUGUI TypeValue;
    public TextMeshProUGUI LevelValueText;

    public TextMeshProUGUI DamageTitle;
    public TextMeshProUGUI TPCostTitle;
    public TextMeshProUGUI TypeTitle;
    public TextMeshProUGUI LevelTitle;

    public GameObject battleSystem;

    public Animator Anim;

    public void Start()
    {
        battleSystem = GameObject.FindWithTag("battlesystem");
        Pointer1.SetActive(true);
        Pointer2.SetActive(false);
        Pointer3.SetActive(false);
        Pointer4.SetActive(false);

        DamageTitle.enabled = true;
        TPCostTitle.enabled = true;
        TypeTitle.enabled = true;
        LevelTitle.enabled = true;
        Anim = Anim.GetComponent<Animator>();
    }

    public void UpdateData(string Title, string DescriptionValue, int Damage, int TPCost, string Type, int LevelValue)
    {
        TitleText.text = Title;
        DescriptionText.text = DescriptionValue;

        DamageValue.gameObject.SetActive(Damage != 0);
        DamageValue.text = Damage != 0 ? Damage.ToString("D3") : "";

        TPCosts.gameObject.SetActive(TPCost != 0);
        TPCosts.text = TPCost != 0 ? TPCost.ToString("D2") : "";

        TypeValue.gameObject.SetActive(!string.IsNullOrEmpty(Type));
        TypeValue.text = Type;

        LevelValueText.gameObject.SetActive(LevelValue != 0);
        LevelValueText.text = LevelValue != 0 ? LevelValue.ToString("D2") : "";
    }

    public void AttackText()
    {
        Pointer1.SetActive(true);
        Pointer2.SetActive(false);
        Pointer3.SetActive(false);
        Pointer4.SetActive(false);

        DamageTitle.enabled = true;
        TPCostTitle.enabled = true;
        TypeTitle.enabled = true;
        LevelTitle.enabled = true;

        Unit playerUnit = battleSystem.GetComponent<BattleSystem>().playerVariables.GetComponent<Unit>();
        string title = "Attack";
        string description = "This is the standard attack, which does not use any TP. It can always be performed. It restores 9 TP.";
        int damage = playerUnit.attack;
        int tpCost = playerUnit.attackTPCost;
        string type = playerUnit.attackType.ToString();
        int level = (int)playerUnit.AttackLevel;

        UpdateData(title, description, damage, tpCost, type, level);
    }


    public void SkillDescriptionText()
    {
        Pointer1.SetActive(false);
        Pointer2.SetActive(true);
        Pointer3.SetActive(false);
        Pointer4.SetActive(false);

        DamageTitle.enabled = false;
        TPCostTitle.enabled = false;
        TypeTitle.enabled = false;
        LevelTitle.enabled = false;

        string title = "Skills";
        string description = "Skills represent a different variant of the attack, which also consume TP as a result. These can be of various types, allowing them to deal elemental or technological damage, which causes additional damage. If the type is at a disadvantage, it inflicts less damage.";

        UpdateData(title, description, 0, 0, "", 0);
    }


    public void ComboDescriptionText()
    {
        Pointer1.SetActive(false);
        Pointer2.SetActive(false);
        Pointer3.SetActive(true);
        Pointer4.SetActive(false);

        DamageTitle.enabled = false;
        TPCostTitle.enabled = false;
        TypeTitle.enabled = false;
        LevelTitle.enabled = false;

        string title = "Combo";
        string description = "";

        UpdateData(title, description, 0, 0, "", 0);
    }

    public void ItemsDescriptionText()
    {
        Pointer1.SetActive(false);
        Pointer2.SetActive(false);
        Pointer3.SetActive(false);
        Pointer4.SetActive(true);

        DamageTitle.enabled = false;
        TPCostTitle.enabled = false;
        TypeTitle.enabled = false;
        LevelTitle.enabled = false;

        string title = "Items";
        string description = "";

        UpdateData(title, description, 0, 0, "", 0);
    }

    public void Skill1Text()
    {
        Pointer1.SetActive(true);
        Pointer2.SetActive(false);
        Pointer3.SetActive(false);
        Pointer4.SetActive(false);

        DamageTitle.enabled = true;
        TPCostTitle.enabled = true;
        TypeTitle.enabled = true;
        LevelTitle.enabled = true;

        Unit playerUnit = battleSystem.GetComponent<BattleSystem>().playerVariables.GetComponent<Unit>();
        string title = "Shoot";
        string description = "";
        int damage = playerUnit.skillAttack1;
        int tpCost = playerUnit.skillAttack1TpCost;
        string type = playerUnit.skill1AttackType.ToString();
        int level = (int)playerUnit.skill1AttackType;

        UpdateData(title, description, damage, tpCost, type, level);
    }

    public void Skill2Text()
    {
        Pointer1.SetActive(false);
        Pointer2.SetActive(true);
        Pointer3.SetActive(false);
        Pointer4.SetActive(false);

        DamageTitle.enabled = true;
        TPCostTitle.enabled = true;
        TypeTitle.enabled = true;
        LevelTitle.enabled = true;

        Unit playerUnit = battleSystem.GetComponent<BattleSystem>().playerVariables.GetComponent<Unit>();
        string title = "Sword";
        string description = "";
        int damage = playerUnit.skillAttack2;
        int tpCost = playerUnit.skillAttack2TpCost;
        string type = playerUnit.skill2AttackType.ToString();
        int level = (int)playerUnit.skill2AttackType;

        UpdateData(title, description, damage, tpCost, type, level);
    }

    public void Skill3Text()
    {
        Pointer1.SetActive(false);
        Pointer2.SetActive(false);
        Pointer3.SetActive(true);
        Pointer4.SetActive(false);

        DamageTitle.enabled = true;
        TPCostTitle.enabled = true;
        TypeTitle.enabled = true;
        LevelTitle.enabled = true;

        Unit playerUnit = battleSystem.GetComponent<BattleSystem>().playerVariables.GetComponent<Unit>();
        string title = "Laser";
        string description = "";
        int damage = playerUnit.skillAttack3;
        int tpCost = playerUnit.skillAttack3TpCost;
        string type = playerUnit.skill3AttackType.ToString();
        int level = (int)playerUnit.skill3AttackType;

        UpdateData(title, description, damage, tpCost, type, level);
    }

    public void BackText()
    {
        Pointer1.SetActive(false);
        Pointer2.SetActive(false);
        Pointer3.SetActive(false);
        Pointer4.SetActive(true);

        DamageTitle.enabled = false;
        TPCostTitle.enabled = false;
        TypeTitle.enabled = false;
        LevelTitle.enabled = false;

        string title = "Back";
        string description = "";

        UpdateData(title, description, 0, 0, "", 0);
    }

}
