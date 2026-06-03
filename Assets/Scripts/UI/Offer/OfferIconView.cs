using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Offers
{
    public class OfferIconView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _image;
        [SerializeField] private Color _hasEnough;
        [SerializeField] private Color _notEnough;
        
        public void Setup(Sprite image, int count, int countInInventory)
        {
            _image.sprite = image;
            _count.text = count.ToString();
            _count.color = (countInInventory >= count) ? _hasEnough : _notEnough;
        } 
    }
}