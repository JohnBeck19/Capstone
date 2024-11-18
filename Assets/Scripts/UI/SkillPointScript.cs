using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button lockButton;
    [SerializeField] SkillPointBuff buff;
    [SerializeField] int costAmount = 5;
    [SerializeField] TMP_Text costAmountText;
    [SerializeField] TMP_Text BuffText;
    void Start()
    {
        lockButton.onClick.AddListener(onBuy);
        buff = GetComponent<SkillPointBuff>();
    }

    // Update is called once per frame
    void Update()
    {
        costAmountText.text = costAmount+" SOULS";
        BuffText.text = buff.increaseAmount + " " + buff.type;
    }
    public void onBuy()
    {
        if (buff.player.souls >= costAmount)
        {
            lockButton.gameObject.SetActive(false);
            buff.Buff();
            buff.player.souls -= costAmount;
        }
        

    }
}
