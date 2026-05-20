using System.Collections.Generic;

namespace Fields
{
    public class FieldService : IFieldService
    {
        private readonly IFieldCollectProvider _fieldProvider;

        public FieldService(IFieldCollectProvider fieldProvider)
        {
            _fieldProvider = fieldProvider;
        }

        public bool HasInactiveField(CropType type)
        {
            if (!_fieldProvider.FieldsDictionary.ContainsKey(type)) return false;
            
            foreach (var field in _fieldProvider.FieldsDictionary[type])
            {
                if (!field.ActiveSelf) return true;
            }
            return false;
        }

        public void OpenField(CropType type)
        {
            if (!_fieldProvider.FieldsDictionary.ContainsKey(type)) return;
            
            foreach (var field in _fieldProvider.FieldsDictionary[type])
            {
                if (!field.ActiveSelf)
                {
                    field.ActiveSelf = true;
                    return;
                }
            }
        }

        public Dictionary<CropType, int> GetActiveFieldCountPerCropType()
        {
            Dictionary<CropType, int> fieldsDictionary = new();

            foreach (var keyValue in _fieldProvider.FieldsDictionary)
            {
                if (keyValue.Value.Count == 0) continue;
                
                int i = 0;
                foreach (var field in keyValue.Value)
                {
                    i += field.ActiveSelf ? 1 : 0;
                }
                
                if (i == 0) continue;
                
                fieldsDictionary[keyValue.Key] = i;
            }

            return fieldsDictionary;
        }
    }
}