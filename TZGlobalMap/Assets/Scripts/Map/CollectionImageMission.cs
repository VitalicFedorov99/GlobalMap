using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectionImageMission")]
public class CollectionImageMission : ScriptableObject
{
    [SerializeField] private List<ImageMission> imageMissions;
    [SerializeField] private ImageMission defaultImageMission;
    public ImageMission GetImageMission(float number)
    {
        var imageMission = imageMissions.Where(img => img.number == number).FirstOrDefault();
        if (imageMission == null)
            imageMission = defaultImageMission;

        return imageMission;
    }
}

[System.Serializable]
public class ImageMission
{
    public Sprite Icon;
    public float number;
}
