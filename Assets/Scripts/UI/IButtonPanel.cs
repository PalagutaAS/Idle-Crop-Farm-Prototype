using System;
using System.Collections.Generic;
using TargetZone.Interfaces;

namespace UI
{
    public interface IButtonPanel
    {
        public void Open(List<IInteractionCommand> commands);
        public void Close();
        
        public event Action OnClickButton;

    }
}