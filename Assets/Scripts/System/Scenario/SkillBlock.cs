using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockColor
{
    Orange,
    Blue,
    Green
}

public class SkillBlock : MonoBehaviour
{
    private bool m_active;

    private int m_blockIndex;

    private BlockColor m_color;

    private bool m_prevLink = false;

    private bool m_nextLink = false;

    [SerializeField]
    Image m_thumbnail;

    [SerializeField]
    Image m_edge;

    [SerializeField]
    Sprite[] m_edgeSprite;

    [SerializeField]
    Image m_link;

    [SerializeField]
    Sprite[] m_linkSprite;

    [SerializeField]
    Image m_glowBlock;

    [SerializeField]
    Image m_glowEdge;

    Animator m_anim;

    public delegate void blockTouchDelegate(int index);

    public blockTouchDelegate m_touchDelegate;

    public void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    public void InitializeSkillBlock(int index, BlockColor color, bool activeValue, string thumbnailPath)
    {
        m_blockIndex = index;
        m_color = color;
        m_active = activeValue;
        m_thumbnail.sprite = Resources.Load<Sprite>(thumbnailPath);
        m_edge.sprite = m_edgeSprite[(int)color];
        m_link.sprite = m_linkSprite[(int)color];
    }

    public void Start()
    {
        m_thumbnail.material = new Material(m_thumbnail.material);

        m_glowBlock.material = new Material(m_glowBlock.material);
        m_glowBlock.material.SetFloat("_Intensity", 1.56f);

        m_glowEdge.material = new Material(m_glowEdge.material);
        m_glowEdge.material.SetFloat("_Intensity", 1.56f);
    }

    public void NextBlockLink()
    {
        m_nextLink = true;
    }

    public bool IsNextBlockLinked()
    {
        return m_nextLink;
    }

    public BlockColor GetBlockColor()
    {
        return m_color;
    }

    public int GetBlockIndex()
    {
        return m_blockIndex;
    }

    public void SetBlockIndex(int index)
    {
        m_blockIndex = index;
    }

    public void PrevBlockLink()
    {
        m_prevLink = true;
        m_link.gameObject.SetActive(true);
    }

    public bool IsPrevBlockLinked()
    {
        return m_prevLink;
    }

    public void OnTouchBlock()
    {
        if (m_touchDelegate != null) m_touchDelegate(m_blockIndex);
    }

    public void AnimationPlay()
    {
        m_anim.SetBool("isLinked", true);
    }

    public void CreateParticle()
    {
        GameObject touchParticle = Instantiate(Resources.Load("Block/BlockTouchEffect") as GameObject, transform.parent);
        touchParticle.transform.position = transform.position;
        touchParticle.GetComponent<BlockTouchParticle>().InitializeParticle(m_color, m_active);
    }

    public void DeactivateBlock()
    {
        m_active = false;
        m_thumbnail.material.SetFloat("_EffectAmount", 1);
    }
}
