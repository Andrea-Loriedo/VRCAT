using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartBlockController : MonoBehaviour
{
    public UnityEvent onStartBlock;

    // Mesh Renderers
    MeshRenderer cubeMesh;

    // Materials
    public Material darkerGlass;
    public Material glass;

    // Coroutines
    IEnumerator waitBlockSTartRoutine;

    private void Awake()
    {
        cubeMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // start the block of trials after two seconds when the cursor is in the start block
        waitBlockSTartRoutine = WaitBlockStart(2);
        StartCoroutine(waitBlockSTartRoutine);
    }

    private void OnTriggerExit(Collider other)
    {
        // reset the start block status if trigger exits
        StopCoroutine(waitBlockSTartRoutine);
        // change cube appearance on trigger exit
        cubeMesh.material = glass;
    }

    IEnumerator WaitBlockStart(int delayTime)
    {
        cubeMesh.material = darkerGlass;
        yield return new WaitForSeconds(delayTime);

        StartBlock();
    }

    private void StartBlock()
    {
        Debug.LogFormat("Block Start");

        // when the block starts the cube disappears and the sphere appears
        gameObject.SetActive(false);
        cubeMesh.material = glass;
        onStartBlock.Invoke();
        cubeMesh.material = glass;
    }

}
