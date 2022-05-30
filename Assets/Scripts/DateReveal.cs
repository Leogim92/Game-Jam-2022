using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateReveal : MonoBehaviour
{
    [SerializeField] Image portrait = null;
    [SerializeField] TextMeshProUGUI destinyText = null;

    [System.Serializable]
    public class Destiny
    {
        public Sprite portrait;
        [TextArea]public string destiny;
    }

    [SerializeField] List<Destiny> destinies = null;

    private void Awake()
    {
        SelectRandomDestiny();
    }

    private void SelectRandomDestiny()
    {
        int random = Random.Range(0, destinies.Count);
        portrait.sprite = destinies[random].portrait;
        destinyText.text = destinies[random].destiny;
    }
}
