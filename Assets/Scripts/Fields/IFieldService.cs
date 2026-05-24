using System.Collections.Generic;

namespace Fields
{
    public interface IFieldService
    {
        Dictionary<CropType, int> GetActiveFieldCountPerCropType();
        bool HasInactiveField(CropType itemType);
        void OpenField(CropType configType);
    }
}