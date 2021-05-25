using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSoldierInfo : MonoBehaviour
{
    private List<GameObject> m_soldierUnits;

    [SerializeField]
    private Text m_stageNumber;

    [SerializeField]
    private Image[] m_thumbnail;

    [SerializeField]
    private Text[] m_level;

    [SerializeField]
    private Text[] m_grade;

    [SerializeField]
    private Text[] m_name;

    [SerializeField]
    private Slider[] m_hpBar;

    [SerializeField]
    private Text[] m_hpValue;

    [SerializeField]
    private Text[] m_damage;

    [SerializeField]
    private Text[] m_heal;

    public void InitializeStageSoldierInfo(List<GameObject> soldierUnits)
    {
        m_soldierUnits = soldierUnits;

        StageManager stageManager = StageManager.Instance;
        m_stageNumber.text = stageManager.GetEpisodeMapName(stageManager.GetCurEpisode()) + "-" + stageManager.GetCurStage().ToString();

        List<SoldierData> soldierTeam = SoldierManager.Instance.GetSoldierTeam();
        for(int index = 0; index < m_soldierUnits.Count; index++)
        {
            m_thumbnail[index].sprite = Resources.Load<Sprite>(soldierTeam[index].spritePath);
            m_level[index].text = "LV" + soldierTeam[index].level.ToString();
            m_grade[index].text = soldierTeam[index].grade.ToString();
            m_name[index].text = soldierTeam[index].name;
        }
    }

    void Update()
    {
        SetFigureValue();
    }

    void SetFigureValue()
    {
        if (m_soldierUnits == null) return;

        for(int index = 0; index < m_soldierUnits.Count; index++)
        {
            m_hpBar[index].value = m_soldierUnits[index].GetComponent<SoldierUnit>().GetHPPercent();
            m_hpValue[index].text = m_soldierUnits[index].GetComponent<SoldierUnit>().GetCurHP().ToString();
            // 피해량
            // 회복량
        }
    }
}
