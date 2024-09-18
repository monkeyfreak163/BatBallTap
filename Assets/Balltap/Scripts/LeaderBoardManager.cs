using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderBoardManager : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> score;

    private string publicKey = "833c13a486774312b35b0999f9b70a9036faecd83818f3004834d48d9908dbb5";
    // Start is called before the first frame update
    void Start()
    {
        LoadEntries();
    }
    public void LoadEntries()
    {
        Leaderboards.Ball_Tap.GetEntries(entries =>
        {
            foreach (TextMeshProUGUI name in names)
            {
                name.text = "";
            }
            foreach (TextMeshProUGUI score in score)
            {
                score.text = "";
            }

            float length=Mathf.Min(names.Count,entries.Length);
            for(int i = 0; i < length; i++) {
                names[i].text = entries[i].Username;
                score[i].text = entries[i].Score.ToString();
            }
        });
    }
    public void SetEntries(string userName,int  score)
    {
        Leaderboards.Ball_Tap.UploadNewEntry(userName,score, isSuccusfull=>
        {
            if(isSuccusfull)
            {
                LoadEntries();
            }
        });
    }
}
