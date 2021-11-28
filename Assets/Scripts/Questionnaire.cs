using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questionnaire : MonoBehaviour
{
    public Serializer serializer;
    public Loading loading;

    public List<Slider> sliders = new List<Slider>();
    // Start is called before the first frame update

    public void submitForm()
    {
        List<float> values = new List<float>();
        foreach(Slider s in sliders)
        {
            values.Add(s.value * 100);
        }
        serializer.submitForm(values);
        loading.toReport();
    }
}
