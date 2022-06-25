using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float Max_healthPoints;
    public float Acctual_healthPoints;
    [Header("Stamina")]
    public float Max_stamina;
    public float Acctual_stamina;
    [Header("Movement")]
    public float Movement_speed;
    public float Jump_force;
    [Header("View")]
    public float Sensitivity_x;
    public float Sensitivity_y;
}
