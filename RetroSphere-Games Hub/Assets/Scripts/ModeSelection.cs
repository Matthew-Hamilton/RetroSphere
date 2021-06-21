using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    public ParticleSystem mySystem;
    public GameObject modeHolder;
    public List<GameObject> modes;
    public SpriteRenderer backplateSprite;

    public GameObject selectedMode;
    public Text modeText;

    public int menuMoveSpeed = 10;

    string nextScene;
    bool transition = false;

    // Start is called before the first frame update
    void Start()
    {
        modes = new List<GameObject>();
        Transform[] ts = modeHolder.GetComponentsInChildren<Transform>();
        foreach(Transform mode in ts)
        {
            if(mode.gameObject != modeHolder)
                modes.Add(mode.gameObject);
        }
        selectedMode = modes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            Transition();
        }
        else
        {
            if (Input.GetAxisRaw("Fire1") != 0 || Input.GetAxisRaw("Jump") != 0)
            {
                nextScene = selectedMode.name;
                transition = true;
            }
            if (Mathf.Abs(0 - selectedMode.transform.position.x) <= 0.05)
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && modes.IndexOf(selectedMode) < modes.Count - 1)
                {
                    selectedMode = modes[modes.IndexOf(selectedMode) + 1];
                }
                if (Input.GetAxisRaw("Horizontal") < 0 && modes.IndexOf(selectedMode) > 0)
                {
                    selectedMode = modes[modes.IndexOf(selectedMode) - 1];
                }
                modeText.text = selectedMode.name;
            }

            if (Mathf.Abs(0 - selectedMode.transform.position.x) > 0.05)
            {
                modeHolder.transform.position += new Vector3(-selectedMode.transform.position.x.CompareTo(0), modeHolder.transform.position.y, modeHolder.transform.position.z) * menuMoveSpeed * Time.deltaTime;
            }
        }

        
    }

    void Transition(float transitionSpeed = 2)
    {
        SpriteRenderer selectedSprite = selectedMode.GetComponent<SpriteRenderer>();
        modeText.color = new Color(modeText.color.r, modeText.color.g, modeText.color.b, modeText.color.a - Time.deltaTime * transitionSpeed);
        backplateSprite.color = new Color(backplateSprite.color.r, backplateSprite.color.g, backplateSprite.color.b, backplateSprite.color.a - Time.deltaTime * transitionSpeed);
        selectedSprite.color = new Color(selectedSprite.color.r, selectedSprite.color.g, selectedSprite.color.b, selectedSprite.color.a - Time.deltaTime * transitionSpeed);
        var col = mySystem.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(selectedSprite.color.a, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        col.color = grad;


        if (modeText.color.a <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
