using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] CinemachineBrain brain;
    [SerializeField] CinemachineBlenderSettings blends;
    CinemachineBlenderSettings old_blends;
    [SerializeField] GameObject[] cameras;
    GameObject current_camera;
    void OnEnable()
    {
        EventBus.act_Switch_Camera += SwitchCamera;
        old_blends = brain.m_CustomBlends;
        brain.m_CustomBlends = blends;
    }
    void OnDisable()
    {
        if (current_camera != null) current_camera.SetActive(false);
        EventBus.act_Switch_Camera -= SwitchCamera;
        brain.m_CustomBlends = old_blends;
        //main_camera.SetActive(false);
    }
    void SwitchCamera(int _index)
    {
        cameras[_index].SetActive(true);
        if(current_camera!=null) current_camera.SetActive(false);
        //if (!main_camera.activeSelf) main_camera.SetActive(true);
        current_camera = cameras[_index];
    }
}
