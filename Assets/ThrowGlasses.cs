using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGlasses : MonoBehaviour
{
    [SerializeField] ThrowSimulation glassesToThrow;
    [SerializeField] Transform glassesPosition;
    [SerializeField] Vector3 target;
    public IEnumerator ThrowThemGlasses()
    {
        var rock = Instantiate(glassesToThrow, glassesPosition);
        rock.transform.parent = null;
        rock.SetTarget(target);
        rock.StartThrow();

        while (rock != null)
        {
            yield return null;
        }
    }
}