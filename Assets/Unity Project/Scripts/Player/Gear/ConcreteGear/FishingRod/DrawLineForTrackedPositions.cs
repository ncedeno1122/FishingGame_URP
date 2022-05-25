using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineForTrackedPositions : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    public List<Transform> TrackedTransforms;

    // Start is called before the first frame update
    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void OnValidate()
    {
        var lineRenderer = GetComponent<LineRenderer>();
        for (int i = 0; i < TrackedTransforms.Count; i++)
        {
            lineRenderer.SetPosition(i, TrackedTransforms[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: If we reduce the number during runtime, expect errors.
        for (int i = 0; i < TrackedTransforms.Count; i++)
        {
            m_LineRenderer.SetPosition(i, TrackedTransforms[i].position);
        }
    }
}
