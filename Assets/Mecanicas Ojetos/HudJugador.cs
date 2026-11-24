using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudJugador : MonoBehaviour
{
    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonVacio;
    public TMP_Text contadorPiezas;

    void OnEnable()
    {
        NivelManager.VidasCambiaron += ActualizarVidas;
        NivelManager.PiezasCambiaron += ActualizarPiezas;
    }

    void OnDisable()
    {
        NivelManager.VidasCambiaron -= ActualizarVidas;
        NivelManager.PiezasCambiaron -= ActualizarPiezas;
    }

    private void ActualizarVidas(int vidas)
    {
        for (int i = 0; i < corazones.Length; i++)
            corazones[i].sprite = i < vidas ? corazonLleno : corazonVacio;
    }

    private void ActualizarPiezas(int recogidas, int total)
    {
        contadorPiezas.text = recogidas + " / " + total;
    }
}