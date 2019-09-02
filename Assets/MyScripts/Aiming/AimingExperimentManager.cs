using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.Events;

namespace UXFExamples
{

    public class AimingExperimentManager : MonoBehaviour
    {   
        public UnityEvent onNewBlockStart;
        bool lastBlock = false;

        /*
        public GameObject Block1;
        public GameObject block2;
        public GameObject block3;
         */

        public Session session;

        // void Awake()
        // {
        //     StartNewBlockIfLastTrial(session.currentTrial);
        // }

        void Start()
        {
            // disable the whole task initially to give time for the experimenter to use the UI
            gameObject.SetActive(false);

            /* 
            Block1.SetActive(false);
            block2.SetActive(false);
            block3.SetActive(false);
            */

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
