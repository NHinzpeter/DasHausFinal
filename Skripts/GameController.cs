using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

    public enum levelenum { l1, l2, l3, l4, l5, l6, l7};
    enum triggercount { t1, t2, t3};
    public levelenum level;
    triggercount triggercounter;
    GameObject szene2;
    GameObject zuBewegen;
    Text untertitel, tutorial1, tutorial2, Loop1, Loop2;
    float xWert, yWert, zWert;
    int clearCanvas, Tür1;
    public float testopacity;
    public Material mat;
    public Material mat2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "BeginnSzene2")
        {
            level = levelenum.l2;
            untertitel.text = "Dieser Flur hat offenbar ein Gefälle.";
            clearCanvas = 300;

            Destroy(other);
        }

        else if (other.name == "Szene2Trigger1")
        {
            Destroy(GameObject.Find("WandSzene1"));
            Destroy(GameObject.Find("TextSzene1Boden"));
            Destroy(GameObject.Find("TextSzene1Decke"));
            Destroy(GameObject.Find("TextSzene1Rechts"));
            Destroy(GameObject.Find("TextSzene1Links"));

            triggercounter = triggercount.t2;
            untertitel.text = "Er dreht um und geht in die entgegengesetzte Richtung, das heißt eigentlich bergauf.";
            clearCanvas = 300;

            Destroy(other);
        }

        else if (other.name == "Szene2Trigger2")
        {
            if (triggercounter == triggercount.t2)
            {
                Destroy(GameObject.Find("Text1Boden"));
                Destroy(GameObject.Find("Text1Decke"));
                Destroy(GameObject.Find("Text1Links"));
                Destroy(GameObject.Find("Text1Rechts"));

                //neue Texte werden an die richtige Position geschoben, ein Loch in der Wand erscheint
                zuBewegen = GameObject.Find("Text3Boden");
                zuBewegen.transform.position = new Vector3(zuBewegen.transform.position.x, zuBewegen.transform.position.y + 0.54f, zuBewegen.transform.position.z);
                zuBewegen = GameObject.Find("Text3Decke");
                zuBewegen.transform.position = new Vector3(zuBewegen.transform.position.x, zuBewegen.transform.position.y -1, zuBewegen.transform.position.z);
                GameObject.Find("GeheimeWand").GetComponent<Collider> ().enabled = false;
                GameObject.Find("GeheimeWand").GetComponent<Renderer>().enabled = false;
                Destroy(GameObject.Find("GeheimeWandRueckseite"));

                GameObject.Find("VerkleidungS").GetComponent<Renderer>().enabled = true;
                GameObject.Find("VerkleidungN").GetComponent<Renderer>().enabled = true;
                GameObject.Find("VerkleidungD").GetComponent<Renderer>().enabled = true;

                triggercounter = triggercount.t3;
                untertitel.text = "Abermals geht es bergab.";
                clearCanvas = 300;

                Destroy(other);
            }
        }

        else if (other.name == "Szene2Loop")
        {
            Loop1.text = "In diese Richtung geht es wohl nicht weiter.";
        }

        else if (other.name == "Szene2Loop2")
        {
            Loop2.text = "In diese Richtung geht es wohl nicht weiter.";
        }

        else if (other.name == "Szene2Trigger3")
        {
            if (triggercounter == triggercount.t3)
            {
                level = levelenum.l3;

                foreach(Transform child in szene2.transform)
                {
                    child.localEulerAngles = new Vector3(0, 90, 0);
                }
                triggercounter = triggercount.t1;

                //Boden, Decke und Rueckwand werden angepasst, da sie vorher nicht in ihrer eigentlichen Position sein können (Grafikglitch)
                GameObject.Find("S3Rueckwand").GetComponent<MeshRenderer>().enabled=true;
                zuBewegen = GameObject.Find("Decke");
                zuBewegen.transform.position = new Vector3(zuBewegen.transform.position.x, zuBewegen.transform.position.y, zuBewegen.transform.position.z +0.1f);
                zuBewegen = GameObject.Find("S3Decke");
                zuBewegen.transform.position = new Vector3(zuBewegen.transform.position.x, zuBewegen.transform.position.y, zuBewegen.transform.position.z + 2);

                Destroy(GameObject.Find("Text2Decke"));
                Destroy(GameObject.Find("Text2Boden"));
                Destroy(GameObject.Find("Text2Links"));
                Destroy(GameObject.Find("Text2Rechts"));

                untertitel.text = "Verwirrt biegt er in einen großen Raum ein und versucht, seine Gedanken zu ordnen.";
                tutorial2.text = "Stehe nah genug an einem Objekt und fokussiere es, \num mit ihm zu interagieren.";
                clearCanvas = 300;

                Destroy(other);
            }
            
        }

        else if (other.name == "Szene3Trigger0")
        {
            untertitel.text = "Auch bleibt der endlose Korridor nicht immer gleich groß.";
            clearCanvas = 300;

            Destroy(other);
        }

        else if (other.name == "Szene3Trigger1")
        {

            untertitel.text = "Bisweilen stürzt die Decke auf ihn hernieder.";
            clearCanvas = 300;

            Destroy(other);
        }

        else if (other.name == "Szene3Trigger2")
        {
            triggercounter = triggercount.t2;
            
            //Loch in der Wand wird wieder geschlossen, Türen verschwinden
            GameObject.Find("GeheimeWand").GetComponent<Collider>().enabled = true;
            GameObject.Find("GeheimeWand").GetComponent<Renderer>().enabled = true;

            Destroy(GameObject.Find("VerkleidungN"));
            Destroy(GameObject.Find("VerkleidungS"));
            Destroy(GameObject.Find("VerkleidungD"));

            zuBewegen = GameObject.Find("TuerBody");
            zuBewegen.GetComponent<Renderer>().enabled = false;
            zuBewegen.GetComponent<Collider>().enabled = false;
            zuBewegen = GameObject.Find("TuerBody2");
            zuBewegen.GetComponent<Renderer>().enabled = false;
            zuBewegen.GetComponent<Collider>().enabled = false;

            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().maxReticleDistance = 8f;

            Destroy(other);
        }

        else if (other.name == "Szene4Trigger1")
            {
            level = levelenum.l4;

            xWert = GameObject.Find("Wendeltreppe").transform.eulerAngles.x;
            yWert = GameObject.Find("Wendeltreppe").transform.eulerAngles.y;
            zWert = GameObject.Find("Wendeltreppe").transform.eulerAngles.z;

            untertitel.text = "Navidson gelangt in einen kleinen Raum mit einer Wendeltreppe.";
            clearCanvas = 300;

            Destroy(other);
        }

        else if (other.name == "Szene4Trigger2")
        {
            untertitel.text = "Nur eine Reihe Sprossen ragt aus der Wand.";
            clearCanvas = 300;

            Destroy(other);
        }

        else if (other.name == "TreppenTrigger")
        {
            GameObject.Find("Wendeltreppe").GetComponent<MeshCollider>().enabled = true;

            Destroy(other);
        }

        else if (other.name == "Szene6Trigger1") 
        {
            GetComponent<VRWalk>().crouch = true;

                untertitel.text = "Je weiter er geht, \ndesto enger wird der Flur, \nbis er kriechen muss.";
                clearCanvas = 300;

            Destroy(other);

        }

        else if (other.name == "Szene6Trigger2") 
        {
            GetComponent<VRWalk>().crawl = true;

                untertitel.text = "Noch mal hundert Meter, \nund er muss auf \ndem Bauch kriechen.";
                clearCanvas = 300;

            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().maxReticleDistance = 5f;

            Destroy(other);

        }

        else if (other.name == "Szene6Trigger3")
        {
            GetComponent<VRWalk>().stand = true;
            Tür1 = 90;

            Destroy(other);
        }

        else if (other.name == "Szene6Trigger5")
        {
            //Spieler wird in die Mitte der Ebene verschoben, das Haus wird gelöscht und der Endscreen erscheint
            level = levelenum.l7;

            Vector3 pos = new Vector3(40f,27.25f,-600f);
            transform.position = pos;

            GetComponent<VRWalk>().moveForward = false;
            GetComponent<VRWalk>().fire1Blocked = true;

            GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>().maxReticleDistance = 100f;

            Delete_world();

            GameObject.Find("Endscreen Text").GetComponent<Text>().enabled=true;
            GameObject.Find("Neustart").GetComponent<Text>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Szene2Loop")
        {
            Loop1.text = "";
        }

        else if (other.name == "Szene2Loop2")
        {
            Loop2.text = "";
        }
    }
    /*
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * */
    // Use this for initialization
    void Start()
    {
        level = levelenum.l1;
        triggercounter = triggercount.t1;
        szene2 = GameObject.Find("Szene2");
        untertitel = GameObject.Find("Untertitel").GetComponent<Text>();
        tutorial1 = GameObject.Find("Tutorial1").GetComponent<Text>();
        tutorial2 = GameObject.Find("Tutorial2").GetComponent<Text>();
        Loop1 = GameObject.Find("Loop1").GetComponent<Text>();
        Loop2 = GameObject.Find("Loop2").GetComponent<Text>();
        clearCanvas = 0;
        Tür1 = 90;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);
    }
    /*
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * */
    // Update is called once per frame
    void Update()
    {

        //clear Canvas löscht den Text der Untertitel nach einer gewissen Zeit (60 Einheiten=1 Sekunde (60FPS))
        if (clearCanvas > 1) clearCanvas--;
        else if (clearCanvas == 1)
        {
            untertitel.text = "";
            clearCanvas--;
        }
       

        if (level == levelenum.l1)
        {
            if (transform.position.x > -40) Destroy(tutorial1);
        }

        if (level==levelenum.l2)
        {

            //Neigung des Gangs wird auf Basis der Spielerposition angepasst
            foreach (Transform child in szene2.transform)
            {
                if ((transform.position.x<75) && (transform.position.x>-70)) child.localEulerAngles = new Vector3(transform.position.x * 0.27f, 90, 0);
            }
        }

        if (level == levelenum.l3)
        {

            //Türen werden geöffnet, wenn mit ihnen interagiert wurde
            if (GameObject.Find("TuerBody").GetComponent<BoxCollider>().enabled == false || GameObject.Find("TuerBody2").GetComponent<BoxCollider>().enabled == false)
            {
                Destroy(tutorial2);
                if (Tür1 > 0)
                {
                    GetComponent<VRWalk>().moveForward = false;
                    GetComponent<VRWalk>().fire1Blocked = true;
                    Tür1--;
                    zuBewegen = GameObject.Find("Tuer");
                    zuBewegen.transform.parent = null;
                    zuBewegen.transform.eulerAngles = new Vector3(zuBewegen.transform.eulerAngles.x, Tür1, zuBewegen.transform.eulerAngles.z);
                    zuBewegen = GameObject.Find("Tuer2");
                    zuBewegen.transform.parent = null;
                    zuBewegen.transform.eulerAngles = new Vector3(zuBewegen.transform.eulerAngles.x, -Tür1, zuBewegen.transform.eulerAngles.z);
                }
                else
                {
                    GameObject.Find("TuerBody").GetComponent<BoxCollider>().enabled = true;
                    GameObject.Find("TuerBody").GetComponent<ObjectGazeControl>().enabled = false;
                    GameObject.Find("TuerBody").GetComponent<EventTrigger>().enabled = false;
                    GameObject.Find("TuerBody2").GetComponent<BoxCollider>().enabled = true;
                    GameObject.Find("TuerBody2").GetComponent<ObjectGazeControl>().enabled = false;
                    GameObject.Find("TuerBody2").GetComponent<EventTrigger>().enabled = false;
                    GetComponent<VRWalk>().moveForward = true;
                    GetComponent<VRWalk>().fire1Blocked = false;

                }
            }

            //Wände verschieben sich auf Basis der Spielerposition zunächst zu ihm hin, nach dem Durchlaufen eines Triggers von ihm weg
            if (triggercounter == triggercount.t1){
                zuBewegen = GameObject.Find("S3Links");
                zuBewegen.transform.localPosition = new Vector3(70 + (transform.position.z + 167.4f) * 0.35f, 10.5f, -131);
                zuBewegen = GameObject.Find("S3Rechts");
                zuBewegen.transform.localPosition = new Vector3(-30 - (transform.position.z + 167.4f) * 0.35f, 10.5f, -131);
                zuBewegen = GameObject.Find("S3Decke");
                zuBewegen.transform.localPosition = new Vector3(20.6f, 63 + (transform.position.z + 140) * 0.35f, -131);
            }
            else if (triggercounter == triggercount.t2 && transform.position.z<=-290)
            {
                zuBewegen = GameObject.Find("S3Links");
                zuBewegen.transform.localPosition = new Vector3(27.09f -(transform.position.z + 290) * 0.35f, 10.5f, -131);
                zuBewegen = GameObject.Find("S3Rechts");
                zuBewegen.transform.localPosition = new Vector3(12.91f + (transform.position.z + 290) * 0.35f, 10.5f, -131);
                zuBewegen = GameObject.Find("S3Decke");
                if (transform.position.z>=-331.43f)
                    zuBewegen.transform.localPosition = new Vector3(20.6f, -(transform.position.z + 290) * 0.35f + 10.5f, -131);
            }
        }

        if (level == levelenum.l4)
        {
            //langsame Drosselung des Movespeeds
            if (GetComponent<VRWalk>().speed > 2.5f)
            {
                GetComponent<VRWalk>().speed = GetComponent<VRWalk>().speed - 0.06f;
            }

            //Wendeltreppe rotiert und bewegt sich, nachdem mit ihr interagiert wurde
            GameObject wendeltreppe = GameObject.Find("Wendeltreppe");
            if (wendeltreppe.GetComponent<MeshCollider>().enabled == false && GameObject.Find("Wendeltreppe").GetComponent<ObjectGazeControl>().enabled == true)
            {
                GetComponent<VRWalk>().moveForward = false;
                GetComponent<VRWalk>().fire1Blocked = true;

                untertitel.text = "";

                if (xWert > -90)
                {
                    xWert -= 0.5f;
                    wendeltreppe.transform.eulerAngles = new Vector3(xWert, wendeltreppe.transform.eulerAngles.y, wendeltreppe.transform.eulerAngles.z);
                }
                if (yWert > 222)
                {
                    yWert -= 0.5f;
                    wendeltreppe.transform.eulerAngles = new Vector3(wendeltreppe.transform.eulerAngles.x, yWert, wendeltreppe.transform.eulerAngles.z);
                }
                if (zWert < 207.3f)
                {
                    zWert += 0.5f;
                    wendeltreppe.transform.eulerAngles = new Vector3(wendeltreppe.transform.eulerAngles.x, wendeltreppe.transform.eulerAngles.y, zWert);
                }

                if (wendeltreppe.transform.position.x > 20) wendeltreppe.transform.localPosition = new Vector3(wendeltreppe.transform.position.x - 0.05f, wendeltreppe.transform.position.y, -265.6f);
                if (wendeltreppe.transform.position.x < 19.9) wendeltreppe.transform.localPosition = new Vector3(wendeltreppe.transform.position.x + 0.05f, wendeltreppe.transform.position.y, -265.6f);
                if (wendeltreppe.transform.position.y > 0.33 && yWert < 223) wendeltreppe.transform.localPosition = new Vector3(wendeltreppe.transform.position.x, wendeltreppe.transform.position.y - 0.05f, -265.6f);
                if (wendeltreppe.transform.position.y < 0.4f)
                {
                    untertitel.text = "Navidson geht hoch.";
                    clearCanvas = 300;

                    GameObject.Find("Wendeltreppe").GetComponent<ObjectGazeControl>().enabled = false;
                    GameObject.Find("Wendeltreppe").GetComponent<EventTrigger>().enabled = false;
                    GameObject.Find("TreppenTrigger").GetComponent<BoxCollider>().isTrigger = true;

                    GetComponent<VRWalk>().moveForward = true;
                    GetComponent<VRWalk>().fire1Blocked = false;
                }
            }

            //Spieler erklimmt die Leiter, nachdem mit ihr interagiert wurde
            if (GameObject.Find("Leiter").GetComponent<MeshCollider>().enabled == false)
            {
                GameObject.Find("Leiter").GetComponent<MeshCollider>().enabled = true;
                GameObject.Find("Leiter").GetComponent<EventTrigger>().enabled = false;
                GameObject.Find("Leiter").GetComponent<ObjectGazeControl>().enabled = false;

                level = levelenum.l5;
                untertitel.text = "";

                GetComponent<VRWalk>().enabled = false;
                GetComponent<VRWalk>().moveForward = false;
                GetComponent<VRWalk>().fire1Blocked = true;
                GetComponent<VRWalk>().moveUpward = true;
                GetComponent<VRWalk>().enabled = true;

            }
        }

        //wenn der Spieler oben an der Leiter angekommen ist, wird er nach rechts in den kleinen Raum mit der Tür verschoben
        if (level == levelenum.l5)
        {
            if (transform.position.y > 28.28f)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<VRWalk>().moveUpward = false;
                GetComponent<VRWalk>().leiteranimation = false;

                Vector3 pos = new Vector3();
                pos = transform.position;

                if (transform.position.z > -432.5f)
                {
                    pos -= new Vector3(0f, 0f, 0.03f);
                    transform.position = pos;
                }
                if (transform.position.z <= -432.5f)
                {
                    GetComponent<Rigidbody>().drag = 0f;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<VRWalk>().fire1Blocked = false;
                    GetComponent<VRWalk>().moveForward = true;
                    GetComponent<VRWalk>().speed = 4;

                    GameObject.Find("LeiterBlockade").GetComponent<BoxCollider>().enabled = true;
                    Tür1 = 90;
                    level = levelenum.l6;
                    untertitel.text = "Ein enger Korridor \ngleitet ins Dunkel.";
                    clearCanvas = 300;
                }
            }

            //bevor er jedoch die Leiter erklimmt, wird der Spieler erst in die richtige Position verschoben
            else
            {
                if (transform.position.x > 28)
                {
                    Vector3 pos = new Vector3();
                    pos = transform.position;
                    pos -= new Vector3(0.03f, 0, 0);
                    transform.position = pos;
                }
                if (transform.position.x < 27.9f)
                {
                    Vector3 pos = new Vector3();
                    pos = transform.position;
                    pos += new Vector3(0.03f, 0, 0);
                    transform.position = pos;
                }
                if (transform.position.z > -430)
                {
                    Vector3 pos = new Vector3();
                    pos = transform.position;
                    pos -= new Vector3(0, 0, 0.03f);
                    transform.position = pos;
                }
            }
        }

        if (level == levelenum.l6)
        {
            //Öffnen der Tür, nachdem mit ihr interagiert wurde
            if (GameObject.Find("Level6TuerBody").GetComponent<BoxCollider>().enabled == false)
            {
                if (Tür1 > 0)
                {
                    GetComponent<VRWalk>().moveForward = false;
                    GetComponent<VRWalk>().fire1Blocked = true;
                    Tür1--;
                    zuBewegen = GameObject.Find("Level6Tuer");
                    zuBewegen.transform.eulerAngles = new Vector3(zuBewegen.transform.eulerAngles.x, Tür1, zuBewegen.transform.eulerAngles.z);
                }
                else
                {
                    GameObject.Find("Level6TuerBody").GetComponent<BoxCollider>().enabled = true;
                    GameObject.Find("Level6TuerBody").GetComponent<ObjectGazeControl>().enabled = false;
                    GameObject.Find("Level6TuerBody").GetComponent<EventTrigger>().enabled = false;
                    GetComponent<VRWalk>().moveForward = true;
                    GetComponent<VRWalk>().fire1Blocked = false;
                }
            }

            //Anpassung der Transparenz des Fensters auf Basis der Spielerposition
            if (transform.position.z < -520 && transform.position.z > -545)
            {
                testopacity = (-535 - transform.position.z) / 10;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, testopacity);
                testopacity = (-535 - transform.position.z) / 300;
                mat2.color = new Color(mat2.color.r, mat2.color.g, mat2.color.b, testopacity);
            }

            //Durchschreiten und Öffnen des Fenster
            if (GameObject.Find("FlügelLBody").GetComponent<Collider>().enabled == false || GameObject.Find("FlügelRBody").GetComponent<Collider>().enabled == false)
            {
                GetComponent<VRWalk>().moveForward = false;
                GetComponent<VRWalk>().fire1Blocked = true;

                //öffnen des Fensters
                if (Tür1 > 0)
                {
                    Tür1--;
                    zuBewegen = GameObject.Find("FlügelL");
                    zuBewegen.transform.parent = null;
                    zuBewegen.transform.eulerAngles = new Vector3(zuBewegen.transform.eulerAngles.x, -90+Tür1, zuBewegen.transform.eulerAngles.z);
                    zuBewegen = GameObject.Find("FlügelR");
                    zuBewegen.transform.parent = null;
                    zuBewegen.transform.eulerAngles = new Vector3(zuBewegen.transform.eulerAngles.x, 270-Tür1, zuBewegen.transform.eulerAngles.z);
                }

                    //nach vorne und in die mitte
                    if (transform.position.z >= -544.8f)
                {
                    Vector3 pos = new Vector3();
                    pos = transform.position;
                    pos -= new Vector3(0, 0, 0.015f);
                    transform.position = pos;
                    GetComponent<VRWalk>().leiteranimation = true;
                }
                if (transform.position.x <= 28.28f)
                {
                    Vector3 pos2 = new Vector3();
                    pos2 = transform.position;
                    pos2 += new Vector3(0.015f, 0, 0);
                    transform.position = pos2;
                    GetComponent<VRWalk>().leiteranimation = true;
                }
                else if (transform.position.x >= 28.32f)
                {
                    Vector3 pos2 = new Vector3();
                    pos2 = transform.position;
                    pos2 -= new Vector3(0.015f, 0, 0);
                    transform.position = pos2;
                    GetComponent<VRWalk>().leiteranimation = true;
                }
                //nach oben
                if (transform.position.z < -544.8f && transform.position.z > -547f && transform.position.x > 28.28f && transform.position.x < 28.32f)
                {
                    if (transform.position.y < 30.4f)
                    {
                        GetComponent<VRWalk>().moveUpward = true;
                        GetComponent<VRWalk>().leiteranimation = false;
                    }
                    else GetComponent<VRWalk>().moveUpward = false;
                }

                //hinter dem fenster nach unten
                if (transform.position.y >= 30.4f) 
                {

                    if (transform.position.z >= -547f)
                    {
                        Vector3 pos = new Vector3();
                        pos = transform.position;
                        pos -= new Vector3(0, 0, 0.03f);
                        transform.position = pos;
                    }
                    else
                    {
                        GameObject.Find("FlügelLBody").GetComponent<BoxCollider>().enabled = true;
                        GameObject.Find("FlügelLBody").GetComponent<ObjectGazeControl>().enabled = false;
                        GameObject.Find("FlügelLBody").GetComponent<EventTrigger>().enabled = false;
                        GameObject.Find("FlügelRBody").GetComponent<BoxCollider>().enabled = true;
                        GameObject.Find("FlügelRBody").GetComponent<ObjectGazeControl>().enabled = false;
                        GameObject.Find("FlügelRBody").GetComponent<EventTrigger>().enabled = false;
                        GetComponent<VRWalk>().moveForward = true;
                        GetComponent<Rigidbody>().isKinematic = true;

                        GetComponent<Rigidbody>().drag = 0f;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<VRWalk>().fire1Blocked = false;
                        GetComponent<VRWalk>().speed = 3f;
                        GetComponent<VRWalk>().enabled = true;
                    }
                }
            }
            
        }

        //ggf Neustart des Spiels im Endscreen
        if (level == levelenum.l7)
        {
            if (GameObject.Find("NeustartCollider").GetComponent<Collider>().enabled == false) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }


    //Funktion löscht einen Großteil der Objekte aus dem Spiel, um das Haus verschwinden zu lassen
    void Delete_world()
    {
        for(int i = 2; i <= 6; i++)
        {
            string s = i.ToString();
            Destroy(GameObject.Find("Szene"+s));
            untertitel.text = "Als er sich umdreht und wieder zurück gehen will,\nsieht er, dass das Fenster mitsamt dem Raum verschwunden\nist. Übrig bleibt allein die aschescharze Platte, auf der er\nsteht, nun offenbar von nichts getragen: unten und oben\nFinsternis und natürlich Finsternis auch ringsherum.";
            clearCanvas = 600;
        }
        Destroy(GameObject.Find("FlügelL"));
        Destroy(GameObject.Find("FlügelR"));

    }
}
