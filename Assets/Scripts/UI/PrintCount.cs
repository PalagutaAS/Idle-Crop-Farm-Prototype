using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrintCount : MonoBehaviour
    {
        [SerializeField] private Text _text;
        public void Print(int count)
        {
            _text.text = count.ToString();
        }
    }
}
