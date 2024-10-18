using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer[] _renderers;
    private Color _defaultColor = Color.white;
    private Color _busyColor = Color.red;
    private Color _freeColor = Color.green;
    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
    }

    public void SetDefaultColor()
    {
        SetColor(_defaultColor);
    }

    public void SetBusyOrFreeColor(bool isBusy)
    {
        SetColor(isBusy ? _busyColor : _freeColor);
    }
    private void SetColor(Color color)
    {
        foreach (var item in _renderers)
        {
            item.material.color = color;
        }
    }
}
