using System.Collections.Generic;
using TargetZone.Interfaces;

namespace UI
{
    public interface IPanel
    {
        public void Open(List<IInteractionCommand> commands);
        
        public void Close();

    }
}