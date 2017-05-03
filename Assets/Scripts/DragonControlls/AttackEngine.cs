using UnityEngine;
using System.Collections.Generic;

public class AttackEngine : MonoBehaviour
{

    private AnimatorHandler animHandler;

    public GameObject UI;
    public GameObject owca;


    public KeyCode tailAttackKey;
    public List<ColliderController> tailsColliders;
    public float tailAttackDamage;

    public KeyCode mouthAttackKey;
    public List<ColliderController> mouthColliders;
    public float mouthAttackDamage;

    public KeyCode armAttackKey;
    public List<ColliderController> armColliders;
    public float armAttackDamage;


    public AudioClip audioTailAttack;
    public AudioClip audioArmAttack;
    public AudioClip audioMouthAttack;

    private AudioSource effectAudio;

    // Use this for initialization
    private void Start()
    {
        effectAudio = GetComponent<AudioSource>();
        animHandler = GetComponent<AnimatorHandler>();
    }

    // Update is called once per frame
    private void Update()
    {
        AttackMonitor();
    }

    private void AttackMonitor()
    {
        //Arm Attack
        if (Input.GetKeyDown(armAttackKey))
        {
            animHandler.TriggerAttack("JumpAttack");
            TurnOnColliders(armColliders);
            PlayEffect(audioArmAttack);
        }

        //Mouth Attack
        if (Input.GetKeyDown(mouthAttackKey))
        {
            animHandler.TriggerAttack("HapTrigger");
            //owca.GetComponent<Owca_script>().eat = true;
            TurnOnColliders(mouthColliders);
            PlayEffect(audioMouthAttack);
        }

        //Tail Attack
        if (Input.GetKeyDown(tailAttackKey))
        {
            animHandler.TriggerAttack("TailAttack");
            TurnOnColliders(tailsColliders);
            PlayEffect(audioTailAttack);
        }
    }

    private void TurnOnColliders(List<ColliderController> colliderList)
    {
        foreach(var x in colliderList)
        {
            x.ColliderOn();
            //Debug.Log("turned on for " + x);
        }
    }

    private void PlayEffect(AudioClip clip)
    {
        effectAudio.clip = clip;
        effectAudio.Play();
    }
}