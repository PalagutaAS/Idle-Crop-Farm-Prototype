using UnityEngine;

namespace UI
{
    public class PrintCrop : PrintCount
    {
        [SerializeField] private CropType _type;
        public CropType Type => _type;

    }
}
