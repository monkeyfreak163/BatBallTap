using System.Collections;
using UnityEngine;

public class CenterSurrounder : MonoBehaviour
{
    public GameObject OriginalSurrounderObject;
    public int SurrounderObjectCount;

    private readonly float AppearWaitDuration = 0.3f;
    private Transform SurrounderParentTransform;

    void Start()
    {
        SurrounderParentTransform = new GameObject( gameObject.name + " Surrounder Parent").transform;
        StartCoroutine(SurroundStepAnimated());
        //Surround();
    }

    IEnumerator SurroundStepAnimated()
    {
        yield return new WaitForSeconds(AppearWaitDuration);

        float AngleStep = 360.0f / SurrounderObjectCount;
       
        OriginalSurrounderObject.transform.SetParent(SurrounderParentTransform);

        for (int i = 1; i < SurrounderObjectCount; i++)
        {
            GameObject newSurrounderObject = Instantiate(OriginalSurrounderObject);
            
            newSurrounderObject.transform.RotateAround(transform.position,Vector3.up,AngleStep * i);
            newSurrounderObject.transform.SetParent(SurrounderParentTransform);

            yield return new WaitForSeconds(AppearWaitDuration);
        }
        
    }
    
    
    void Surround()
    {
        float AngleStep = 360 / SurrounderObjectCount;
        OriginalSurrounderObject.transform.SetParent(SurrounderParentTransform);

        for (int i = 1; i < SurrounderObjectCount; i++)
        {
            GameObject newSurrounderObject = Instantiate(OriginalSurrounderObject);
            
            newSurrounderObject.transform.RotateAround(transform.position,Vector3.up,AngleStep * i);
            newSurrounderObject.transform.SetParent(SurrounderParentTransform);
        }
        
    }

}