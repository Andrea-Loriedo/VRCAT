using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockTypeUpdater : MonoBehaviour
{
    public Text blockDisplayText;

    public void UpdateNextTrial(UXF.Session session)
    {
        try
        {
            UXF.Trial nextTrial = session.NextTrial;
            blockDisplayText.text = nextTrial.settings.GetString("block_type");
        }
        catch (UXF.NoSuchTrialException)
        {
            blockDisplayText.text = "Finished!";
        }
    }

    public void UpdateNextTrial(UXF.Trial trial)
    {
        try
        {
            UXF.Trial nextTrial = trial.session.NextTrial;
            blockDisplayText.text = nextTrial.settings.GetString("block_type");
        }
        catch (UXF.NoSuchTrialException)
        {
            blockDisplayText.text = "Finished!";
        }
    }
}
