using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteOrder : MonoBehaviour
{

    [SerializeField] GameObject billPrefab;
    private Animator dummyAnimator;
    private System.String parentName;
    private GameObject dummy;
    private GameObject bill;

    void Start(){
        parentName = this.transform.parent.name;
        dummy = GameObject.Find(parentName+"DummyPrefab(Clone)");
        dummyAnimator = dummy.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cake" & dummy != null)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = this.transform.parent;
            other.gameObject.transform.localPosition = new Vector3(0f, 1.62f, 0f);

            dummyAnimator.SetTrigger("Completed");
            StartCoroutine(DummyPayment());
        }
    }

    IEnumerator DummyPayment()
    {
        yield return new WaitForSeconds(3.833f);

        bill = Instantiate(billPrefab, dummy.transform.parent.position + new Vector3(0f, 1.62f, 0f), Quaternion.identity);
        bill.transform.parent = dummy.transform.parent;

        bill.transform.localScale = new Vector3(16f, 16f, 16f);

        Destroy(dummy);
    }
}
