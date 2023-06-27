using UnityEngine;

[CreateAssetMenu(fileName = "ResourceTypeSO", menuName = "ScriptableObjects/ResourceType", order = 0)]
public class ResourceTypeSO : ScriptableObject
{
    public string nameString;
    public Sprite sprite;
    public Color colorName;
}