using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Add this script to the player game object
/// </summary>
public class FadeObstructionsManager : MonoBehaviour
{
    /// <summary>
    /// Internal class to keep track of the fading object
    /// </summary>
    private class FadeObject
    {
        public GameObject GameObject { get; set; }
        public FadeObjectOptions Options { get; set; }
        public float TransparencyLevel { get; set; }
    }

    /// <summary>
    /// This script is placed on any object that we keep track of internally, 
    /// so that we can remove it from our lists when it gets destroyed 
    /// </summary>
    private class NotifyFadeSystem : MonoBehaviour
    {
        void OnDestroy()
        {
            FadeObstructionsManager.Instance.RemoveFadingObject(this.gameObject);
            FadeObstructionsManager.Instance.UnRegisterShouldBeVisible(this.gameObject);
        }
    }

    static FadeObstructionsManager instance;
    public static FadeObstructionsManager Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// There can only be one FadeObstructingObjects script running in our enviroment, check this on Awake()
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("There should be only one FadeObstructingObjects component in your scene");
    }

    // Camera viewing the object this script is on
    public Camera Camera;

    // Seconds it takes to fade
    public float FadeOutSeconds = 1f;
    public float FadeInSeconds = 1f;

    // A ray is cast from the camera to the object this script is on
    public float RayRadius = 0.25f;

    // The final alpha value of the objects being faded
    public float FinalAlpha = 0.1f;
    
    // Objects on layers to fade
    public LayerMask LayerMask = ~0;

    List<FadeObject> HiddenObjects = new List<FadeObject>();
    List<GameObject> ShouldBeVisibleObjects = new List<GameObject>();

    //
    List<GameObject> objectsInWay = new List<GameObject>();

    // Check to see if a game object is faded
    public bool IsHidden(GameObject go)
    {
        return HiddenObjects.FirstOrDefault(x => x.GameObject == go) != null;
    }

    public void RegisterShouldBeVisible(GameObject shouldBeVisible)
    {
        ShouldBeVisibleObjects.Add(shouldBeVisible);
        shouldBeVisible.AddComponent<NotifyFadeSystem>();
    }

    public void UnRegisterShouldBeVisible(GameObject shouldBeVisible)
    {
        ShouldBeVisibleObjects.Remove(shouldBeVisible);
    }

    bool loggedCameraError = false;
    void Update()
    {
        // Check to make sure we have everything we need
        if (Camera == null)
        {
            if (!loggedCameraError)
            {
                Debug.LogError("You need to set the camera for Fade Obstructing Objects to work", this);
                loggedCameraError = true;
            }

            return;
        }
        loggedCameraError = false;

        // Clear our object list
        objectsInWay.Clear();

        // Build up a list of objects that are in the way of all registered should show objects
        foreach (GameObject go in ShouldBeVisibleObjects)
        {
            Vector3 diffVector = go.transform.position - Camera.transform.position;
            Ray ray = new Ray(Camera.transform.position, Vector3.Normalize(diffVector));
            RaycastHit[] hits = Physics.SphereCastAll(ray, RayRadius, diffVector.magnitude, LayerMask.value);

            foreach (RaycastHit hit in hits)
            {
                // Get the collider
                Collider c = hit.collider;

                // skip any objects that should be visible
                if (ShouldBeVisibleObjects.Contains(c.gameObject) || c.gameObject.GetComponent<Renderer>() == null)
                    continue;

                objectsInWay.Add(c.gameObject);
            }
        }

        FadeObstructions(objectsInWay);
    }

    private void FadeObstructions(List<GameObject> objectsInWay)
    {        
        foreach (GameObject go in objectsInWay)
        {
            // If the object is already hidden
            FadeObject hiddenObject = HiddenObjects.FirstOrDefault(x => x.GameObject == go);
            if (hiddenObject == null)
            {
               // Get the object fade options
                FadeObjectOptions fadeObjectOptions = go.GetComponent<FadeObjectOptions>();
                
                // If the maximum fade is set to 1 then this object won't be hidden, so skip it
                if (fadeObjectOptions != null && fadeObjectOptions.OverrideFinalAlpha && fadeObjectOptions.FinalAlpha == 1)
                    continue;

                hiddenObject = new FadeObject { GameObject = go, TransparencyLevel = 1.0f };
                
                //
                hiddenObject.Options = fadeObjectOptions;

                // Add to hidden objects
                HiddenObjects.Add(hiddenObject);
                go.AddComponent<NotifyFadeSystem>();
            }
        }

        // Unhide the objects that are not hidden anymore
        HiddenObjects.RemoveAll(x =>
        {
            float fadeOutSeconds = x.Options != null && x.Options.OverrideFadeOutSeconds ? x.Options.FadeOutSeconds : FadeOutSeconds;
            float fadeInSeconds = x.Options != null && x.Options.OverrideFadeInSeconds ? x.Options.FadeInSeconds : FadeInSeconds;

            foreach (GameObject go in objectsInWay)
            {
                if (go == x.GameObject)
                {
                    // Change the transparency of already hidden items
                    float maximumFade = x.Options != null && x.Options.OverrideFinalAlpha ? x.Options.FinalAlpha : FinalAlpha;
                    if (x.TransparencyLevel > maximumFade)
                    {
                        x.TransparencyLevel -= Time.deltaTime * (1.0f / fadeOutSeconds);

                        if (x.TransparencyLevel <= maximumFade)
                            x.TransparencyLevel = maximumFade;

                        foreach (Material m in x.GameObject.GetComponent<Renderer>().materials)
                            m.color = new Color(m.color.r, m.color.g, m.color.b, x.TransparencyLevel);

                        // Reached the intended level of fade, disable the renderer if the alpha is 0
                        if (x.TransparencyLevel == maximumFade && x.TransparencyLevel == 0)
                            x.GameObject.GetComponent<Renderer>().enabled = false;
                    }

                    return false;
                }
            }

            // Bring the object up to full transparency before removing it from the fade list 
            if (x.TransparencyLevel < 1.0f)
            {
                // Renable the renderer if the transparency level was 0
                if (x.TransparencyLevel == 0)
                    x.GameObject.GetComponent<Renderer>().enabled = true;

                x.TransparencyLevel += Time.deltaTime * (1.0f / fadeInSeconds);
                if (x.TransparencyLevel > 1)
                    x.TransparencyLevel = 1;

                foreach (Material m in x.GameObject.GetComponent<Renderer>().materials)
                    m.color = new Color(m.color.r, m.color.g, m.color.b, x.TransparencyLevel);

                return false;
            }

            // Remove the FadedObject monobehaviour
            Destroy(x.GameObject.GetComponent<NotifyFadeSystem>());

            return true;
        });


    }

    /// <summary>
    /// Remove an object from the fading sytem, called on destroy by 
    /// FadedObject which is added when the object enters the fade system
    /// </summary>
    /// <param name="gameObject"></param>
    internal void RemoveFadingObject(GameObject gameObject)
    {
        HiddenObjects.RemoveAll(x => x.GameObject == gameObject);
    }
}