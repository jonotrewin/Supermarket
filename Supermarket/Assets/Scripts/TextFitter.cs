using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFitter : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI m_TextMeshPro;
    public TMPro.TextMeshProUGUI TextMeshPro
    {       
        get 
        {
            if (m_TextMeshPro == null && transform.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                m_TextMeshPro = transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                m_TMPRectTransform = m_TextMeshPro.rectTransform;
            }
            return m_TextMeshPro; 
        } 
    }
    private RectTransform m_rectTransform;
    public RectTransform Rect 
    { 
        get 
        { 
            if (m_rectTransform == null)
            {
                m_rectTransform = GetComponent<RectTransform>();
            }
            return m_rectTransform; 
        } 
    }
    private RectTransform m_TMPRectTransform;
    public RectTransform TMPRectTransform { get { return m_TMPRectTransform;} }

    private float m_PreferredHeight;
    public float PrefferedHeight { get { return PrefferedHeight; } }

    private void SetHeight()
    {
        if (m_TextMeshPro == null)
            return;
        m_PreferredHeight = TextMeshPro.preferredHeight;
        m_rectTransform.sizeDelta = new Vector2(m_rectTransform.sizeDelta.x, m_PreferredHeight);
    }

    private void OnEnable()
    {
        SetHeight();
    }

    private void Start()
    {
        SetHeight();
    }

    private void Update()
    {
        if(m_PreferredHeight != TextMeshPro.preferredHeight)
        {
            SetHeight(); 
        }
    }
}
