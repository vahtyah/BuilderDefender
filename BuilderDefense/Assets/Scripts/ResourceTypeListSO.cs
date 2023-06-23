using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceTypeListSO", menuName = "ScriptableObjects/ResourceTypeList", order = 0)]
public class ResourceTypeListSO : ScriptableObject
{
        public List<ResourceTypeSO> list;
}