using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NameChildren : MonoBehaviour
{
    private const char CHAR = '.';

    [SerializeField]
    private bool done = false;
    [SerializeField]
    private bool remove = true;

    private void Awake()
    {
#if !UNITY_EDITOR
        Destroy(this);
#else
        StartRemoveFatherName(transform);
        StartAddFatherName(transform);
#endif
    }

    private void StartAddFatherName(Transform father)
    {
        if (!done)
        {
            AddFatherName(father);
            done = true;
        }
    }

    private void AddFatherName(Transform father)
    {
        Transform child;
        for (int i = 0; i < father.childCount; i++)
        {
            child = father.GetChild(i);
            if (!child.name.Contains(father.name + CHAR))
            {
                child.name = father.name + CHAR + child.name;
            }
            AddFatherName(child);
        }
    }

    private void StartRemoveFatherName(Transform father)
    {
        if (remove)
        {
            RemoveFatherName(father);
            remove = false;
        }
    }

    private void RemoveFatherName(Transform father)
    {
        Transform child;
        for (int i = 0; i < father.childCount; i++)
        {
            child = father.GetChild(i);

            string[] n = child.name.Split(CHAR);

            if (n.Length > 1)
            {
                child.name = n[n.Length - 1];
            }
            RemoveFatherName(child);
        }
    }

    private void Update()
    {
        StartAddFatherName(transform);
        StartRemoveFatherName(transform);
        DestroyImmediate(this);
    }
}