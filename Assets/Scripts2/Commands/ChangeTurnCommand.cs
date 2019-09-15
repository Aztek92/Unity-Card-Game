using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ChangeTurnCommand : Command
{
    
    public override void Execute()
    {
        GameViewSystem.Instance.infoTextView.ShowInfo();
        Complete();
    }
}
