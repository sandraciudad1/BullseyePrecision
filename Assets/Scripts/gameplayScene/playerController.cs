using System.Collections;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    [SerializeField] GameObject target1;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject target3;
    [SerializeField] GameObject target4;

    Vector3 position;
    Vector3 rotation;

    [SerializeField] GameObject player; 
    [SerializeField] Rigidbody arrow;  
    [SerializeField] Transform bow;    

    [SerializeField] float shootForce = 30f; 
    bool arrowReady = true;

    [SerializeField] TextMeshProUGUI arrowText;
    public int arrowCounter;

    void Start()
    {
        arrowCounter = 5;
        gameManager.gameManagerInstance.sublevel = 1;
        gameManager.gameManagerInstance.SaveProgress();
        activeTarget();
        arrowText.text = arrowCounter.ToString();

        position = mainSceneController.position;
        rotation = mainSceneController.rotation;
        player.transform.position = position;
        player.transform.rotation = Quaternion.Euler(rotation);

        arrow.useGravity = false;
        arrow.isKinematic = true;
    }

    void activeTarget()
    {
        int level = gameManager.gameManagerInstance.level;
        GameObject[] targets = { target1, target2, target3, target4 };

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].active = (i + 1 == level);
        }
        sublevelsManager manager = GameObject.Find("sublevelsManager").GetComponent<sublevelsManager>();
        if (manager != null)
        {
            manager.manageSublevels(gameManager.gameManagerInstance.level, gameManager.gameManagerInstance.sublevel);
        }
    }

    void Update()
    {
        // on-screen click detection for computers
        if (Input.GetMouseButtonDown(0) && arrowReady && arrowCounter > 0)
        {
            ShootArrow();
        }

        // on-screen click detection for mobile devices 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && arrowReady && arrowCounter > 0)
            {
                ShootArrow();
            }
        }
    }

    void ShootArrow()
    {
        arrowCounter--;
        arrowText.text = arrowCounter.ToString();
        
        Vector3 pos = new Vector3((bow.position.x + 0.5f), (bow.position.y+0.1f), bow.position.z);
        Rigidbody newArrow = Instantiate(arrow, pos, bow.rotation);
        newArrow.isKinematic = false;
        newArrow.useGravity = true;

        // calculate shooting direction
        Vector3 shootDirection = Camera.main.transform.forward;
        newArrow.velocity = shootDirection * shootForce;

        // add wind force
        Vector3 windForce = windController.instance.ObtenerFuerzaViento();
        StartCoroutine(ApplyWindOverTime(newArrow, windForce));
    }

    System.Collections.IEnumerator ApplyWindOverTime(Rigidbody arrowRigidbody, Vector3 windForce)
    {
        while (arrowRigidbody != null && !arrowRigidbody.isKinematic)
        {
            Debug.Log("force " + windForce);
            arrowRigidbody.AddForce(windForce, ForceMode.Force);
            yield return new WaitForFixedUpdate();
        }
    }
}
