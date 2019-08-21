using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ExperimentManager : MonoBehaviour
    {
        // UXF
        public Session session;
        // public BlockSettings settings;  
        // public ParticipantDetails ppDetails;
        bool sessionHasEnded;
        bool newBlockStart;

        public void StartNextTrial()
        {
            session.nextTrial.Begin();
            Debug.LogFormat("Started trial {0}", session.currentTrialNum);
            // settings.distance = session.currentTrial.settings["distance"].ToString(); // Set shape
            // settings.targetMode = session.currentTrial.settings["target_mode"].ToString(); // Set speed
            // Set 3D/2D
            // options.ApplyBlockSettings(settings);
        }

//         public void EndCurrentTrial()
//         {
//             RecordResults(bullseye.GetBullseyeResults(), dart.GetKinematicResults());
//             session.currentTrial.End();
//             Debug.LogFormat("Ended trial {0}", session.currentTrialNum);
//         }

//         public ParticipantDetails GetParticipantDetails()
//         {
//             return ppDetails;
//         }

//         public BlockSettings GetBlockSettings()
//         {
//             return settings;
//         }

//         public void RetrieveParticipantDetails()
//         {
//             ppDetails.age = (int) session.participantDetails["participant_age"];
//             ppDetails.gender = (string) session.participantDetails["participant_gender"];
//             ppDetails.gloveOn = (bool) session.participantDetails["glove_on"];
//             ppDetails.wristLength = (float) session.participantDetails["wrist_length"];
//             ppDetails.lowerArmLength = (float) session.participantDetails["lower_arm_length"];
//             ppDetails.upperArmLength = (float) session.participantDetails["upper_arm_length"];
//             ppDetails.armOn = (bool) session.participantDetails["arm_on"];
//         }

//         public void RecordResults(BullseyeController.TrialResults bullseyeResults, Follower.KinematicResults kinematicResults)
//         {
//             // Behavioural results
//             session.currentTrial.result["target_zone_hit"] = bullseyeResults.targetZone;
//             session.currentTrial.result["total_score"] = bullseyeResults.totalScore;

//             // Kinematic results
//             // session.currentTrial.result["vel_at_release_x"] = kinematicResults.velocityAtReleaseX;
//             // session.currentTrial.result["vel_at_release_y"] = kinematicResults.velocityAtReleaseX;
//             // session.currentTrial.result["vel_at_release_z"] = kinematicResults.velocityAtReleaseX;
//             // session.currentTrial.result["ang_vel_at_release_x"] = kinematicResults.angVelocityAtReleaseX;
//             // session.currentTrial.result["ang_vel_at_release_y"] = kinematicResults.angVelocityAtReleaseX;
//             // session.currentTrial.result["ang_vel_at_release_z"] = kinematicResults.angVelocityAtReleaseX;
//         }

//         public void RecordCurrentJointAngles(ArmKinematicsController.ArmResults armResults, Vector3 handPos)
//         {
//             session.currentTrial.result["wrist_angle"] = armResults.wristAngle.ToString("F2");
//             session.currentTrial.result["elbow_angle"] = armResults.elbowAngle.ToString("F2");
//             session.currentTrial.result["shoulder_angle"] = armResults.shoulderAngle.ToString("F2");
//             session.currentTrial.result["hand_pos_x"] = handPos.x.ToString("F2");
//             session.currentTrial.result["hand_pos_y"] = handPos.y.ToString("F2");
//             session.currentTrial.result["hand_pos_z"] = handPos.z.ToString("F2");
//         }

//         public void MarkBlockBegin()
//         {
//             if(session.currentTrial.numberInBlock == 1)
//             {
//                 newBlockStart = true;
//                 bullseye.ResetScore(); // Reset the score after each block
//                 DestroyAllDarts();
//                 soundFX.Play();
//             }
//             else
//             {
//                 newBlockStart = false;
//             }

//         }

//         public void SetBlockBegin(bool value)
//         {
//             newBlockStart = value;
//         }

//         public bool NewBlockBegin()
//         {
//             return newBlockStart;
//         }

//         public void MarkSessionEnd()
//         {
//             sessionHasEnded = true;
//         }

//         public bool SessionHasEnded()
//         {
//             return sessionHasEnded;
//         }

//         public void DestroyAllDarts()
//         {
//             GameObject[] allDarts; // Create an empty array of GameObjects
//             allDarts =  GameObject.FindGameObjectsWithTag("DartObject"); // Append all GameObject tagged 'DartObject' to the array 'allDarts'

//             foreach(GameObject dart in allDarts)
//             {
//                 Destroy(dart); // Loop through the array and destroy each dart in it
//             }
//         }

//         // Structs for session parameters
//         public struct BlockSettings
//         {
//             public string distance;
//             public string targetMode;
//         }

//         public struct ParticipantDetails
//         {
//             public int age;
//             public string gender;
//             public bool gloveOn;
//             public bool armOn;
//             public float wristLength;
//             public float lowerArmLength;
//             public float upperArmLength;
//         }
    }
}