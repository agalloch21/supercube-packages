using UnityEngine;

public class LavaMaskTestor : MonoBehaviour
{
    public GameObject testGO;
    public LavaMaskDiscriminator lavaMaskDiscriminator;


    // Update is called once per frame
    void Update()
    {
        float v = lavaMaskDiscriminator.IsAboveLava(testGO.transform.position);
        Debug.Log(v);
    }
}
