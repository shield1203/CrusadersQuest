using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite m_defaultBox;

    [SerializeField]
    private Sprite m_selectedBox;

    [SerializeField]
    private Image m_edge;

    [SerializeField]
    private Sprite m_defaultEdge;

    [SerializeField]
    private Sprite m_selectedEdge;

    [SerializeField]
    private Image m_thumbnail;

    [SerializeField]
    private Text m_name;

    [SerializeField]
    private Text m_level;

    private MonsterData m_monsterData;

    public void InitializeMonsterSlot(MonsterData monsterData)
    {
        m_monsterData = monsterData;

        m_name.text = m_monsterData.name;
        m_level.text = "LV" + m_monsterData.level.ToString();
        m_thumbnail.sprite = Resources.Load<Sprite>(m_monsterData.thumbnailPath);
    }

    public void SlotSelected(MonsterCode code)
    {
        if (m_monsterData.code == code)
        {
            gameObject.GetComponent<Image>().sprite = m_selectedBox;
            m_edge.sprite = m_selectedEdge;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = m_defaultBox;
            m_edge.sprite = m_defaultEdge;
        }
    }

    public void OnSelecMonster()
    {
        SoundSystem.Instance.PlaySound(Sound.button_touch);
        MonsterManager.Instance.SetSelectedMonster(m_monsterData);
    }
}
