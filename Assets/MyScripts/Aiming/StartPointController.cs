using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartPointController : MonoBehaviour
{
    // Events
    [SerializeField] UnityEvent onStartBlock;

    // Materials
    [SerializeField] Material ready;
    [SerializeField] Material go;

    // Mesh Renderers
    MeshRenderer mesh;

    // Coroutines
    IEnumerator waitBlockSTartRoutine;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        // Change start point colour back to the original one
        mesh.material = ready; 
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // start the block of trials if the cursor is kept within the start block for two seconds
        waitBlockSTartRoutine = WaitBlockStart(2);
        StartCoroutine(waitBlockSTartRoutine);
    }

    private void OnTriggerExit(Collider other)
    {
        // reset the start block status if trigger exits
        StopCoroutine(waitBlockSTartRoutine);
        // change start point colour on trigger exit
        mesh.material = ready;
    }

    IEnumerator WaitBlockStart(int delayTime)
    {
        // change start point colour on trigger enter
        mesh.material = go;
        yield return new WaitForSeconds(delayTime);
        // Start the experimental block after a delay
        StartBlock();
    }

    private void StartBlock()
    {
        // when the block starts the start point disappears and the target appears
        TurnOff();
        // call Unity event that resumes the experiment from the next experimental block
        onStartBlock.Invoke();
    }

}
