using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.Events;

namespace UXFExamples
{

    public class AimingExperimentManager : MonoBehaviour
    {   
        [SerializeField] UnityEvent onNewBlockStart;
        [SerializeField] Session session;

        bool lastBlock = false;

        void Start()
        {
            // disable the whole task initially to give time to use the UI
            gameObject.SetActive(false);
        }

        public void CheckIfLastBlock()
        {
            if(session.currentBlockNum == session.blocks.Count)
            {
                lastBlock = true;
            }
        }

        public void StartNewBlockIfLasttTrial(Trial endedTrial)
        {
            CheckIfLastBlock();

            if(endedTrial == endedTrial.block.lastTrial && endedTrial.status == TrialStatus.Done && !lastBlock)
            {  
                try
                {
                Debug.LogFormat("Invoke Start");
                onNewBlockStart.Invoke();
                }
                catch (UXF.NoSuchTrialException) // Avoid error message at the end of session
                {
                    ;
                }
            }
        }
    }
}
