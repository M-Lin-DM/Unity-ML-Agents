using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.IO;
using MLAgents;
using MLAgents.Sensors;

public class Transporter_Arena : MonoBehaviour
{
    public Transporter_agent agent_p;
    // public Tball tball;
    public GameObject spawnbox;
    public GameObject sinkbox;
    // public TextMeshPro cumulative_R;
    public TextMeshPro timeText;
    public int NumberAgents;
    


    // string filename = "D:/Unity/Transporters/mode_freq_episode.csv"; 

    private void Start()
    {
        // var mode_freqs_increment = GetComponentInChildren<Transporter_agent>().mode_freqs;
        // 
        // WriteArrayToCSV(agent_p.mode_freqs, filename);
        ResetArea();
    }


    public void ResetArea()
    {
        PlaceAgents();
        ClearObjects(GameObject.FindGameObjectsWithTag("Sphere"));
        ClearObjects(GameObject.FindGameObjectsWithTag("Sphere1"));
    }

    void ClearObjects(GameObject[] objects)
    {
        foreach (var j in objects)
        {
            Destroy(j);
        }
    }


    public void UpdateAgentProperties()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");
        foreach (GameObject p in agents)
        {
            if (p.GetComponent<Transporter_agent>().has_passed == 1)
            {
                p.GetComponent<Transporter_agent>().has_passed = 0;
                p.GetComponent<Transporter_agent>().item_carried = 0;
            }
            else if (p.GetComponent<Transporter_agent>().has_received == 1)
            {
                p.GetComponent<Transporter_agent>().has_received = 0;
                p.GetComponent<Transporter_agent>().item_carried = 2; 
            }
            // p.GetComponent<Transform>().up = new Vector3(0,1,0); you cant modify transform directly
            // p.GetComponent<Transform>().Rotate(transform.right, -transform.rotation.x, Space.Self);
            // p.GetComponent<Transform>().Rotate(transform.forward, -transform.rotation.z, Space.Self);
            CheckViolations(p);
            
        }

    }

    public void CheckViolations(GameObject p)
    {
        if (Vector3.Angle(Vector3.up, p.GetComponent<Transform>().up) > 20f)//((Math.Abs(p.GetComponent<Transform>().up.x) > 0.1f) | (Math.Abs(p.GetComponent<Transform>().up.z) > 0.1f))
            {
                // Debug.Log(p.GetComponent<Transform>().forward);
                p.GetComponent<Transform>().LookAt(new Vector3(p.GetComponent<Transform>().forward.x, 0, p.GetComponent<Transform>().forward.z), Vector3.up);
            } 
        // else if (p.GetComponent<Transform>().childCount > 3)
        //     {
        //         // Debug.Log("too many balls carried.." + p.GetComponent<Transform>().childCount.ToString());
        //         p.GetComponent<Transporter_agent>().OnEpisodeBegin();
        //         // 
        //     } 

    }

        private void Update()
    {
        float tt = Time.fixedTime;
        // Update the cumulative reward text
        // GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");
        // float cum_R = 0;
        // foreach (GameObject p in agents)
        // {
        //     cum_R += p.GetComponent<Transporter_agent>().GetCumulativeReward();
        //     if ((transform.Rotation.x != 0)|(transform.Rotation.y != 0))
        //         {}
        // }
        // cumulative_R.text = agent_p.GetCumulativeReward().ToString("0.0");
        // cumulative_R.text = cum_R.ToString("0.00");
        timeText.text = agent_p.StepCount.ToString("0"); //STEPS
        // timeText.text = agent_p.StepCount.ToString("0");
        // timeText.text =tt.ToString("0.0");
        UpdateAgentProperties();


        
    }

    public void PlaceAgents()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");
        foreach (GameObject p in agents)
        {
                p.transform.SetParent(transform);
                p.transform.position = new Vector3(UnityEngine.Random.Range(-20, 20), 0.5f, UnityEngine.Random.Range(-20, 20));           
                p.transform.rotation = Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0, 360), 0f));
         
        }

    }

    // public void WriteArrayToCSV(int[,] data, string file)
    // {
    //     // string filename = "D:/ml-agents-release-0.15.1/ml-agents-release-0.15.1/Project/Assets/ML-Agents/Examples/Thermoregulators/extracted_data/group_mean_Tp_RTg_2.csv";
    //     using (var writer = new StreamWriter(file))
    //     {
    //         for (int i = 0; i < data.GetLength(0); i++)
    //         {
    //             IEnumerable row = ExtractRow(data, i);     //get Enumerator representing/generating the ith row
    //             int[] rowarray = row.Cast<int>().ToArray(); // cast it as an array of ints
    //             string row_string = string.Join(",", rowarray); //convert array to string separated by commas
    //             writer.WriteLine(row_string);  //write each line to csv
    //         }
        
    //     }


    // }

    // public static IEnumerable ExtractRow(int[,] array, int row) //this is an extention method
    // {
    //     for (var i = 0; i < array.GetLength(1); i++)
    //     {
    //         yield return array[row, i];
    //     }
    // }
}

//make a section using EndEpisode() when only green balls captured