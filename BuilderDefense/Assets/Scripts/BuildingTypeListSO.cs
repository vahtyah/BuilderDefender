﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingTypeList", menuName = "ScriptableObjects/BuildingTypeList", order = 0)]
public class BuildingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO> list;
}