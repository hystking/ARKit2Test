using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using Photon.Pun;

public class MyWorldMapManager : WorldMapManager {
    [SerializeField] WorldMapUploader worldMapUploader;
    [SerializeField] PhotonView photonView;
	
    public void UploadCurrentWorldMap()
    {
        session.GetCurrentWorldMapAsync(worldMap => {
            var worldMapInBytes = worldMap.SerializeToByteArray();
            StartCoroutine(worldMapUploader.Upload(worldMapInBytes, path => {
                photonView.RPC("DownloadWorldMap", RpcTarget.AllBuffered, new object[] { path });
            }));
        });
    }
    
    [PunRPC]
    public void DownloadWorldMap(string path)
    {
        Debug.Log("Download World map");
        StartCoroutine(worldMapUploader.Download(path, LoadSerializedWorldMap));
    }

    void LoadSerializedWorldMap(byte[] worldMapInBytes)
    {
        ARWorldMap worldMap = ARWorldMap.SerializeFromByteArray(worldMapInBytes);
        
        Debug.LogFormat("Map loaded. Center: {0} Extent: {1}", worldMap.center, worldMap.extent);

        UnityARSessionNativeInterface.ARSessionShouldAttemptRelocalization = true;

        var config = m_ARCameraManager.sessionConfiguration;
        config.worldMap = worldMap;
        UnityARSessionRunOption runOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;

        Debug.Log("Restarting session with worldMap");
        session.RunWithConfigAndOptions(config, runOption);
    }
}
