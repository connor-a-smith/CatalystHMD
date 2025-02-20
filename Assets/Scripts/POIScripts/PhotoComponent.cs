﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotoComponent : POIScriptComponent {

  private bool alreadyLoaded = false;

  [SerializeField] private bool pathIsExternal = false;

  [SerializeField] private string photoPath;

  [SerializeField] private int maximumNumberOfPictures = 24;

  public GameObject loaderObj;

  public override void Activate() {

    // Calls Activate on parent object.
    base.Activate();

    if (alreadyLoaded) {
      // Sets the photo loader to active.
      loaderObj.GetComponent<PhotoController>().photoHolder.transform.localRotation = Quaternion.identity;
      loaderObj.SetActive(true);
      loaderObj.GetComponent<PhotoController>().photoHolder.SetActive(true);

    }
    else {

      loaderObj = new GameObject();
      loaderObj.transform.position = this.transform.position;
      loaderObj.transform.parent = this.transform;


      PhotoController componentController = loaderObj.AddComponent<PhotoController>();

      componentController.photoPath = photoPath;
      componentController.numPicsToLoad = maximumNumberOfPictures;
      componentController.pathIsExteral = pathIsExternal;

      // Load the photos. Should only happen once on scene start.
      componentController.Load();

      alreadyLoaded = true;
    }
  }

  public override void Deactivate() {

    base.Deactivate();
    // Deactivate the loader object

    if (loaderObj != null) {

      loaderObj.GetComponent<PhotoController>().movingPhotos = false;
      loaderObj.GetComponent<PhotoController>().photoHolder.SetActive(false);
      loaderObj.SetActive(false);

    }
  }
}
