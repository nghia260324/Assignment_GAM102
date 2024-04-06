using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill",menuName = "Skill/New Skill")]
public class Skill : ScriptableObject
{
    public int id;
    public string skillName;
    public GameObject skillPrefabs;
    public GameObject effectPrefabs;
    public int damage;
    public float cooldownTime;
}
