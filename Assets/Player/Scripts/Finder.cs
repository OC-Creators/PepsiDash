//------------------------------------------------------------------------
//
// (C) Copyright 2017 Urahimono Project Inc.
//
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;

public class Finder : MonoBehaviour
{
    [SerializeField]
    private Material    m_defaultMaterial   = null;
    [SerializeField]
    private Material    m_foundMaterial     = null;

    private Renderer            m_renderer  = null;
    private List<GameObject>    m_targets   = new List<GameObject>();


    private void Awake()
    {
        //m_renderer  = GetComponent<Renderer>();

        var searching   = GetComponentInChildren<SearchingBehavior>();
        searching.onFound   += OnFound;
        searching.onLost    += OnLost;
    }

    private void OnFound( GameObject i_foundObject )
    {
        m_targets.Add( i_foundObject );
        //m_renderer.material = m_foundMaterial;
        Debug.Log(i_foundObject);
        Debug.Log("Found");
    }

    private void OnLost( GameObject i_lostObject )
    {
        m_targets.Remove( i_lostObject );
        Debug.Log("Lost");
        if ( m_targets.Count == 0 )
        {
           // m_renderer.material = m_defaultMaterial;
            Debug.Log("Lost");
        }
    }

} // class Finder
