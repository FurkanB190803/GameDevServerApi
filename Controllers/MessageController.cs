using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class KeySender : MonoBehaviour
{
    private string keyPressUrl = "https://app-fe15f6b4-ff30-45f9-acff-a6015aba674a.cleverapps.io/api/message/keypress";
    private string mouseUrl = "https://app-fe15f6b4-ff30-45f9-acff-a6015aba674a.cleverapps.io/api/message/mouse";
    private string cameraUrl = "https://app-fe15f6b4-ff30-45f9-acff-a6015aba674a.cleverapps.io/api/message/camera";

    private Transform cameraTransform;
    private Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();

    private void Start()
    {
        // Kameranýn Transform komponentini al
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // WASD ve ok tuþlarýný kontrol et
        foreach (KeyCode key in keyStates.Keys)
        {
            bool isKeyPressed = Input.GetKey(key);

            if (isKeyPressed && !keyStates.ContainsKey(key))
            {
                // Tuþ ilk kez basýldýðýnda
                keyStates[key] = true;
                StartCoroutine(SendKeyPress(true, key.ToString()));
            }
            else if (!isKeyPressed && keyStates.ContainsKey(key))
            {
                // Tuþ býrakýldýðýnda
                keyStates.Remove(key);
                StartCoroutine(SendKeyPress(false, key.ToString()));
            }
        }

        // Kamera pozisyonu, rotasyonu ve mouse hareketini gönder
        StartCoroutine(SendCameraData());
        StartCoroutine(SendMouseData());
    }

    IEnumerator SendKeyPress(bool isPressed, string key)
    {
        string timestamp = DateTime.Now.ToString("o"); // ISO 8601 formatý

        string json = $"{{\"isPressed\": {isPressed.ToString().ToLower()}, \"key\": \"{key}\", \"timestamp\": \"{timestamp}\"}}";
        yield return SendRequest(keyPressUrl, json);
    }

    IEnumerator SendMouseData()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        string timestamp = DateTime.Now.ToString("o");

        string json = $"{{\"mouseX\": {mouseX}, \"mouseY\": {mouseY}, \"timestamp\": \"{timestamp}\"}}";
        yield return SendRequest(mouseUrl, json);
    }

    IEnumerator SendCameraData()
    {
        Vector3 position = cameraTransform.position;
        Vector3 rotation = cameraTransform.eulerAngles;
        string timestamp = DateTime.Now.ToString("o");

        string json = $"{{\"cameraPosition\": {{\"x\": {position.x}, \"y\": {position.y}, \"z\": {position.z}}}, \"cameraRotation\": {{\"x\": {rotation.x}, \"y\": {rotation.y}, \"z\": {rotation.z}}}, \"timestamp\": \"{timestamp}\"}}";
        yield return SendRequest(cameraUrl, json);
    }

    IEnumerator SendRequest(string url, string json)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"POST Response: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"POST Error: {request.error} - Response: {request.downloadHandler.text}");
            }
        }
    }
}
